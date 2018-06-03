using Entitas;
using System;

[Game]
[Serializable]
public sealed class ReceiverComponent : ACloneable, IComponent
{
    public ComponentCount[] filterMaxComponents = new ComponentCount[0];
    public int occupantId;
}
