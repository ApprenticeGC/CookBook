namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DemoController : MonoBehaviour
    {
        public MapData mapData;
        public Graph graph;

        public Pathfinder pathfinder;
        public int startX = 0;
        public int startY = 0;

        public int goalX = 15;
        public int goalY = 1;

        public float timeStep = 0.1f;
        
        private void Start()
        {
            if (mapData != null && graph != null)
            {
                var mapInstance = mapData.MakeMap();
                graph.Construct(mapInstance);

                var graphView = graph.GetComponent<GraphView>();

                if (graphView != null)
                {
                    graphView.Construct(graph);
                }

                if (GraphUtility.IsWithinBounds(startX, startY, graph.Width, graph.Height) &&
                    GraphUtility.IsWithinBounds(goalX, goalY, graph.Width, graph.Height) &&
                    pathfinder != null)
                {
                    Debug.Log($"Passing condition");
                    var startNode = graph.nodes[startX, startY];
                    var goalNode = graph.nodes[goalX, goalY];
                    pathfinder.Construct(graph, graphView, startNode, goalNode);

                    StartCoroutine(pathfinder.SearchRoutine(timeStep));
                }
            }
        }
    }
}
