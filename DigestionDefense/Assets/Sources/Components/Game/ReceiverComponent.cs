using Entitas;
using Entitas.CodeGeneration.Attributes;
using Finegamedesign.Entitas;
using System.Collections.Generic;

[Game]
[Event(bindToEntity: true)]
public sealed class ReceiverComponent : IComponent
{
    public HashSet<int> filterComponentIndexes = new HashSet<int>();

    public int[] occupantIds = new int[1]{ReceiverUtils.kEmpty};

    public int availableIndex = 0;
}
