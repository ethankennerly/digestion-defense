using Entitas;
using UnityEngine;

namespace FineGameDesign.Entitas
{
    public sealed class UpdateNavAgentSystem : IExecuteSystem
    {
        private readonly GameContext m_Context;
        private readonly IGroup<GameEntity> m_NavAgentGroup;

        public UpdateNavAgentSystem(Contexts contexts)
        {
            m_Context = contexts.game;
            m_NavAgentGroup = m_Context.GetGroup(GameMatcher.NavAgent);
        }

        public void Execute()
        {
            float deltaTime = Time.deltaTime;

            foreach (GameEntity navigator in m_NavAgentGroup.GetEntities())
            {
                navigator.navAgent.agent.Update(deltaTime);
            }
        }
    }
}
