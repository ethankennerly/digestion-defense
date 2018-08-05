using Entitas;

namespace Finegamedesign.Entitas
{
    public sealed class DigestionDefenseSystems : Feature
    {
        public DigestionDefenseSystems(Contexts contexts)
        {
            Add(new TriggerReactionSystem(contexts));
            Add(new PetriGameSystems(contexts));
            Add(new NavSystems(contexts));
            Add(new HealthSystems(contexts));

            // Last, to publish to external listeners.
            // Otherwise, destroying game object in the middle might be unexpected.
            Add(new GameEventSystems(contexts));
        }
    }
}
