using Entitas;

namespace Finegamedesign.Entitas
{
    public sealed class NavSystems : Feature
    {
        public NavSystems(Contexts contexts)
        {
            Add(new TriggerNavTargetSystem(contexts));
            Add(new UpdateNavAgentSystem(contexts));
        }
    }
}
