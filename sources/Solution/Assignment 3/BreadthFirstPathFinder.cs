using GXPEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/**
 Iterative BFS path finder
 */
class BreadthFirstPathFinder : PathFinder
{
    public List<Node> shortestPath;
    public BreadthFirstPathFinder(NodeGraph pGraph) : base(pGraph) {}
    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        shortestPath = null;

        FindPath(pFrom, pTo);
        
        if (shortestPath != null)
        {
            Console.WriteLine("smallest path: " + shortestPath.Count);
            return shortestPath; 
        }
        else
        {
            return null;
        }
    }
    
    public void FindPath(Node pStart, Node pFinish)
    {
        List<List<Node>> currentLevelPaths = new List<List<Node>>();
        currentLevelPaths.Add(new List<Node> { pStart });

        FindPathIterative(currentLevelPaths, pFinish);
    }

    private void FindPathIterative(List<List<Node>> currentLevelPaths, Node pFinish)
    {
        if (currentLevelPaths.Count == 0) return;

        List<List<Node>> nextLevelPaths = new List<List<Node>>();

        for (int j = 0; j < currentLevelPaths.Count; j++)
        {
            List<Node> path = currentLevelPaths[j];
            Node currentNode = path[path.Count - 1];

            if (currentNode == pFinish)
            {
                if (shortestPath == null || path.Count < shortestPath.Count)
                {
                    Console.WriteLine("smaller");
                    shortestPath = new List<Node>(path);
                    return;
                }
                else { Console.WriteLine("bigger");}
                continue;
            }

            foreach (Node connection in currentNode.connections)
            {
                if (!path.Contains(connection))
                {
                    List<Node> newPath = new List<Node>(path) { connection };
                    nextLevelPaths.Add(newPath);
                }
            }
            
            if (currentLevelPaths.IndexOf(path) == currentLevelPaths.Count - 1)
            {
                foreach (List<Node> p in nextLevelPaths)
                {
                    currentLevelPaths.Add(p);
                }
            }
        }
    }
}