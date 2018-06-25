using Finegamedesign.Nav;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class NavAgentComponentView : AGameComponentView<NavAgentComponent>
    {
        [SerializeField]
        private NavTilemapView m_NavTilemapView = null;

        public override void Initialize()
        {
            var agent = m_Component.agent;
            if (agent == null)
            {
                agent = new NavTilemapAgent();
                m_Component.agent = agent;
            }
            if (m_NavTilemapView != null && agent.nav == null)
                agent.nav = m_NavTilemapView.controller;

            agent.position = transform.position;

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
