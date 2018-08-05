using Entitas;
using Finegamedesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class HealthComponent : ACloneable, IComponent
{
    public int value;
}
