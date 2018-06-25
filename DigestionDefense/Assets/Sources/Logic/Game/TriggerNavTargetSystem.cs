using Entitas;
using Finegamedesign.Nav;
using Finegamedesign.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerNavTargetSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        public TriggerNavTargetSystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Trigger
            );
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity triggerEntity in entities)
            {
                var trigger = triggerEntity.trigger;
                GameEntity attractor = m_Context.GetEntityWithId(trigger.sourceId);
                GameEntity traveler = m_Context.GetEntityWithId(trigger.targetId);
                if (traveler == null)
                    continue;

                if (!traveler.hasNavAgent)
                {
                    var nav = attractor.navAgent.agent.nav;
                    var travelerObject = GameLinkUtils.GetObject(traveler);
                    var navAgentView = travelerObject.AddComponent<NavAgentComponentView>();
                    navAgentView.Initialize();
                    var navComponent = navAgentView.Component;
                    var navAgent = navComponent.agent;
                    navAgent.nav = nav;
                }

                SetDestinationIfIsCloser(traveler.navAgent.agent,
                    GameLinkUtils.GetObject(attractor).transform.position);
            }
        }

        private static void SetDestinationIfIsCloser(NavTilemapAgent agent, Vector3 attractorPosition)
        {
            if (!IsCloser(agent, attractorPosition))
                return;

            agent.destination = attractorPosition;
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
