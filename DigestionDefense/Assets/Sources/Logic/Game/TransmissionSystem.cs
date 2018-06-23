using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class TransmissionSystem : ReactiveSystem<GameEntity>
    {
        private readonly GameContext m_Context;

        /// <remarks>
        /// Bidirectional links would be fastest at scale.
        /// For single-inputs this system would run faster at scale
        /// if the inputs to transmitter were listed as outputs of the receiver.
        /// Although for multi-inputs, there would be a search of all receivers.
        /// </remarks>
        private readonly IGroup<GameEntity> m_TransmitterGroup;

        public TransmissionSystem(Contexts contexts) : base(contexts.game)
        {
            m_Context = contexts.game;
            m_TransmitterGroup = m_Context.GetGroup(GameMatcher.Transmitter);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(
                GameMatcher.Receiver
            );
        }

        /// <summary>
        /// Does not filter receivers not linked to a transmitter,
        /// because the receiver may be indirectly linked.
        /// </summary>
        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity[] transmitters = m_TransmitterGroup.GetEntities();
            foreach (GameEntity transmitter in transmitters)
            {
                foreach (GameEntity receiver in entities)
                {
                    ReceiverUtils.TryTransmit(m_Context, receiver,
                        transmitter.transmitter.inputIds,
                        transmitter.transmitter.outputIds);
                }
            }
        }
    }
}
