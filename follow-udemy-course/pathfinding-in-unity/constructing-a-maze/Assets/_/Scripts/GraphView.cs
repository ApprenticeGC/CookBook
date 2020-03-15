namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Graph))]
    public class GraphView : MonoBehaviour
    {
        public GameObject nodeViewPrefab;
        
        public Color baseColor = Color.white;
        public Color wallColor = Color.black;

        public void Construct(Graph graph)
        {
            if (graph == null)
            {
                Debug.LogWarning($"No graph");
                return;
            }

            foreach (var node in graph.nodes)
            {
                var instance = Instantiate(nodeViewPrefab, Vector3.zero, Quaternion.identity);
                var nodeView = instance.GetComponent<NodeView>();

                if (nodeView != null)
                {
                    nodeView.Construct(node);

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
    }
}
