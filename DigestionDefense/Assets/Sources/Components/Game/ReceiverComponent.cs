using Entitas;
using System;

[Game]
[Serializable]
public sealed class ReceiverComponent : IComponent
{
    public ComponentCount[] filterMaxComponents = new ComponentCount[0];
    public int occupantId;
}
