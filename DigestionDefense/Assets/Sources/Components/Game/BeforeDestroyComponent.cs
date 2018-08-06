using Entitas;
using Entitas.CodeGeneration.Attributes;

[Event(bindToEntity: false)]
[Game]
public sealed class BeforeDestroyComponent : IComponent
{
}
