using Entitas;

namespace FineGameDesign.Entitas
{
    public sealed class PetriGameSystems : Feature
    {
        public PetriGameSystems(Contexts contexts)
        {
            Add(new TransmissionSystem(contexts));
        }
    }
}
