namespace GiantCroissant.LearnECS.DrawCustomGrid
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Rendering;
    using Unity.Transforms;
    using UnityEngine;

    public class Testing : MonoBehaviour
    {
        public Mesh mesh;
        public Material material;
        
        private EntityManager _entityManager;
        
        void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            using (var entityArray = new NativeArray<Entity>(2, Allocator.Temp))
            {
                var entityArchetype = _entityManager.CreateArchetype(
                    typeof(RenderMesh),
                    typeof(RenderBounds),
                    typeof(LocalToWorld),
                    typeof(Translation));
                    // typeof(Rotation));

                _entityManager.CreateEntity(entityArchetype, entityArray);
                
                var rnd = new Unity.Mathematics.Random((uint)System.DateTime.UtcNow.Ticks);

                // foreach (var entity in entityArray)
                for (var i = 0; i < entityArray.Length; ++i)
                {
                    var entity = entityArray[i];
                    
                    _entityManager.SetSharedComponentData(entity, new RenderMesh
                    {
                        mesh = GenerateGrid(10, 10),
                        material = material
                    });
                    
                    // _entityManager.SetComponentData(entity, new Translation
                    // {
                    //     Value = rnd.NextFloat3(new float3(-5, -3, 0), new float3(5, 3, 0))
                    // });
                }
            }
        }
        
        private Mesh GenerateGrid(int xSize, int ySize)
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
    
    [DisableAutoCreation]
    public class MoveSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref Translation translation) =>
                {
                    //
                    var moveSpeed = 1.0f;
                    translation.Value.y += moveSpeed * Time.DeltaTime;
                })
                .WithoutBurst()
                .Run();
        }
    }
}
