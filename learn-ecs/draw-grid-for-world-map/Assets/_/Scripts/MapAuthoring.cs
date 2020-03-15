namespace GiantCroissant.LearnECS.DrawGridForWorldMap
{
    using System;
    using Unity.Entities;
    using Unity.Rendering;
    using UnityEngine;

    public class MapAuthoring :
        MonoBehaviour,
        IConvertGameObjectToEntity
    {
        public int width;
        public int height;

        public Material material;
        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new MapData
            {
                Width = width,
                Height = height
            });
            dstManager.AddBuffer<MapTileBuffer>(entity);

            // dstManager.AddSharedComponentData(entity, new RenderMesh
            // {
            //     material = material
            // });
        }

        private void OnDrawGizmos()
        {
            var r = new Rect(0, 0, width, height);
            r.position = -(new Vector2(width, height) * 0.5f);

            var pos = r.center;
            var size = new Vector3(width, height, 1);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(pos, size);
        }
    }
}
