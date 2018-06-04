using Entitas;
using Finegamedesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class ConsumableComponent : ACloneable, IComponent
{
    public string name;
}
