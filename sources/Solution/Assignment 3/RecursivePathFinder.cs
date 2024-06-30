using GXPEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/**
 Recursive BFS path finder
 */
class RecursivePathFinder : PathFinder
{

    private List<List<Node>> foundPaths = new List<List<Node>>();
    
    private List<Node> toDo = new List<Node>();
    private List<Node> done = new List<Node>();
    
    private List<Node> shortestPath;
    public RecursivePathFinder(NodeGraph pGraph) : base(pGraph) {}
    protected override List<Node> generate(Node pFrom, Node pTo)
    {
        // int smallestNodeCount = Int32.MaxValue;
        shortestPath = null;
        foundPaths.Clear();
        toDo.Clear();
        done.Clear();
        
        toDo.Add(pFrom);//added

        FindPath(pFrom, pTo);

        //Console.WriteLine("number of found paths: " + foundPaths.Count);
        
        // foreach (List<Node> path in foundPaths)
        // {
        //     if (path.Count < smallestNodeCount)
        //     {
        //         smallestNodeCount = path.Count;
        //         smallestPath = path;
        //     }
        //     
        // }
        
        //smallestPath = foundPaths.OrderBy(path => path.Count).FirstOrDefault();
        
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

    private void FindPath(Node pStart, Node pFinish, Node pVisited)
    {
        Console.WriteLine("start: " + pStart + ", finish:  " + pFinish + ", visited:  " + pVisited);
        if (pVisited != null)
        {
            pStart.path = pVisited.path;
            pStart.path.Add(pVisited);
        }
        
        if(pStart == pFinish)
        {
            Console.WriteLine("start = finish");
            List<Node> path = pStart.path;
            path.Add(pStart);
            foundPaths.Add(path);
        }
        else
        {
            //pStart.connections.Remove(pVisited);
            foreach (Node n in pStart.connections)
            {
                if (n != pVisited)
                {
                    Console.WriteLine("starting search for: "+n);
                    FindPath(n, pFinish, pStart);
                }
                else
                {
                    Console.WriteLine("finish search");
                    //continue;
                    return;
                }
            }
        }
    }

    
    // private void FindPath(Node pStart, Node pFinish, List<Node> currentPath)
    // {
    //     Console.WriteLine("start: " + pStart + ", finish:  " + pFinish);
    //     
    //     currentPath.Add(pStart);
    //
    //     if (pStart == pFinish)
    //     {
    //         Console.WriteLine("start = finish");
    //         foundPaths.Add(currentPath);
    //         currentPath.Clear();//
    //     }
    //     else
    //     {
    //         foreach (Node n in pStart.connections)
    //         {
    //             if (!currentPath.Contains(n))
    //             {
    //                 FindPath(n, pFinish, currentPath);
    //             }
    //         }
    //     }
    // }
    
    
    //main
    // private void FindPath(Node pStart, Node pFinish, List<Node> path)
    // {
    //     Console.WriteLine("start: " + pStart + ", finish:  " + pFinish);
    //
    //     done.Add(pStart);
    //     List<Node> newPath = new List<Node>();
    //     newPath = path;
    //     newPath.Add(pStart);
    //     foundPaths.Add(newPath);
    //     
    //     if (pStart == pFinish)
    //     {
    //         Console.WriteLine("start = finish");
    //         //pStart.path.Add(pStart);
    //         foundPaths.Add(newPath);
    //         foreach (Node n in path)
    //         {
    //             Console.WriteLine("path: " + n);
    //         }
    //         
    //         //toDo.Clear();
    //         //return;
    //     }
    //     // else
    //     // { 
    //         if (toDo.Count <= 1)//|| toDo.Count == 0)
    //         {
    //             foreach (Node node in done)
    //             {
    //                 if (node != pFinish)
    //                 {
    //                     foreach (Node connection in node.connections)
    //                     {
    //                         if (!done.Contains(connection) && !toDo.Contains(connection))
    //                         {
    //                             //connection.path = (node.path);
    //                             //connection.path.Add(node);
    //                             toDo.Add(connection);
    //                         }
    //                     }
    //                 }
    //             }
    //         }
    //         // if (toDo.Count > 0)
    //         // {
    //         //     Node nextNode = toDo[0];
    //         //     done.Add(nextNode);
    //         //     toDo.RemoveAt(0);
    //         //     // foreach (Node n in toDo)
    //         //     // {
    //         //     //     FindPath(n, pFinish);
    //         //     // }
    //         //     // Dequeue the next node to explore
    //         //     FindPath(nextNode, pFinish);
    //         // }
    //         
    //     //}
    //     
    //     if (toDo.Count > 1)
    //     {
    //         Node nextNode = toDo[0];
    //         //done.Add(nextNode);
    //         toDo.RemoveAt(0);
    //         // foreach (Node n in toDo)
    //         // {
    //         //     FindPath(n, pFinish);
    //         // }
    //         // Dequeue the next node to explore
    //         FindPath(nextNode, pFinish, newPath);
    //     }
    //    
    //     // if (toDo.Count > 0)
    //     // {
    //     //     foreach (Node n in toDo)
    //     //     {
    //     //         FindPath(n, pFinish);
    //     //     }
    //     // }
    // }
    
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
                if (shortestPath == null || path.Count < shortestPath.Count)
                {
                    Console.WriteLine("smaller");
                    shortestPath = new List<Node>(path);
                    return; //???? for dijkstra no return???
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



    
    
    // private void FindPath(Node pStart, Node pFinish)
    // {
    //     Console.WriteLine("start: " + pStart + ", finish: " + pFinish);
    //
    //     if (pStart == pFinish)
    //     {
    //         Console.WriteLine("start = finish");
    //         List<Node> path = new List<Node>(pStart.path) { pStart };
    //         foundPaths.Add(path);
    //     }
    //     else
    //     {
    //         done.Add(pStart);
    //         foreach (Node connection in pStart.connections)
    //         {
    //             if (!done.Contains(connection) && !toDo.Contains(connection))
    //             {
    //                 connection.path = new List<Node>(pStart.path) { pStart };  // Clone the current path and add current node
    //                 toDo.Add(connection);
    //             }
    //         }
    //
    //         while (toDo.Count > 0)
    //         {
    //             Node nextNode = toDo[0];
    //             toDo.RemoveAt(0);  // Dequeue the next node to explore
    //             FindPath(nextNode, pFinish);
    //         }
    //     }
    // }
    
    // private void FindPath(Node pStart, Node pFinish, List<Node> currentPath)
    // {
    //     Console.WriteLine("start: " + pStart + ", finish: " + pFinish);
    //
    //     currentPath.Add(pStart);
    //     done.Add(pStart);
    //
    //     if (pStart == pFinish)
    //     {
    //         Console.WriteLine("start = finish");
    //         foundPaths.Add(new List<Node>(currentPath));
    //         foreach (Node n in currentPath)
    //         {
    //             Console.WriteLine("path: " + n);
    //         }
    //     }
    //     else
    //     {
    //         foreach (Node connection in pStart.connections)
    //         {
    //             if (!done.Contains(connection))
    //             {
    //                 List<Node> newPath = new List<Node>(currentPath);
    //                 FindPath(connection, pFinish, newPath);
    //             }
    //         }
    //     }
    // }
}