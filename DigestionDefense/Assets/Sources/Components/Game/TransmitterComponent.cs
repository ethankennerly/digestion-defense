using Entitas;
using Finegamedesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class TransmitterComponent : ACloneable, IComponent
{
    public int[] inputIds = new int[0];
    public int[] outputIds = new int[0];
}
