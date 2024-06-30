using GXPEngine;

/**
 * This is an example subclass of the TiledView that just generates random tiles.
 */
class SampleTiledView : TiledView
{
	/**
	 * This constructor takes a dungeon but doesn't do anything with it, it is just an example of how
	 * to initialize the TiledView parameters with size and scale data from the dungeon,
	 * make sure you understand what is happening here before you continue.
	 */
	public SampleTiledView(Dungeon pDungeon, TileType pDefaultTileType) : base(pDungeon.size.Width, pDungeon.size.Height, (int)pDungeon.scale, pDefaultTileType)
	{
	}

	/**
	 * Fill the tileview with random data instead.
	 * In your subclass, you should set the tiletype correctly based on the provided dungeon contents.
	 */
	protected override void generate()
	{
		for (int y = 0; y < rows; y++)
		{
			for (int x = 0; x < columns; x++)
			{
				TileType tileType = TileType.GROUND;

				if (Utils.Random(0, 2) == 1)
				{
					tileType = TileType.WALL;
				}
				
				SetTileType(x, y, tileType);
                
			}
		}
	}
}

