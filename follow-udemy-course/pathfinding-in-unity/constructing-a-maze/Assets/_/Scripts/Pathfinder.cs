﻿namespace GiantCroissant.FollowUdemyCourse.PathfindingInUnity
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Pathfinder : MonoBehaviour
    {
        private Node _startNode;
        private Node _goalNode;

        private Graph _graph;
        private GraphView _graphView;

        private Queue<Node> _frontierNodes;
        private List<Node> _exploredNodes;
        private List<Node> _pathNodes;
        
        public Color startColor = Color.green;
        public Color goalColor = Color.red;
        public Color frontierColor = Color.magenta;
        public Color exploredColor = Color.gray;
        public Color pathColor = Color.cyan;
        
        public Color arrowColor = new Color(0.85f, 0.85f, 0.85f, 1.0f);
        public Color highlightColor = new Color(1.0f, 1.0f, 0.5f, 1.0f);

        public bool isComplete = false;
        private int _iterations = 0;

        public void Construct(Graph graph, GraphView graphView, Node start, Node goal)
        {
            if (start == null || goal == null || graph == null || graphView == null)
            {
                Debug.LogWarning($"Missing");
                return;
            }

            if (start.nodeType == NodeType.Blocked || goal.nodeType == NodeType.Blocked)
            {
                Debug.LogWarning($"Start and Goal can not be blocked");
                return;
            }

            _graph = graph;
            _graphView = graphView;
            _startNode = start;
            _goalNode = goal;

            ShowColors(graphView, start, goal);

            _frontierNodes = new Queue<Node>();
            _frontierNodes.Enqueue(start);
            _exploredNodes = new List<Node>();
            _pathNodes = new List<Node>();

            for (var x = 0; x < graph.Width; ++x)
            {
                for (var y = 0; y < graph.Height; ++y)
                {
                    _graph.nodes[x, y].Reset();
                }
            }

            isComplete = false;
            _iterations = 0;
        }

        private void ShowColors()
        {
            ShowColors(_graphView, _startNode, _goalNode);
        }

        private void ShowColors(GraphView graphView, Node start, Node goal)
        {
            if (graphView == null || start == null || goal == null)
            {
                Debug.LogWarning($"GraphView, start or goal is null");
                return;
            }

            if (_frontierNodes != null)
            {
                graphView.ColorNodes(_frontierNodes.ToList(), frontierColor);
            }

            if (_exploredNodes != null)
            {
                graphView.ColorNodes(_exploredNodes, exploredColor);
            }

            if (_pathNodes != null && _pathNodes.Count > 0)
            {
                graphView.ColorNodes(_pathNodes, pathColor);
            }
            
            var startNodeView = graphView.nodeViews[start.xIndex, start.yIndex];
            if (startNodeView != null)
            {
                startNodeView.ColorNode(startColor);
            }

            var goalNodeView = graphView.nodeViews[goal.xIndex, goal.yIndex];
            if (goalNodeView != null)
            {
                goalNodeView.ColorNode(goalColor);
            }
        }

        public IEnumerator SearchRoutine(float timeStep = 0.1f)
        {
            yield return null;

            while (!isComplete)
            {
                if (_frontierNodes.Count > 0)
                {
                    var currentNode = _frontierNodes.Dequeue();
                    ++_iterations;

                    if (!_exploredNodes.Contains(currentNode))
                    {
                        _exploredNodes.Add(currentNode);
                    }
                    
                    ExpandFrontier(currentNode);

                    if (_frontierNodes.Contains(_goalNode))
                    {
                        _pathNodes = GetPathNodes(_goalNode);
                    }
                    ShowColors();
                    if (_graphView != null)
                    {
                        _graphView.ShowNodeArrows(_frontierNodes.ToList(), arrowColor);

                        if (_frontierNodes.Contains(_goalNode))
                        {
                            _graphView.ShowNodeArrows(_pathNodes, highlightColor);
                        }
                    }
                    
                    yield return  new WaitForSeconds(timeStep);
                }
                else
                {
                    isComplete = true;
                }
            }
        }

        private void ExpandFrontier(Node node)
        {
            if (node != null)
            {
                for (var i = 0; i < node.neighbors.Count; ++i)
                {
                    if (!_exploredNodes.Contains(node.neighbors[i]) && 
                        !_frontierNodes.Contains(node.neighbors[i]))
                    {
                        node.neighbors[i].previousNode = node;
                        _frontierNodes.Enqueue(node.neighbors[i]);
                    }
                }
            }
        }

        private List<Node> GetPathNodes(Node endNode)
        {
            var path = new List<Node>();
            if (endNode == null)
            {
                return path;
            }
            
            path.Add(endNode);

            var currentNode = endNode.previousNode;
            while (currentNode != null)
            {
                path.Insert(0, currentNode);
                currentNode = currentNode.previousNode;
            }

            return path;
        }
    }
}
