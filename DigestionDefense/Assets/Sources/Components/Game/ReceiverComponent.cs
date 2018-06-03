using Entitas;
using Finegamedesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class ReceiverComponent : ACloneable, IComponent
{
    public string[] filterComponentNames = new string[0];
    public int occupantId = -1;
}
