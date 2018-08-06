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

            // After game mechanics, to publish to external listeners.
            // Otherwise, destroying game object in the middle might be unexpected.
            Add(new GameEventSystems(contexts));

            // After all effects. Equivalent to clean up phase.
            // Otherwise, downstream system might expect entity was enabled.
            Add(new DestroySystem(contexts));
        }
    }
}
