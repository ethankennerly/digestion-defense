using Entitas;
using Finegamedesign.Nav;
using Finegamedesign.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Finegamedesign.Entitas
{
    public sealed class TriggerNavTargetSystem : ReactiveSystem<GameEntity>
    {
        private static s_IsVerbose = false;

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
            var trigger = entity.trigger;
            GameEntity traveler = m_Context.GetEntityWithId(trigger.targetId);
            if (traveler == null)
                return false;

            GameEntity attractor = m_Context.GetEntityWithId(trigger.sourceId);
            DebugUtil.Assert(attractor != null,
                "Filter expected trigger source was defined. " +
                "Trigger source ID=" + trigger.sourceId + " traveler=" + traveler);
            if (attractor == null)
                return false;

            DebugUtil.Assert(attractor.hasReceiver,
                "Filter expected trigger source has receiver. " +
                "Trigger source ID=" + attractor + " traveler=" + traveler);
            if (!attractor.hasReceiver)
                return false;

            return ReceiverUtils.Filter(attractor.receiver, traveler);
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (GameEntity triggerEntity in entities)
            {
                var trigger = triggerEntity.trigger;
                GameEntity attractor = m_Context.GetEntityWithId(trigger.sourceId);
                GameEntity traveler = m_Context.GetEntityWithId(trigger.targetId);

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
