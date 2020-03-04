namespace GiantCroissant.LearnECS.InitialAttemptGameOfLife
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Rendering;
    using Unity.Transforms;
    using UnityEngine;

    public struct CurrentStatus : IComponentData
    {
        public int Value;
    }

    public struct NextStatus : IComponentData
    {
        public int Value;
    }

    public struct Neighbors : IComponentData
    {
        public int nw;
        public int n;
        public int ne;
        public int w;
        public int e;
        public int sw;
        public int s;
        public int se;
    }

    public class GameManager : MonoBehaviour
    {
        public Mesh cellMesh;
        public Material liveMaterial;
        public Material deadMaterial;

        public Camera camera;
        
        public int width = 40;
        public int height = 40;
        
        private EntityManager _entityManager;
        private Unity.Mathematics.Random _random;

        void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            _random = new Unity.Mathematics.Random((uint)System.DateTime.UtcNow.Ticks);

            // var cell = CreateCell(1, 1);
            // CreateCell(0, 0);
            // CreateCell(1, 0);
            // CreateCell(2, 0);
            
            //
            camera.transform.position = new Vector3((width - 1) * 0.5f, (height - 1) * 0.5f, camera.transform.position.z);
            camera.orthographicSize = width * 0.5f;
            
            CreateGrid();
        }

        private void CreateGrid()
        {
            using (var cells = new NativeArray<Entity>(width * height, Allocator.Temp))
            {
                var cellArchetype = _entityManager.CreateArchetype(
                    typeof(RenderMesh),
                    typeof(RenderBounds),
                    typeof(LocalToWorld),
                    typeof(Translation),
                    typeof(CurrentStatus),
                    typeof(NextStatus),
                    typeof(Neighbors));

                _entityManager.CreateEntity(cellArchetype, cells);

                for (var x = 0; x < width; ++x)
                {
                    for (var y = 0; y < height; ++y)
                    {
                        var cell = cells[x * height + y];

                        SetComponentDataForEntity(cell, x, y);
                    }
                }
            }
        }

        // private void CreateGrid()
        // {
        //     var cells = new List<Entity>();
        //     for (var i = 0; i < 10; ++i)
        //     {
        //         cells.Add(CreateCell(i, 0));
        //     }
        // }
        //
        // private Entity CreateCell(int x, int y)
        // {
        //     var cellArchetype = _entityManager.CreateArchetype(
        //         typeof(RenderMesh),
        //         typeof(RenderBounds),
        //         typeof(LocalToWorld),
        //         typeof(Translation));
        //
        //     var entity = _entityManager.CreateEntity(cellArchetype);
        //     
        //     SetComponentDataForEntity(entity, x, y);
        //
        //     return entity;
        // }
        //
        private void SetComponentDataForEntity(Entity entity, int x, int y)
        {
            var v = _random.NextInt(2);
            
            _entityManager.SetSharedComponentData<RenderMesh>(entity, new RenderMesh
            {
                mesh = cellMesh,
                material = (v == 0) ? deadMaterial : liveMaterial
            });
            
            _entityManager.SetComponentData<Translation>(entity, new Translation
            {
                Value = new float3(x, y, 0)
            });
            
            _entityManager.SetComponentData<CurrentStatus>(entity, new CurrentStatus
            {
                Value = v
            });

            _entityManager.SetComponentData<Neighbors>(entity, new Neighbors
            {
                // nw = (y == 0) ? -1 : x - height - 1,
                // n = (y == 0) ? -1 : x - height,
                // ne = (y == 0) ? -1 : x - height + 1,
                // w = x * y - 1,
                // e = x  + 1,
                // sw = (y == height - 1) ? -1 : x + height -1,
                // s = (y == height - 1) ? -1 : x + height,
                // se = (y == height - 1) ? -1 : x + height + 1
            });
        }
    }
}
