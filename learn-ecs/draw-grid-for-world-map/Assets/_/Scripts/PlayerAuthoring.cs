namespace GiantCroissant.LearnECS.DrawGridForWorldMap
{
    using Unity.Entities;
    using Unity.Mathematics;
    using UnityEngine;

    public class PlayerAuthoring :
        MonoBehaviour,
        IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<Player>(entity);
            dstManager.AddComponentData(entity, new GridPosition
            {
                Value = int2.zero
            });
        }
    }
}
