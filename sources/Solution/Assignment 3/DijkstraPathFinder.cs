using GXPEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/**
 Recursive BFS path finder
 */
class DijkstraPathFinder : RecursivePathFinder
{
    private List<Node> shortestPath;
    public DijkstraPathFinder(NodeGraph pGraph) : base(pGraph) {}
    
    //BFS
    public void FindPath(Node pStart, Node pFinish)
    {
        List<List<Node>> currentLevelPaths = new List<List<Node>>();
        currentLevelPaths.Add(new List<Node> { pStart });

        FindPathRecursive(currentLevelPaths, pFinish);
    }

    private void FindPathRecursive(List<List<Node>> currentLevelPaths, Node pFinish)
    {
        if (currentLevelPaths.Count == 0) return;

        List<List<Node>> nextLevelPaths = new List<List<Node>>();

        foreach (List<Node> path in currentLevelPaths)
        {
            Node currentNode = path[path.Count - 1];

            if (currentNode == pFinish)
            {
                //dijkstra
                if (shortestPath == null || PathLength(path, path[0], 0) < PathLength(shortestPath, shortestPath[0], 0))
                {
                    Console.WriteLine("smaller");
                    shortestPath = new List<Node>(path);
                }
                Console.WriteLine("bigger");
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
        }

        FindPathRecursive(nextLevelPaths, pFinish);
    }

    private float PathLength(List<Node> path, Node currentNode, float currentLength)
    {
        if (path.IndexOf(currentNode) != path.Count-1)
        {
            Node nextNode = path[path.IndexOf(currentNode) + 1];
            Vec2 first = new Vec2(nextNode.location.X, nextNode.location.Y);
            Vec2 second = new Vec2(currentNode.location.X, currentNode.location.Y);
            currentLength += (first.DistanceTo(second));
            PathLength(path, nextNode, currentLength);
        }
        else
        {
            return currentLength;
        }

        return 0;
    }
}