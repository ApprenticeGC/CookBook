namespace GiantCroissant.FollowPartykGalach.UnityEcsSpriteRendering
{
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Rendering;
    using Unity.Transforms;
    using UnityEngine;

    public class SimpleSpriteGenerator : MonoBehaviour
    {
        public int entitiesToSpawn = 10;

        public Mesh spriteMesh;

        public Material spriteMaterial;

        void Start()
        {
            GenerateEntities();
        }

        private void GenerateEntities()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            // using (var spriteEntities = new NativeArray<Entity>(entitiesToSpawn, Allocator.Temp))
            var spriteEntities = new NativeArray<Entity>(entitiesToSpawn, Allocator.Temp);
            // {
                var spriteArchetype = entityManager.CreateArchetype(
                    typeof(RenderMesh),
                    typeof(RenderBounds),
                    typeof(LocalToWorld),
                    typeof(Translation));

                entityManager.CreateEntity(spriteArchetype, spriteEntities);
                
                var rnd = new Unity.Mathematics.Random((uint)System.DateTime.UtcNow.Ticks);

                for (var i = 0; i < entitiesToSpawn; ++i)
                {
                    var spriteEntity = spriteEntities[i];
                    
                    entityManager.SetSharedComponentData(spriteEntity, new RenderMesh
                    {
                        mesh = spriteMesh,
                        material = spriteMaterial
                    });
                    
                    entityManager.SetComponentData(spriteEntity, new Translation
                    {
                        Value = rnd.NextFloat3(new float3(-5, -3, 0), new float3(5, 3, 0))
                    });
                }
            // }
            spriteEntities.Dispose();
        }
    }    
}