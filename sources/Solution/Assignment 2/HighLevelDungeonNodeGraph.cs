using System;
using System.Diagnostics;
using System.Drawing;

class HighLevelDungeonNodeGraph : SampleDungeonNodeGraph
{
    protected Dungeon _dungeon;

    public HighLevelDungeonNodeGraph(Dungeon pDungeon) : base(pDungeon)
    {
        Debug.Assert(pDungeon != null, "Please pass in a dungeon.");
        _dungeon = pDungeon;
    }
    
    protected override void generate ()
    {
        foreach (Door d in _dungeon.doors)
        {
            //add door node
            Node doorNode = new Node(getDoorCenter(d));
            nodes.Add(doorNode);
            //add nodes for rooms this door connects
            if (d.roomA.roomNode == null)
            {
                Node roomNodeA = new Node(getRoomCenter(d.roomA));
                nodes.Add(roomNodeA);
                d.roomA.roomNode = roomNodeA;
                //add connections
                AddConnection(doorNode, roomNodeA);
            }
            else
            {
                AddConnection(doorNode, d.roomA.roomNode);
            }
            
            if (d.roomB.roomNode == null)
            {
                Node roomNodeB = new Node(getRoomCenter(d.roomB));
                nodes.Add(roomNodeB);
                d.roomB.roomNode = roomNodeB;
                //add connections
                AddConnection(doorNode, roomNodeB);
            }
            else
            {
                AddConnection(doorNode, d.roomB.roomNode);
            }
            
        }
        
    }
}
