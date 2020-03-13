namespace GiantCroissant.LearnECS.LetBulletSpawnInECS
{
    using Unity.Entities;
    using UnityEngine;

    [RequiresEntityConversion]
    public class BulletAuthoring : 
        MonoBehaviour,
        IConvertGameObjectToEntity
    {
        public float moveSpeed;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new Bullet
            {
                MoveSpeed = moveSpeed
            });

            dstManager.AddComponentData(entity, new TimedAlive
            {
                MaxAliveTime = 10.0f,
                CountDown = 0
            });
        }
    }
}
