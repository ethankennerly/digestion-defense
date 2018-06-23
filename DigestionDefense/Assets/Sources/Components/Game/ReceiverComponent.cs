using Entitas;
using Entitas.CodeGeneration.Attributes;
using Finegamedesign.Entitas;
using System.Collections.Generic;

[Game]
[Event(bindToEntity: true)]
public sealed class ReceiverComponent : IComponent
{
    public const int kNone = -1;

    public HashSet<int> filterComponentIndexes = new HashSet<int>();

    public int occupantId = kNone;
}
