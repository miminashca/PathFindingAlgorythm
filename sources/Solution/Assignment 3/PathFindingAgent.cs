using System;
using System.Collections.Generic;
using System.Drawing;
using GXPEngine;
using GXPEngine.Managers;

class PathFindingAgent : NodeGraphAgent
{
    //Current target to move towards
    private Node _target = null;
    private Node _finalTarget = null;
    private Node currentNodePosition = null;
    private Node lastVisitedNode = null;
    private PathFinder _pathFinder;
    private List<Node> path = null;
    public PathFindingAgent(NodeGraph pNodeGraph, PathFinder pPathFinder) : base(pNodeGraph)
    {
        SetOrigin(width / 2, height / 2);

        _pathFinder = pPathFinder;

        //position ourselves on a random node
        if (pNodeGraph.nodes.Count > 0)
        {
            Node randNode = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
            jumpToNode(randNode);
            currentNodePosition = randNode;
        }

        //listen to nodeClicks
        pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
    }

    protected virtual void onNodeClickHandler(Node pNode)
    {
        ////// not able to change route
        // if (path == null && pNode!=currentNodePosition)  // && !currentNodePosition.connections.Contains(pNode))
        // {
        //     path = _pathFinder.Generate(currentNodePosition, pNode);
        // }
        
        ////// if we dont want to find shortest path for a neighbour node
        // else if(_target==null && currentNodePosition.connections.Contains(pNode))
        // {
        //     _target = pNode;
        // }

        //able to change route
        if (pNode != currentNodePosition)
        {
            path = null;
            path = _pathFinder.Generate(currentNodePosition, pNode);
        }
    }
    
    protected override void Update()
    {
        // Console.WriteLine(currentNodePosition);
        if (path != null)
        {
            if (path.IndexOf(currentNodePosition) != path.Count - 1)
            {
                if (_target==null) _target = path[path.IndexOf(currentNodePosition) + 1];
            }
            else
            {
                path = null;
            }
        }
        
        //no target? Don't walk
        if (_target == null || !currentNodePosition.connections.Contains(_target))
        {
            _target = null;
            return;
        }
        
        //Move towards the target node, if we reached it, clear the target
        if (moveTowardsNode(_target))
        {
            lastVisitedNode = currentNodePosition;
            currentNodePosition = _target;
            _target = null;
        }
    }
}