namespace GiantCroissant.FollowPartykGalach.UnityEcsSpriteRendering
{
    using System.Collections.Generic;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Rendering;
    using Unity.Transforms;
    using UnityEngine;
    using UnityEngine.U2D;

    public class SingleMatSpriteGenerator : MonoBehaviour
    {
        public int entitiesToSpawn = 10;

        public Mesh spriteMesh;

        public List<Sprite> sprites;
        public SpriteAtlas spriteAtlas;
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
                    typeof(Translation),
                    typeof(NonUniformScale));

                entityManager.CreateEntity(spriteArchetype, spriteEntities);

                var spriteMaterials = new Dictionary<Sprite, Material>();

                for (var i = 0; i < sprites.Count; ++i)
                {
                    // var sprite = sprites[i];
                    
                    var spriteMat = Material.Instantiate(spriteMaterial);
                    // spriteMat.mainTextureOffset = SpriteECSHelper.GetTextureOffset(sprite);
                    // spriteMat.mainTextureScale = SpriteECSHelper.GetTextureSize(sprite);
                    spriteMat.mainTextureOffset = SpriteECSHelper.GetTextureOffset(sprites[i]);
                    spriteMat.mainTextureScale = SpriteECSHelper.GetTextureSize(sprites[i]);

                    // spriteMat.name = sprite.name;
                    spriteMat.name = sprites[i].name;
                    // spriteMat.SetTexture("_BaseMap", sprite.texture);
                    // spriteMat.SetTexture("_BaseMap", sprite.te);
                    // spriteMat.mainTexture = sprite.texture;
                    // spriteMaterials[sprite] = spriteMat;
                    spriteMaterials[sprites[i]] = spriteMat;
                }
                
                var rnd = new Unity.Mathematics.Random((uint)System.DateTime.UtcNow.Ticks);

                for (var i = 0; i < entitiesToSpawn; ++i)
                {
                    var spriteEntity = spriteEntities[i];

                    var sprite = sprites[rnd.NextInt(sprites.Count)];
                    
                    // spriteMaterials[sprite].SetTexture("_BaseMap", sprite.texture);
                    
                    entityManager.SetSharedComponentData(spriteEntity, new RenderMesh
                    {
                        mesh = spriteMesh,
                        material = spriteMaterials[sprite]
                    });
                    
                    entityManager.SetComponentData(spriteEntity, new Translation
                    {
                        Value = rnd.NextFloat3(new float3(-5, -3, 0), new float3(5, 3, 0))
                    });
                    
                    entityManager.SetComponentData(spriteEntity, new NonUniformScale
                    {
                        Value = SpriteECSHelper.GetQuadScale(sprite)
                    });
                }
            // }
            spriteEntities.Dispose();
            spriteMaterials.Clear();
        }
    }    
}