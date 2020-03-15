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
            }
        }

    }
}
