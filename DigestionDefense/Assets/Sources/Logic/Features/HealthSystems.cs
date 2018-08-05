using Entitas;

namespace Finegamedesign.Entitas
{
    public sealed class HealthSystems : Feature
    {
        public HealthSystems(Contexts contexts)
        {
            Add(new HealthTriggerSystem(contexts));
            Add(new HealthEmptyDestroySystem(contexts));
            Add(new SpawnBeforeDestroySystem(contexts));
        }
    }
}
