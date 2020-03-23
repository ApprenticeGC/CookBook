namespace Game
{
    using Unity.Entities;
    using Unity.Mathematics;

    public struct Unit : IComponentData
    {
    }

    public struct Team : IComponentData
    {
    }

    public struct PathfindingGroup : IComponentData
    {
        public Entity Value;
    }
    
    //
    public struct MapGrid : IComponentData
    {
        public int Width;
        public int Height;
    }
    
    public struct PathTileBuffer : IBufferElementData
    {
        public Entity Value;
        public static implicit operator Entity(PathTileBuffer b) => b.Value;
        public static implicit operator PathTileBuffer(Entity v) => new PathTileBuffer { Value = v };
    }

    public struct OrderedPathTile : IComponentData
    {
        public int Id;
    }
    
    public struct OrderedPathTileBuffer : IBufferElementData
    {
        public int3 Value;
        public static implicit operator int3(OrderedPathTileBuffer b) => b.Value;
        public static implicit operator OrderedPathTileBuffer(int3 v) => new OrderedPathTileBuffer { Value = v };
    }

    public struct PathCellBuffer : IBufferElementData
    {
        public Entity Value;
        public static implicit operator Entity(PathCellBuffer b) => b.Value;
        public static implicit operator PathCellBuffer(Entity v) => new PathCellBuffer { Value = v };
    }

    public struct Shared : ISharedComponentData
    {
        
    }
}
