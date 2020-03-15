namespace GiantCroissant.LearnECS.DrawGridForWorldMap
{
    using System;
    using Unity.Burst;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Rendering;
    using UnityEngine;

    public enum TileType : int
    {
        Floor = 0,
        Wall = 1
    }

    public struct MapTileBuffer : IBufferElementData
    {
        public TileType Value;
        public static implicit operator TileType(MapTileBuffer b) => b.Value;
        public static implicit operator MapTileBuffer(TileType v) => new MapTileBuffer { Value = v };
    }

    public struct MapData : IComponentData
    {
        public int Height;
        public int Width;
    }

    public struct VisualMapData : IComponentData
    {
        
    }
    
    public struct Player : IComponentData
    {
    }

    public struct GridPosition : IComponentData
    {
        public int2 Value;
    }

    // public struct GridRenderMesh :
    //     ISharedComponentData,
    //     IEquatable<GridRenderMesh>
    // {
    //     public Mesh Mesh;
    //     public Material Material;
    //
    //     public bool Equals(GridRenderMesh other)
    //     {
    //         return Equals(Mesh, other.Mesh) && Equals(Material, other.Material);
    //     }
    //
    //     public override bool Equals(object obj)
    //     {
    //         return obj is GridRenderMesh other && Equals(other);
    //     }
    //
    //     public override int GetHashCode()
    //     {
    //         unchecked
    //         {
    //             return ((Mesh != null ? Mesh.GetHashCode() : 0) * 397) ^ (Material != null ? Material.GetHashCode() : 0);
    //         }
    //     }
    // }
    
    public static class MeshUtility
    {
        public static Mesh GenerateGrid(int xSize, int ySize)
        {
        
            var mesh = new Mesh();
            mesh.name = "Procedural Grid";
        
            //calculate number of vertices
            var vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        
            Vector2[] uv = new Vector2[vertices.Length];
        
            //positioning vertices and providing Uv coordinates
            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x, y);
                    uv[i] = new Vector2((float)x / xSize,(float) y / ySize);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
        
            int[] triangles = new int[xSize * ySize * 6];
            for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            {
                for (int x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                    triangles[ti + 5] = vi + xSize + 2;
                }
            }
            mesh.triangles = triangles;
        
            mesh.RecalculateNormals();
        
            return mesh;
        }
    }

    public class GenerateMapSystem : SystemBase
    {
        private BeginSimulationEntityCommandBufferSystem _entityCommandBufferSystem;
    
        private EntityQuery _mapQuery;
        private EntityArchetype _visualMapArchetype;

        protected override void OnCreate()
        {
            base.OnCreate();

            _mapQuery = GetEntityQuery(
                ComponentType.ReadOnly<GenerateMap>(),
                ComponentType.ReadOnly<MapData>(),
                ComponentType.ReadOnly<MapTileBuffer>());

            _visualMapArchetype = EntityManager.CreateArchetype(
                typeof(GenerateVisualMap));

            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
            
            RequireForUpdate(_mapQuery);
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
            var concurrentCommandBuffer = commandBuffer.ToConcurrent();

            var mapEntity = _mapQuery.GetSingletonEntity();
            // var mapData = EntityManager.GetComponentData<MapData>(mapEntity);
            var mapTileBuffer = EntityManager.GetBuffer<MapTileBuffer>(mapEntity);
            
            var visualMapArchetype = EntityManager.CreateArchetype(
                typeof(GenerateVisualMap));

            Entities
                .WithAll<GenerateMap>()
                .WithName("GenerateMapSystem_InitializeMap")
                .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
                .ForEach((Entity entity, int entityInQueryIndex,
                    ref MapData mapData,
                    ref DynamicBuffer<MapTileBuffer> buffer) =>
                {
                    var w = mapData.Width;
                    var h = mapData.Height;

                    buffer.ResizeUninitialized(w * h);

                    for (var i = 0; i < buffer.Length; ++i)
                    {
                        buffer[i] = TileType.Floor;
                        // buffer[i] = TileType.Wall;
                    }

                })
                .Schedule();

            // Dependency = job.Schedule(Dependency);

            Entities
                .WithReadOnly(mapTileBuffer)
                .WithAll<Player>()
                .WithName("GenerateMapSystem_PlacePlayer")
                .ForEach((Entity entity, int entityInQueryIndex, ref GridPosition gridPosition) =>
                {
                    Debug.Log($"Player keep being placed");
                    if (mapTileBuffer[20] == TileType.Floor)
                    {
                        gridPosition.Value = new int2(3, 5);
                    }
                })
                .Schedule();
            // Dependency = job2.Schedule(Dependency);
            
            // Entities
            //     .WithAll<GenerateMap>()
            //     .WithName("GenerateMapSystem_SetMesh")
            //     .WithoutBurst()
            //     .ForEach((Entity entity, int entityInQueryIndex, MapData mapData, RenderMesh renderMesh) =>
            //     {
            //         var w = mapData.Width;
            //         var h = mapData.Height;
            //
            //         var mesh = MeshUtility.GenerateGrid(w, h);
            //         renderMesh.mesh = mesh;
            //         
            //         commandBuffer.SetSharedComponent(entity, renderMesh);
            //     })
            //     .Run();

            Entities
                .WithAll<GenerateMap>()
                .WithName("GenerateMapSystem_RemoveGenerateTag")
                .WithBurst(FloatMode.Default, FloatPrecision.Standard, true)
                .ForEach((Entity entity, int entityInQueryIndex) =>
                {
                    var eventEntity =
                        concurrentCommandBuffer.CreateEntity(entityInQueryIndex, visualMapArchetype);
                    
                    concurrentCommandBuffer.AddComponent<GenerateVisualMap>(entityInQueryIndex, eventEntity);
                    
                    concurrentCommandBuffer.RemoveComponent<GenerateMap>(entityInQueryIndex, entity);
                })
                .Schedule();
            
            // commandBuffer.RemoveComponent<GenerateMap>(_mapQuery);
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }
    
    [UpdateInGroup(typeof(PresentationSystemGroup))]
    public class ModifyVisualMapSystem : SystemBase
    {
        // private BeginSimulationEntityCommandBufferSystem _entityCommandBufferSystem;
        private BeginPresentationEntityCommandBufferSystem _entityCommandBufferSystem;
        private EntityQuery _mapQuery;
        private EntityQuery _visualMapQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            // _entityCommandBufferSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
            _entityCommandBufferSystem = World.GetOrCreateSystem<BeginPresentationEntityCommandBufferSystem>();
         
            _mapQuery = GetEntityQuery(
                ComponentType.ReadOnly<MapData>(),
                ComponentType.ReadOnly<MapTileBuffer>());

            _visualMapQuery = GetEntityQuery(
                ComponentType.ReadOnly<VisualMapData>(),
                typeof(RenderMesh));
            
            RequireForUpdate(_mapQuery);
            RequireForUpdate(_visualMapQuery);
        }

        protected override void OnUpdate()
        {
            var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
            // var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer().ToConcurrent();
            
            var mapEntity = _mapQuery.GetSingletonEntity();
            var mapData = _mapQuery.GetSingleton<MapData>();
            var mapTileBuffer = EntityManager.GetBuffer<MapTileBuffer>(mapEntity);

            var visualMapEntity = _visualMapQuery.GetSingletonEntity();
            var renderMesh = EntityManager.GetSharedComponentData<RenderMesh>(visualMapEntity);

            Entities
                .WithAll<GenerateVisualMap>()
                // .ForEach((Entity entity) =>
                .ForEach((Entity entity, int entityInQueryIndex) =>
                {
                    var w = mapData.Width;
                    var h = mapData.Height;
                    Debug.Log($"Making grid mesh - w: {w} h: {h}");
                    var mesh = MeshUtility.GenerateGrid(w, h);

                    renderMesh.mesh = mesh;
                    commandBuffer.SetSharedComponent(visualMapEntity, renderMesh);
                    // commandBuffer.SetSharedComponent(entityInQueryIndex, visualMapEntity, renderMesh);

                    commandBuffer.DestroyEntity(entity);
                    // commandBuffer.DestroyEntity(entityInQueryIndex, entity);
                })
                .WithoutBurst()
                .Run();
                // .Schedule();
            
            _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
        }
    }

    // // [AlwaysSynchronizeSystem]
    // [UpdateInGroup(typeof(PresentationSystemGroup))]
    // public class PresentMapSystem : SystemBase
    // {
    //     private BeginPresentationEntityCommandBufferSystem _entityCommandBufferSystem;
    //     private EntityQuery _mapQuery;
    //     private EntityQuery _visualMapQuery;
    //
    //     protected override void OnCreate()
    //     {
    //         base.OnCreate();
    //         
    //         _mapQuery = GetEntityQuery(
    //             ComponentType.ReadOnly<MapData>(),
    //             ComponentType.ReadOnly<MapTileBuffer>());
    //
    //         _visualMapQuery = GetEntityQuery(
    //             ComponentType.ReadOnly<VisualMapData>(),
    //             typeof(RenderMesh));
    //
    //         _entityCommandBufferSystem = World.GetOrCreateSystem<BeginPresentationEntityCommandBufferSystem>();
    //         
    //         RequireForUpdate(_mapQuery);
    //         RequireForUpdate(_visualMapQuery);
    //     }
    //
    //     protected override void OnUpdate()
    //     {
    //         var commandBuffer = _entityCommandBufferSystem.CreateCommandBuffer();
    //         
    //         var mapEntity = _mapQuery.GetSingletonEntity();
    //         var mapData = _mapQuery.GetSingleton<MapData>();
    //         var mapTileBuffer = EntityManager.GetBuffer<MapTileBuffer>(mapEntity);
    //
    //         var visualMapEntity = _visualMapQuery.GetSingletonEntity();
    //         var renderMesh = EntityManager.GetSharedComponentData<RenderMesh>(visualMapEntity);
    //         
    //         Job
    //             .WithoutBurst()
    //             .WithCode(() =>
    //             {
    //                 //
    //                 var w = mapData.Width;
    //                 var h = mapData.Height;
    //                 // Debug.Log($"Making grid mesh - w: {w} h: {h}");
    //                 var mesh = MeshUtility.GenerateGrid(w, h);
    //                 
    //                 // commandBuffer.AddComponent(mapEntity, );
    //                 renderMesh.mesh = mesh;
    //                 commandBuffer.SetSharedComponent(visualMapEntity, renderMesh);
    //             })
    //             .Run();
    //         
    //         _entityCommandBufferSystem.AddJobHandleForProducer(Dependency);
    //     }
    // }
    
    public class Bootstrap : MonoBehaviour
    {
        
    }    
}
