namespace SettlersEngine
{
    public sealed class WalkableNode : IPathNode<object>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsWall { get; set; }

        public bool IsWalkable(object unusedContext)
        {
            return !IsWall;
        }
    }
}
