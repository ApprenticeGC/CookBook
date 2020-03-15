namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class MeshUtility
    {
        public static readonly Mesh UnitQuadMesh = GenerateMesh(1, 1);
        
        public static Mesh GenerateMesh(float width, float height)
        {
            var vertices = new Vector3[4];
            var uv = new Vector2[4];
            var triangles = new int[6];
            
            /*
             * 0, 0
             * 0, 1
             * 1, 1
             * 1, 0
             */

            var halfWidth = width * 0.5f;
            var halfHeight = height * 0.5f;
            
            vertices[0] = new Vector3(-halfWidth, 0, -halfHeight);
            vertices[1] = new Vector3(-halfWidth, 0, halfHeight);
            vertices[2] = new Vector3( halfWidth, 0,halfHeight);
            vertices[3] = new Vector3( halfWidth, 0, -halfHeight);
            
            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(0, 1);
            uv[2] = new Vector2(1, 1);
            uv[3] = new Vector2(1, 0);

            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 3;
            
            triangles[3] = 1;
            triangles[4] = 2;
            triangles[5] = 3;

            var mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            return mesh;
        }
        
        public static Mesh GenerateGrid(int xSize, int ySize)
        {
        
            var mesh = new Mesh();
            mesh.name = "Procedural Grid";
        
            //calculate number of vertices
            var vertices = new Vector3[(xSize + 1) * (ySize + 1)];
        
            Vector2[] uv = new Vector2[vertices.Length];
        
            //positioning vertices and providing Uv coordinates
            for (int i = 0, y = 0; y <= ySize; y++)
            {
                for (int x = 0; x <= xSize; x++, i++)
                {
                    vertices[i] = new Vector3(x, y);
                    uv[i] = new Vector2((float)x / xSize,(float) y / ySize);
                }
            }
            mesh.vertices = vertices;
            mesh.uv = uv;
        
            int[] triangles = new int[xSize * ySize * 6];
            for (int ti = 0, vi = 0, y = 0; y < ySize; y++, vi++)
            {
                for (int x = 0; x < xSize; x++, ti += 6, vi++)
                {
                    triangles[ti] = vi;
                    triangles[ti + 3] = triangles[ti + 2] = vi + 1;
                    triangles[ti + 4] = triangles[ti + 1] = vi + xSize + 1;
                    triangles[ti + 5] = vi + xSize + 2;
                }
            }
            mesh.triangles = triangles;
        
            mesh.RecalculateNormals();
        
            return mesh;
        }
    }

    public class NodeView : MonoBehaviour
    {
        public GameObject tile;

        [Range(0, 0.5f)]
        public float borderSize = 0.15f;

        public void Construct(Node node)
        {
            if (tile != null)
            {
                gameObject.name = $"Node ({node.xIndex}, {node.yIndex})";
                gameObject.transform.position = node.position;
                tile.transform.localScale = new Vector3(1.0f - borderSize, 1.0f, 1.0f - borderSize);

                ReplaceMeshWith();
            }
        }

        private void ReplaceMeshWith()
        {
            var meshFilter = tile.GetComponent<MeshFilter>();
            if (meshFilter != null)
            {
                // var mesh = MeshUtility.GenerateMesh(1, 1);
                var mesh = MeshUtility.UnitQuadMesh;
                meshFilter.mesh = mesh;
            }
        }

        void ColorNode(Color color, GameObject inGO)
        {
            if (inGO != null)
            {
                var goRenderer = inGO.GetComponent<Renderer>();

                if (goRenderer != null)
                {
                    goRenderer.material.color = color;
                }
            }
        }

        public void ColorNode(Color color)
        {
            ColorNode(color, tile);
        }
    }
}
