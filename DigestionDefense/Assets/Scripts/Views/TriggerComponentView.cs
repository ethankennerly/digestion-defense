using Finegamedesign.Utils;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerComponentView : AGameComponentView<TriggerComponent>
    {
        private const int kNoneId = -1;

        [SerializeField]
        private Collider2D m_Trigger = null;

        [SerializeField]
        private Rigidbody2D m_KinematicBody = null;

        public TriggerComponentView()
        {
            m_ReplaceComponentOnInitialize = false;
        }

        private void OnValidate()
        {
            DebugUtil.Assert(m_Trigger != null,
                this + ".OnValidate: Has no Trigger.");

            DebugUtil.Assert(m_KinematicBody != null,
                this + ".OnValidate: Has no Kinematic Body.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            int targetId = GameLinkUtils.GetId(other.gameObject);
            if (targetId < 0)
                return;

            Trigger(targetId);
        }

        private void Trigger(int targetId)
        {
            int sourceId = m_Entity.id.value;
            if (sourceId < 0)
                return;

            if (m_Entity.hasTrigger &&
                m_Entity.trigger.sourceId == sourceId &&
                m_Entity.trigger.targetId == targetId
            )
                return;

            m_Component.targetId = targetId;
            m_Component.sourceId = sourceId;
            ReplaceComponent();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            m_Entity.isTriggerExit = true;
        }
    }
}
