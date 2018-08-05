using Entitas;
using Finegamedesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class HealthChangerComponent : ACloneable, IComponent
{
    public int value;
}
