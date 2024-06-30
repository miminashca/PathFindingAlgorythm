using System;
using System.Drawing;
using GXPEngine;
using GXPEngine.Managers;

class OnGraphWayPointAgent : NodeGraphAgent
{
    //Current target to move towards
    private Node _target = null;
    private Node _finalTarget = null;
    private Node currentNodePosition = null;
    private Node lastVisitedNode = null;
    public OnGraphWayPointAgent(NodeGraph pNodeGraph) : base(pNodeGraph)
    {
        SetOrigin(width / 2, height / 2);

        //position ourselves on a random node
        if (pNodeGraph.nodes.Count > 0)
        {
            Node randNode = pNodeGraph.nodes[Utils.Random(0, pNodeGraph.nodes.Count)];
            jumpToNode(randNode);
            currentNodePosition = randNode;
        }

        //listen to nodeclicks
        // if (_target == null)
        // {
        //     pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
        // }
        // else if(_target != null && currentNodePosition == _target)
        // {
        //     pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
        // }
        pNodeGraph.OnNodeLeftClicked += onNodeClickHandler;
    }

    protected virtual void onNodeClickHandler(Node pNode)
    {
        if (_target == null)
        {
            _target = pNode;
        }
        else
        {
            Console.WriteLine("target: " + _target.id);
            if (currentNodePosition != null) Console.WriteLine(currentNodePosition.id);
        }
    }
    
    protected override void Update()
    {
        //if(lastVisitedNode!=null) Console.WriteLine(lastVisitedNode.id);
        
        // if (_target != null && !currentNodePosition.connections.Contains(_target) && _target!=currentNodePosition)
        // {
        //     _finalTarget = _target;
        //     _target = currentNodePosition.connections[Utils.Random(0, currentNodePosition.connections.Count)];
        // }
        //
        // if (_finalTarget != null)
        // {
        //     if (_target == null && !currentNodePosition.connections.Contains(_finalTarget))
        //     {
        //         do
        //         {
        //             _target = currentNodePosition.connections[Utils.Random(0, currentNodePosition.connections.Count)];
        //         } 
        //         while (_target == lastVisitedNode && currentNodePosition.connections.Count>1);
        //     }
        //     else if(currentNodePosition.connections.Contains(_finalTarget))
        //     {
        //         _target = _finalTarget;
        //         _finalTarget = null;
        //     } 
        // }
        
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
