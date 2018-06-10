using Entitas;
using Finegamedesign.Entitas;
using System;
using System.Collections.Generic;

[Game]
[Serializable]
public sealed class ReceiverComponent : ACloneable, IComponent
{
    public HashSet<int> filterComponentIndexes = new HashSet<int>();
    [NonSerialized]
    public int occupantId = -1;
}
