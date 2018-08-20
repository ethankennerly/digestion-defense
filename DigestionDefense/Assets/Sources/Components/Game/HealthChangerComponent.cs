using Entitas;
using FineGameDesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class HealthChangerComponent : ACloneable, IComponent
{
    public int value;
}
