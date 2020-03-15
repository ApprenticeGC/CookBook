namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class GraphUtility
    {
        public static readonly Vector2[] AllDirections =
        {
            new Vector2(0.0f, 1.0f),
            new Vector2(1.0f, 1.0f),
            new Vector2(1.0f, 0.0f),
            new Vector2(1.0f, -1.0f),
            new Vector2(0.0f, -1.0f),
            new Vector2(-1.0f, 1.0f),
            new Vector2(-1.0f, 0.0f),
            new Vector2(-1.0f, 1.0f)
        };

        public static bool IsWithinBounds(int x, int y, int width, int height)
        {
            return (x >= 0 && y >= 0 && x < width && y < height);
        }

        public static List<Node> GetNeighbors(int x, int y, int width, int height, Node[,] nodeArray, Vector2[] directions)
        {
            var neighborNodes = new List<Node>();

            foreach (var dir in directions)
            {
                var newX = x + (int) dir.x;
                var newY = y + (int) dir.y;

                if (IsWithinBounds(newX, newY, width, height) && nodeArray[newX, newY] != null &&
                    nodeArray[newX, newY].nodeType != NodeType.Blocked)
                {
                    neighborNodes.Add(nodeArray[newX, newY]);
                }
            }

            return neighborNodes;
        }
    }

    public class Graph : MonoBehaviour
    {
        public Node[,] nodes;

        public List<Node> walls = new List<Node>();

        private int[,] _mapData;
        private int _width;
        private int _height;

        public void Construct(int[,] mapData)
        {
            _mapData = mapData;
            _width = mapData.GetLength(0);
            _height = mapData.GetLength(1);

            nodes = new Node[_width, _height];
            
            for (var y = 0; y < _height; ++y)
            {
                for (var x = 0; x < _width; ++x)
                {
                    var nodeType = (NodeType) mapData[x, y];
                    var newNode = new Node(x, y, nodeType);

                    nodes[x, y] = newNode;
                    
                    newNode.position = new Vector3(x, 0, y);
                    if (nodeType == NodeType.Blocked)
                    {
                        walls.Add(newNode);
                    }
                }
            }

            for (var y = 0; y < _height; ++y)
            {
                for (var x = 0; x < _width; ++x)
                {
                    if (nodes[x, y].nodeType != NodeType.Blocked)
                    {
                        nodes[x, y].neighbors = GetNeighbors(x, y);
                    }
                }
            }
        }

        private List<Node> GetNeighbors(int x, int y)
        {
            return GraphUtility.GetNeighbors(x, y, _width, _height, nodes, GraphUtility.AllDirections);
        }
    }
}
