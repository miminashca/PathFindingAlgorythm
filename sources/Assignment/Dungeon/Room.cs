using System.Collections.Generic;
using System.Drawing;
using GXPEngine;

/**
 * This class represents (the data for) a Room, at this moment only a rectangle in the dungeon.
 */
class Room
{
	public Rectangle area;
	public List<Point> corners;
	public Node roomNode = null;

	public Room (Rectangle pArea)
	{
		area = pArea;
		corners = new List<Point>();
		corners.Add(new Point(area.X, area.Y));
		corners.Add(new Point(area.X + area.Width - 1, area.Y));
		corners.Add(new Point(area.X + area.Width - 1, area.Y + area.Height - 1));
		corners.Add(new Point(area.X, area.Y + area.Height - 1));
		corners.Add(new Point(area.X, area.Y)); //duplicate first corner for easier iteration

		// roomDoors = new List<Door>();
	}

	//TODO: Implement a toString method for debugging?
	//Return information about the type of object and it's data
	//eg Room: (x, y, width, height)
	public override string ToString()
	{
		return "Room: " + area.Location;
	}

}
