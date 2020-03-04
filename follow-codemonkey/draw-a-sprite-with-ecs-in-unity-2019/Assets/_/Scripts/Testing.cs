namespace GiantCroissant.FollowCodeMonkey.DrawASpriteWithEcsInUnity
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
        public Material unitMaterial;
        public Material nonUnitMaterial;
        
        private EntityManager _entityManager;
        
        void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // using (var entityArray = new NativeArray<Entity>(10, Allocator.Temp))
            using (var entityArray = new NativeArray<Entity>(20, Allocator.Temp))
            {
                // var entity = _entityManager.CreateEntity(
                //     typeof(RenderMesh),
                //     typeof(RenderBounds),
                //     typeof(LocalToWorld),
                //     typeof(Translation),
                //     typeof(Rotation),
                //     // typeof(Scale),
                //     typeof(NonUniformScale));
                
                // var entityArchetype = _entityManager.CreateArchetype(
                //     typeof(RenderMesh),
                //     typeof(RenderBounds),
                //     typeof(LocalToWorld),
                //     typeof(Translation),
                //     typeof(Rotation),
                //     // typeof(Scale),
                //     typeof(NonUniformScale));

                var entityArchetype = _entityManager.CreateArchetype(
                    typeof(RenderMesh),
                    typeof(RenderBounds),
                    typeof(LocalToWorld),
                    typeof(Translation),
                    typeof(Rotation));

                _entityManager.CreateEntity(entityArchetype, entityArray);
            
                // _entityManager.SetSharedComponentData(entity, new RenderMesh
                // {
                //     mesh = mesh,
                //     material = material
                // });
                //
                // _entityManager.SetComponentData(entity, new NonUniformScale
                // {
                //     Value = new float3(1.0f, 3.0f, 3.0f)
                // });
                
                var rnd = new Unity.Mathematics.Random((uint)System.DateTime.UtcNow.Ticks);

                var unitMesh = CreateMesh(1.0f, 1.0f);
                var nonUniMesh = CreateMesh(0.6f, 1.0f);
                
                // foreach (var entity in entityArray)
                for (var i = 0; i < entityArray.Length; ++i)
                {
                    var entity = entityArray[i];
                    
                    _entityManager.SetSharedComponentData(entity, new RenderMesh
                    {
                        // mesh = mesh,
                        // mesh = CreateMesh(1.0f, 1.0f),
                        mesh = (i < 10) ? nonUniMesh : unitMesh,
                        // material = material
                        material = (i < 10) ? nonUnitMaterial : unitMaterial
                    });
                    
                    _entityManager.SetComponentData(entity, new Translation
                    {
                        Value = rnd.NextFloat3(new float3(-5, -3, 0), new float3(5, 3, 0))
                    });
                    
                    // _entityManager.SetComponentData(entity, new NonUniformScale
                    // {
                    //     Value = new float3(1.0f, 3.0f, 3.0f)
                    // });
                }
            }
        }

        private Mesh CreateMesh(float width, float height)
        {
            var vertices = new Vector3[4];
            var uv = new Vector2[4];
            var triangles = new int[6];
            
            /*
             * 0, 0
             * 0, 1
             * 1, 1
             * 1, 0
             */

            var halfWidth = width * 0.5f;
            var halfHeight = height * 0.5f;
            
            vertices[0] = new Vector3(-halfWidth, -halfHeight);
            vertices[1] = new Vector3(-halfWidth, halfHeight);
            vertices[2] = new Vector3( halfWidth, halfHeight);
            vertices[3] = new Vector3( halfWidth, -halfHeight);
            
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 3;
            
            triangles[3] = 1;
            triangles[4] = 2;
            triangles[5] = 3;

            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            return mesh;
        }
    }
    
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

    public class RotationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref Rotation rotation) =>
                {
                    //
                    rotation.Value = quaternion.Euler(0, 0, math.PI * Time.realtimeSinceStartup);
                })
                .WithoutBurst()
                .Run();
        }
    }

    public class ScaleSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            Entities
                .ForEach((ref Scale scale) =>
                {
                    //
                    scale.Value += 1.0f * Time.DeltaTime;
                })
                .WithoutBurst()
                .Run();
        }
    }
}
