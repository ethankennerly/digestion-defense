using Finegamedesign.Utils;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerComponentView : AGameComponentView<TriggerEnterComponent>
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
            int otherId = GameLinkUtils.GetId(other.gameObject);
            if (otherId < 0)
                return;

            TriggerEnter(otherId);
        }

        private void TriggerEnter(int otherId)
        {
            if (m_Entity.hasTriggerEnter &&
                m_Entity.triggerEnter.otherId == otherId
            )
                return;

            m_Component.otherId = otherId;
            ReplaceComponent();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            m_Entity.isTriggerExit = true;
        }
    }
}
