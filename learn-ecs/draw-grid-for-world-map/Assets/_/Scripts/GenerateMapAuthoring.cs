namespace GiantCroissant.LearnECS.DrawGridForWorldMap
{
    using Unity.Entities;
    using UnityEngine;

    public struct GenerateMap : IComponentData
    {
    }
    
    public class GenerateMapAuthoring :
        MonoBehaviour,
        IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new GenerateMap());
        }
    }
}
