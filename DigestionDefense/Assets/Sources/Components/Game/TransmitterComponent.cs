using Entitas;
using Finegamedesign.Entitas;

[Game]
public sealed class TransmitterComponent : IComponent
{
    public int[] inputIds = new int[0];

    public int[] outputIds = new int[0];
}
