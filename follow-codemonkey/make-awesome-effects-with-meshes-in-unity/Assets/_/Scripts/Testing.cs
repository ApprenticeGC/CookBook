namespace GiantCroissant.FollowCodeMonkey.MakeAwesomeEffectsWithMeshsInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Testing : MonoBehaviour
    {
        void Start()
        {
            Mesh mesh = new Mesh();
            
            // var vertices = new Vector3[4];
            // var uvs = new Vector2[4];
            // var triangles = new int[6];
            //
            // vertices[0] = new Vector3(0, 0, 0);
            // vertices[1] = new Vector3(0, 100, 0);
            // vertices[2] = new Vector3(100, 100, 0);
            // vertices[3] = new Vector3(100, 0, 0);
            //
            // uvs[0] = new Vector2(0, 0);
            // uvs[1] = new Vector2(0, 1);
            // uvs[2] = new Vector2(1, 1);
            // uvs[2] = new Vector2(1, 0);
            //
            // triangles[0] = 0;
            // triangles[1] = 1;
            // triangles[2] = 2;
            //
            // triangles[3] = 0;
            // triangles[4] = 2;
            // triangles[5] = 3;

            // int width = 4;
            // int height = 4;
            // float tileSize = 10.0f;
            //
            // var vertices = new Vector3[4 * (width * height)];
            // var uvs = new Vector2[4 * (width * height)];
            // var triangles = new int[6 * (width * height)];
            //
            // for (var i = 0; i < width; ++i)
            // {
            //     for (var j = 0; j < height; ++j)
            //     {
            //         var index = i * height + j;
            //         vertices[index * 4 + 0] = new Vector3(tileSize * i, tileSize * j);
            //         vertices[index * 4 + 1] = new Vector3(tileSize * i, tileSize * (j + 1));
            //         vertices[index * 4 + 2] = new Vector3(tileSize * (i + 1), tileSize * (j + 1));
            //         vertices[index * 4 + 3] = new Vector3(tileSize * (i + 1), tileSize * j);
            //         
            //         uvs[index * 4 + 0] = new Vector2(0, 0);
            //         uvs[index * 4 + 1] = new Vector2(0, 1);
            //         uvs[index * 4 + 2] = new Vector2(1, 1);
            //         uvs[index * 4 + 3] = new Vector2(1, 0);
            //
            //         triangles[index * 6 + 0] = index * 4 + 0;
            //         triangles[index * 6 + 1] = index * 4 + 1;
            //         triangles[index * 6 + 2] = index * 4 + 2;
            //         
            //         triangles[index * 6 + 3] = index * 4 + 0;
            //         triangles[index * 6 + 4] = index * 4 + 2;
            //         triangles[index * 6 + 5] = index * 4 + 3;
            //         
            //     }
            // }
            //
            // mesh.vertices = vertices;
            // mesh.uv = uvs;
            // mesh.triangles = triangles;
            //
            //
            // var meshFilter = GetComponent<MeshFilter>();
            // meshFilter.mesh = mesh;
            
            CreateAnimationMesh();
        }

        private void CreateAnimationMesh()
        {
            var mesh = new Mesh();

            var vertices = new Vector3[4 * 2];
            var uvs = new Vector2[4 * 2];
            var triangles = new int[6 * 2];
            
            // Render Body
            var bodySize = 70.0f;
            var bodyPosition = new Vector3(0, -50.0f);
            vertices[0] = bodyPosition + new Vector3(0, 0);
            vertices[1] = bodyPosition + new Vector3(0, bodySize);
            vertices[2] = bodyPosition + new Vector3(bodySize, bodySize);
            vertices[3] = bodyPosition + new Vector3(bodySize, 0);
            
            var bodyPixelsLowerLeft = new Vector2(0, 256);
            var bodyPixelsUpperRight = new Vector2(128, 384);

            var textureWidth = 512;
            var textureHeight = 512;
            
            uvs[0] = new Vector2(bodyPixelsLowerLeft.x / textureWidth, bodyPixelsLowerLeft.y / textureHeight);
            uvs[1] = new Vector2(bodyPixelsLowerLeft.x / textureWidth, bodyPixelsUpperRight.y / textureHeight);
            uvs[2] = new Vector2(bodyPixelsUpperRight.x / textureWidth, bodyPixelsUpperRight.y / textureHeight);
            uvs[3] = new Vector2(bodyPixelsUpperRight.x / textureWidth, bodyPixelsLowerLeft.y / textureHeight);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;

            // Render Head
            var headSize = 55.0f;
            var headPosition = new Vector3(6, -12.0f);
            vertices[4] = headPosition + new Vector3(0, 0);
            vertices[5] = headPosition + new Vector3(0, headSize);
            vertices[6] = headPosition + new Vector3(headSize, headSize);
            vertices[7] = headPosition + new Vector3(headSize, 0);
            
            var headPixelsLowerLeft = new Vector2(0, 384);
            var headPixelsUpperRight = new Vector2(128, 512);

            // var textureWidth = 512;
            // var textureHeight = 512;
            
            uvs[4] = new Vector2(headPixelsLowerLeft.x / textureWidth, headPixelsLowerLeft.y / textureHeight);
            uvs[5] = new Vector2(headPixelsLowerLeft.x / textureWidth, headPixelsUpperRight.y / textureHeight);
            uvs[6] = new Vector2(headPixelsUpperRight.x / textureWidth, headPixelsUpperRight.y / textureHeight);
            uvs[7] = new Vector2(headPixelsUpperRight.x / textureWidth, headPixelsLowerLeft.y / textureHeight);

            triangles[6] = 4;
            triangles[7] = 5;
            triangles[8] = 6;
            
            triangles[9] = 4;
            triangles[10] = 6;
            triangles[11] = 7;

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            
            
            var meshFilter = GetComponent<MeshFilter>();
            meshFilter.mesh = mesh;
        }
    }
}
