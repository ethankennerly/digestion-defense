using Entitas;

namespace FineGameDesign.Entitas
{
    public sealed class NavSystems : Feature
    {
        public NavSystems(Contexts contexts)
        {
            Add(new TriggerNavTargetSystem(contexts));
            Add(new TriggerDestinationLoopSystem(contexts));
            Add(new TriggerExitLoopingDisabledSystem(contexts));
            Add(new UpdateNavAgentSystem(contexts));
        }
    }
}
