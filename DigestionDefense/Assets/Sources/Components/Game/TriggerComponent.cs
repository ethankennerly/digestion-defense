using Entitas;
using Finegamedesign.Entitas;

[Game]
public sealed class TriggerComponent : IComponent
{
    public int sourceId = -1;

    public int targetId = -1;
}
