namespace GiantCroissant.LearnECS.LetBulletSpawnInECS
{
    using System.Collections.Generic;
    using Unity.Entities;
    using Unity.Mathematics;
    using Unity.Transforms;
    using UnityEngine;

    public class EnemyAuthoring : 
        MonoBehaviour,
        IConvertGameObjectToEntity,
        IDeclareReferencedPrefabs
    {
        public GameObject bulletPrefab;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Enemy());

            dstManager.AddComponentData(entity, new Shooting
            {
                Bullet = conversionSystem.GetPrimaryEntity(bulletPrefab)
            });

            dstManager.AddComponentData(entity, new ShootingStyle
            {
                IntervalMax = 2.0f,
                CountDown = 0
            });
            
            var position = Vector3.zero;

            dstManager.AddComponentData(entity, new Translation
            {
                Value = (float3)position
            });
            dstManager.AddComponentData(entity, new LocalToWorld
            {
                Value = float4x4.TRS(position, quaternion.identity, 1)
            });
        }

        public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
        {
            referencedPrefabs.Add(bulletPrefab);
        }
    }
}

