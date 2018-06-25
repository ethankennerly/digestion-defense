using UnityEngine;
using Finegamedesign.Nav;

namespace Finegamedesign.Entitas
{
    public sealed class NavAgentComponentView : AGameComponentView<NavAgentComponent>
    {
        [SerializeField]
        private NavTilemapView m_NavTilemapView = null;

        protected override void Initialize()
        {
            m_Component.agent = new NavTilemapAgent();
            m_Component.agent.nav = m_NavTilemapView.controller;

            base.Initialize();
        }
    }
}
