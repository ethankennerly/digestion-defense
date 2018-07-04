using Finegamedesign.Nav;
using Finegamedesign.Utils;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class NavAgentComponentView : AGameComponentView<NavAgentComponent>
    {
        [SerializeField]
        private NavTilemapView m_NavTilemapView = null;

        [SerializeField]
        private NavTilemapAgent m_Agent = new NavTilemapAgent();

        public override void Initialize()
        {
            DebugUtil.Assert(m_Agent != null,
                this + ".Initialize: Expected agent exists.");

            m_Component.agent = m_Agent;
            if (m_NavTilemapView != null && m_Agent.nav == null)
                m_Agent.nav = m_NavTilemapView.controller;

            m_Agent.position = transform.position;

            base.Initialize();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Component.agent.onPositionChanged += UpdatePosition;
        }

        protected override void OnDisable()
        {
            m_Component.agent.onPositionChanged -= UpdatePosition;

            base.OnDisable();
        }

        private void UpdatePosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}
