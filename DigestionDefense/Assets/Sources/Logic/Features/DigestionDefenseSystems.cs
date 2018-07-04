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
        }
    }
}
