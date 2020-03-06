﻿namespace GiantCroissant.FllowCodeMonkey.FindTargetInUnityECS
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Rendering;
    using Unity.Transforms;
    using UnityEngine;

    public class GameHandler : MonoBehaviour
    {
        public Mesh quadMesh;
        public Material unitMaterial;
        public Material targetMaterial;
        
        private EntityManager _entityManager;

        void Start()
        {
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            //
            SpawnUnitEntity();

            //
            for (var i = 0; i < 10; ++i)
            {
                SpawnTargetEntity();
            }
        }

        private void SpawnUnitEntity()
        {
            SpawnUnitEntity(new float3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-5, 5), 0));
        }

        private void SpawnUnitEntity(float3 position)
        {
            var entity = _entityManager.CreateEntity(
                typeof(RenderMesh),
                typeof(RenderBounds),
                typeof(LocalToWorld),
                typeof(Translation),
                typeof(Scale),
                typeof(Unit));

            SetEntityComponentData(entity, position, quadMesh, unitMaterial);
            _entityManager.SetComponentData(entity, new Scale { Value = 1.5f });
        }

        private void SetEntityComponentData(Entity entity, float3 position, Mesh mesh, Material material)
        {
            _entityManager.SetSharedComponentData(entity, new RenderMesh
            {
                mesh = mesh,
                material = material
            });
            
            _entityManager.SetComponentData(entity, new Translation
            {
                Value = position
            });
        }

        private void SpawnTargetEntity()
        {
            var entity = _entityManager.CreateEntity(
                typeof(RenderMesh),
                typeof(RenderBounds),
                typeof(LocalToWorld),
                typeof(Translation),
                typeof(Scale),
                typeof(Target));

            var position = new float3(UnityEngine.Random.Range(-8, 8), UnityEngine.Random.Range(-5, 5), 0);

            SetEntityComponentData(entity, position, quadMesh, targetMaterial);
            _entityManager.SetComponentData(entity, new Scale { Value = 1.5f });
        }
    }
    
    //
    public struct Unit : IComponentData
    {
    }

    public struct Target : IComponentData
    {
    }
}
