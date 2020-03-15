namespace GiantCroissant.LearnECS.DrawGridForWorldMap
{
    using Unity.Entities;
    using Unity.Rendering;
    using Unity.Transforms;
    using UnityEngine;

    [RequiresEntityConversion]
    public class VisualMapAuthoring :
        MonoBehaviour,
        IConvertGameObjectToEntity
    {
        public Mesh mesh;
        public Material material;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponent<VisualMapData>(entity);
            // dstManager.AddComponentData(entity, new VisualMapData());

            // dstManager.AddComponentData(entity, new LocalToWorld());
            // dstManager.AddComponentData(entity, new Translation());
            // dstManager.AddComponentData(entity, new RenderBounds());
            // dstManager.AddSharedComponentData(entity, new RenderMesh
            // {
            //     mesh = mesh,
            //     material = material
            // });
        }
    }
}
