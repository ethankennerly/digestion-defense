using Finegamedesign.Utils;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerComponentView : AGameComponentView<TriggerComponent>
    {
        [SerializeField]
        private Collider2D m_Trigger = null;

        [SerializeField]
        private Rigidbody2D m_KinematicBody = null;

        private void OnValidate()
        {
            if (m_Trigger == null)
                DebugUtil.LogWarning(this + ".OnValidate: Has no Trigger.");

            if (m_KinematicBody == null)
                DebugUtil.LogWarning(this + ".OnValidate: Has no Kinematic Body.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            m_Component.targetId = GameLinkUtils.GetId(other.gameObject);
            m_Component.sourceId = GameLinkUtils.GetId(gameObject);
            if (m_Component.targetId < 0 || m_Component.sourceId < 0)
                return;

            ReplaceComponent();
            DebugUtil.Assert(false, this + ".OnTriggerEnter2D: TODO: Publish trigger with target=" + other);
        }
    }
}
