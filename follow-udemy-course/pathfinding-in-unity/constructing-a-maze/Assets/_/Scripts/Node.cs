namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System.Collections.Generic;
    using UnityEngine;

    public enum NodeType : int
    {
        Open = 0,
        Blocked = 1
    }
    
    public class Node
    {
        public NodeType nodeType = NodeType.Open;

        public int xIndex = -1;
        public int yIndex = -1;

        public Vector3 position;
        
        public List<Node> neighbors = new List<Node>();

        public Node previousNode = null;

        public Node(int xIndex, int yIndex, NodeType nodeType)
        {
            this.xIndex = xIndex;
            this.yIndex = yIndex;
            this.nodeType = nodeType;
        }

        public void Reset()
        {
            previousNode = null;
        }
    }
}
