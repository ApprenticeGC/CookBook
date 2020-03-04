namespace GiantCroissant.FollowPartykGalach.UnityEcsSpriteRendering
{
    using System;
    using Unity.Mathematics;
    using UnityEngine;

    public class SpriteECSHelper
    {
        public static Vector2 GetTextureOffset(Sprite sprite)
        {
            var offset = Vector2.Scale(sprite.rect.position,
                new Vector2(1.0f / sprite.texture.width, 1.0f / sprite.texture.height));

            return offset;
        }

        public static Vector2 GetTextureSize(Sprite sprite)
        {
            var size = Vector2.Scale(sprite.rect.size,
                new Vector2(1.0f / sprite.texture.width, 1.0f / sprite.texture.height));

            return size;
        }

        public static float3 GetQuadScale(Sprite sprite)
        {
            return new float3(
                sprite.rect.width / sprite.pixelsPerUnit,
                sprite.rect.height / sprite.pixelsPerUnit, 1.0f);
        }
    }
}