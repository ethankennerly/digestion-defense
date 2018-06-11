using Entitas;
using System.Collections.Generic;

namespace Finegamedesign.Entitas
{
    public sealed class PetriGameSystems : Feature
    {
        public PetriGameSystems(Contexts contexts)
        {
            Add(new TransmissionSystem(contexts));
            Add(new GameEventSystems(contexts));
        }
    }
}
