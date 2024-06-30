using System;
using System.Drawing;
using GXPEngine;

/**
 * This is an example subclass of the TiledView that just generates random tiles.
 */
class TiledDungeonView : TiledView
{
    /**
     * This constructor takes a dungeon but doesn't do anything with it, it is just an example of how
     * to initialize the TiledView parameters with size and scale data from the dungeon,
     * make sure you understand what is happening here before you continue.
     */

    private Dungeon _dungeon;
    public TiledDungeonView(Dungeon pDungeon, TileType pDefaultTileType) : base(pDungeon.size.Width, pDungeon.size.Height, (int)pDungeon.scale, pDefaultTileType)
    {
        _dungeon = pDungeon;
    }
    
    protected override void generate()
    {
        TileType tileType = TileType.WALL;
        
        foreach (Room r in _dungeon.rooms)
        {
            //Console.WriteLine(r);
            Console.WriteLine(r.area);
            for (int x = 0; x < r.area.Width; x++)
            {
                for (int y = 0; y < r.area.Height; y++)
                {
                    if ((y == 0 || x == 0 || y == r.area.Height-1 || x == r.area.Width-1)) tileType = TileType.WALL;
                    else
                    {
                        tileType = TileType.GROUND;
                    }
                    SetTileType((r.area.X + x), (r.area.Y + y), tileType);
                }
            }
        }
        
        foreach (Door d in _dungeon.doors)
        {
            SetTileType(d.location.X, d.location.Y, TileType.GROUND);          
        }
    }
}