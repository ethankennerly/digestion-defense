using Finegamedesign.Utils;
using ProGM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class ReceiverComponentView : AGameComponentView<ReceiverComponent>, IReceiverListener
    {
        [Header("If not null, overwrites component indexes.")]
        [Tooltip("The order of indexes are unstable, so uses a string. Then caches to index for performance.")]
        [StringInList(typeof(GameComponentsLookup), "componentNames")]
        [SerializeField]
        private string[] m_FilterComponentNames = null;

        [Header("If not null, overwrites occupant ID when initializing.")]
        [SerializeField]
        private GameObject[] m_OccupantObjects = new GameObject[1];

        /// <summary>
        /// During initialization, converts filter component names to indexes.
        /// Otherwise, if the component indexes changed after editing this view, the indexes would mismatch.
        /// </summary>
        public override void Initialize()
        {
            InitializeOccupants();

            if (m_FilterComponentNames != null)
                ToComponentIndexes(m_FilterComponentNames, ref m_Component.filterComponentIndexes);

            base.Initialize();

            GameEntity receiverEntity = GameLinkUtils.GetEntity(gameObject);
            receiverEntity.AddReceiverListener(this);
        }

        private void InitializeOccupants()
        {
            Debug.Assert(m_OccupantObjects != null,
                "Expected Unity Editor serializes occupant objects as an empty collection.");

            if (m_OccupantObjects == null)
                return;

            Array.Resize(ref m_Component.occupantIds, m_OccupantObjects.Length);
            int index = -1;
            foreach (GameObject occupantObject in m_OccupantObjects)
            {
                GameEntity occupantEntity = GameLinkUtils.TryLink(occupantObject);
                m_Component.occupantIds[++index] = occupantEntity == null ? ReceiverUtils.kEmpty :
                    occupantEntity.id.value;
            }

            m_Component.availableIndex = ReceiverUtils.GetNextAvailableIndex(m_Component);
        }

        private void OnValidate()
        {
            if (m_FilterComponentNames != null)
                ToComponentIndexes(m_FilterComponentNames, ref m_Component.filterComponentIndexes);

            DebugUtil.Assert(m_OccupantObjects.Length >= 1,
                this + ".OnValidate: Occupant objects length=" + m_OccupantObjects.Length +
                " is less than 1, so it will never be occupiable.");
        }

        private static void ToComponentIndexes(string[] componentNames, ref HashSet<int> componentIndexes)
        {
            if (componentIndexes == null)
                componentIndexes = new HashSet<int>();

            componentIndexes.Clear();
            string[] gameComponentNames = GameComponentsLookup.componentNames;
            foreach (string componentName in componentNames)
            {
                int componentIndex = Array.IndexOf(gameComponentNames, componentName);
                DebugUtil.Assert(componentIndex >= 0,
                    "ReceiverComponentView.ToComponentIndexes: Expects component '" + componentName + "' " +
                    "in game component names " + DataUtil.ToString(componentNames));
                if (componentIndex < 0)
                    continue;

                componentIndexes.Add(componentIndex);
            }
        }

        public void OnReceiver(GameEntity receiver, HashSet<int> newFilterComponentIndexes,
            int[] newOccupantIds, int newAvailableIndex)
        {
            GameLinkUtils.TryAddChildren(receiver.id.value, newOccupantIds, silentIfChildIsUnlinked: true);
        }
    }
}
