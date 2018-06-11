using Entitas;
using Entitas.CodeGeneration.Attributes;
using Finegamedesign.Entitas;
using System;
using System.Collections.Generic;

[Game]
[Serializable]
[Event(bindToEntity: true)]
public sealed class ReceiverComponent : ACloneable, IComponent
{
    public const int kNone = -1;

    [NonSerialized]
    public HashSet<int> filterComponentIndexes = new HashSet<int>();

    [NonSerialized]
    public int occupantId = kNone;
}
