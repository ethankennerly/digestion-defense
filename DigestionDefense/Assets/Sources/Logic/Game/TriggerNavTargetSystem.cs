using Entitas;
using FineGameDesign.Nav;
using FineGameDesign.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace FineGameDesign.Entitas
{
    public sealed class TriggerNavTargetSystem : ReactiveSystem<GameEntity>
    {
        private static bool s_IsVerbose = false;

        private readonly GameContext m_Context;

        public TriggerNavTargetSystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Reaction.Added()
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            if (!entity.hasTriggerEnter)
                return false;

            return entity.isNavAttractive;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity attractor in entities)
            {
                var trigger = attractor.triggerEnter;
                GameEntity traveler = m_Context.GetEntityWithId(trigger.otherId);

                if (!traveler.hasNavAgent)
                {
                    InitializeNavAgentView(traveler, attractor.navAgent.agent.nav);
                }

                SetDestinationIfIsCloser(traveler.navAgent.agent,
                    GameLinkUtils.GetObject(attractor).transform.position);
            }
        }

        private static void InitializeNavAgentView(GameEntity traveler, NavTilemapController navController)
        {
            var travelerObject = GameLinkUtils.GetObject(traveler);
            var navAgentView = travelerObject.AddComponent<NavAgentComponentView>();
            navAgentView.Initialize();
            var navComponent = navAgentView.Component;
            var navAgent = navComponent.agent;
            navAgent.nav = navController;
        }

        private static void SetDestinationIfIsCloser(NavTilemapAgent agent, Vector3 attractorPosition)
        {
            if (!IsCloser(agent, attractorPosition))
                return;

            agent.destination = attractorPosition;

            if (s_IsVerbose)
                DebugUtil.Log("TriggerNavTargetSystem.SetDestinationIfIsCloser: agent=" + agent +
                    " attractorPosition=" + attractorPosition);
        }

        private static bool IsCloser(NavTilemapAgent agent, Vector3 attractorPosition)
        {
            var pathToAttractor = agent.GetPath(attractorPosition);
            if (pathToAttractor == null)
                return false;

            if (agent.hasPath)
            {
                int attractorDistance = NavTilemapAgent.GetNumSteps(pathToAttractor);
                if (attractorDistance >= agent.pathDistance)
                    return false;
            }

            return true;
        }
    }
}
