using Entitas;
using FineGameDesign.Entitas;
using System;

[Game]
[Serializable]
public sealed class HealthComponent : ACloneable, IComponent
{
    public int value;
}
