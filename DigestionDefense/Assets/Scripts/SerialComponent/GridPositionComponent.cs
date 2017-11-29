using System;
using Entitas;

[Serializable]
[Game]
public sealed class GridPositionComponent : SerialComponent
{
    public int x;
    public int y;
}
