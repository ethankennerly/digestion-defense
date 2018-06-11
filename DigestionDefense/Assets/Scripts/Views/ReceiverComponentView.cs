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
        private GameObject m_OccupantObject = null;

        /// <summary>
        /// During initialization, converts filter component names to indexes.
        /// Otherwise, if the component indexes changed after editing this view, the indexes would mismatch.
        /// </summary>
        protected override void Initialize()
        {
            if (m_OccupantObject != null)
            {
                GameEntity occupantEntity = GameLinkUtils.TryLink(m_OccupantObject);
                DebugUtil.Assert(occupantEntity != null, "ReceiverComponentView.Initialize: Expected "
                    + m_OccupantObject + " was linked to an entity.");

                m_Component.occupantId = occupantEntity.id.value;
            }

            if (m_FilterComponentNames != null)
                ToComponentIndexes(m_FilterComponentNames, ref m_Component.filterComponentIndexes);

            base.Initialize();

            GameEntity receiverEntity = GameLinkUtils.GetEntity(gameObject);
            receiverEntity.AddReceiverListener(this);
        }

        private void OnValidate()
        {
            if (m_FilterComponentNames != null)
                ToComponentIndexes(m_FilterComponentNames, ref m_Component.filterComponentIndexes);
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

        public void OnReceiver(GameEntity receiver, HashSet<int> newFilterComponentIndexes, int occupantId)
        {
            GameLinkUtils.TryAddChild(receiver.id.value, occupantId);
        }
    }
}
