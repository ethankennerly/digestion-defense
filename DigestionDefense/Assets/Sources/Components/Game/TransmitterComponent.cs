using Entitas;
using FineGameDesign.Entitas;

[Game]
public sealed class TransmitterComponent : IComponent
{
    public int[] inputIds = new int[0];

    public int[] outputIds = new int[0];
}
