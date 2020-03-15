namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Graph))]
    public class GraphView : MonoBehaviour
    {
        public GameObject nodeViewPrefab;
        public NodeView[,] nodeViews;
        
        public Color baseColor = Color.white;
        public Color wallColor = Color.black;

        public void Construct(Graph graph)
        {
            if (graph == null)
            {
                Debug.LogWarning($"No graph");
                return;
            }

            nodeViews = new NodeView[graph.Width, graph.Height];
            foreach (var node in graph.nodes)
            {
                var instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
                var nodeView = instance.GetComponent<NodeView>();

                if (nodeView != null)
                {
                    nodeView.Construct(node);

                    nodeViews[node.xIndex, node.yIndex] = nodeView;
                    
                    if (node.nodeType == NodeType.Blocked)
                    {
                        nodeView.ColorNode(wallColor);
                    }
                    else if (node.nodeType == NodeType.Open)
                    {
                        nodeView.ColorNode(baseColor);
                    }
                }
            }
        }

        public void ColorNodes(List<Node> nodes, Color color)
        {
            foreach (var node in nodes)
            {
                if (node != null)
                {
                    var nodeView = nodeViews[node.xIndex, node.yIndex];
                    if (nodeView != null)
                    {
                        nodeView.ColorNode(color);
                    }
                }
            }
        }

        public void ShowNodeArrow(Node node, Color color)
        {
            if (node != null)
            {
                var nodeView = nodeViews[node.xIndex, node.yIndex];
                if (nodeView != null)
                {
                    nodeView.ShowArrow(color);
                }
            }
        }

        public void ShowNodeArrows(List<Node> nodes, Color color)
        {
            foreach (var node in nodes)
            {
                ShowNodeArrow(node, color);
            }
        }
    }
}
