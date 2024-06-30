using System;
using System.Diagnostics;
using GXPEngine;
using System.Drawing;

/**
 * An example of a dungeon implementation.
 * This implementation places two rooms manually but your implementation has to do it procedurally.
 */
class SufficientDungeon : Dungeon
{
    public SufficientDungeon(Size pSize) : base(pSize) {}

    Random random = new Random();
    enum Direction
    {
        Horizontal,
        Vertical
    }
    Direction RandomDirection()
    {
        if (random.Next(0, 2) == 0)
        {
            return Direction.Horizontal;
        }
        else
        {
            return Direction.Vertical;
        }
    }
    Direction InvertDirection(Direction direction)
    {
        if (direction == Direction.Horizontal)
        {
            return Direction.Vertical;
        }
        else
        {
            return Direction.Horizontal;
        }
    }
    /**
     * This method overrides the super class generate method to implement a two-room dungeon with a single door.
     * The good news is, it's big enough to house an Ogre and his ugly children, the bad news your implementation
     * should generate the dungeon procedurally, respecting the pMinimumRoomSize.
     *
     * Hints/tips:
     * - start by generating random rooms in your own Dungeon class and placing random doors.
     * - playing/experiment freely is the key to all success
     * - this problem can be solved both iteratively or recursively
     **/
    protected override void generate(int pMinimumRoomSize)
    {
        int minRoomSize = pMinimumRoomSize;
        rooms.Add(new Room(new Rectangle(0, 0, size.Width, size.Height)));
        
        // rooms.Add(new Room(new Rectangle(0, 0, size.Width, size.Height/2 +1)));
        // //right room from half of screen to the end
        // rooms.Add(new Room(new Rectangle(0, size.Height/2, size.Width, size.Height/2)));
        // foreach (Room r in rooms)
        // {
        //     AddDoor(r);
        // }
        // foreach (Door d in doors)
        // {
        //     Console.WriteLine(d.location);
        // }
        
        //Direction randAxis = RandomDirection();
        
        for(int i = 0; i<rooms.Count; i++)
        {
            Direction randAxis = RandomDirection();
           
            for (int j = 0; j <= i; j++)
            {
                Room r = rooms[j];
                randAxis = InvertDirection(randAxis);
                
                //find a random
                if (randAxis == Direction.Vertical && r.area.Width < minRoomSize * 2)
                {
                    randAxis = InvertDirection(randAxis);
                }
                if(randAxis == Direction.Horizontal && r.area.Height < minRoomSize * 2)
                {
                    randAxis = InvertDirection(randAxis);
                }
                //Console.WriteLine(randAxis);
                
                //divide rooms
                Divide(randAxis, r, minRoomSize);
                
            }
        }

        foreach (Room r in rooms)
        {
            AddDoor(r);
            for (int i = 0; i < doors.Count; i++)
            {
                Door door = doors[i];
                foreach (Room room in rooms)//
                {
                    foreach (Point corner in room.corners)
                    {
                        if (door.location == corner)
                        {
                            doors.Remove(door);
                            doorLocations.Remove(door.location);
                        }
                    }
                }
            }
            
            //extra check for doors in roomDoors
            // do
            // {
            //    AddDoor(r);
            // }
            // while()
        }

        foreach (Door d in doors)
        {
            Console.WriteLine(d);
        }
    }

    private void Divide(Direction pRandAxis, Room pR, int pMinRoomSize)
    {
        Direction randAxis = pRandAxis;
        Room r = pR;
        int minRoomSize = pMinRoomSize;

        if (randAxis == Direction.Vertical)
            if (r.area.Width >= minRoomSize * 2)
            {
                int newWidth = random.Next(minRoomSize, r.area.Width - minRoomSize + 1);
                if (newWidth >= minRoomSize && r.area.Width - newWidth + 1 >= minRoomSize)
                {
                    rooms.Remove(r);
                    rooms.Add(new Room(new Rectangle(r.area.X, r.area.Y, newWidth, r.area.Height)));
                    rooms.Add(new Room(new Rectangle(r.area.X + newWidth - 1, r.area.Y, r.area.Width - newWidth + 1, r.area.Height)));
                }
            }

        if (randAxis == Direction.Horizontal)
            if (r.area.Height >= minRoomSize * 2)
            {
                int newHeight = random.Next(minRoomSize, r.area.Height - minRoomSize + 1);
                if (newHeight >= minRoomSize && r.area.Height - newHeight + 1 >= minRoomSize)
                {
                    rooms.Remove(r);
                    rooms.Add(new Room(new Rectangle(r.area.X, r.area.Y, r.area.Width, newHeight)));
                    rooms.Add(new Room(new Rectangle(r.area.X, r.area.Y+newHeight - 1, r.area.Width, r.area.Height - newHeight + 1)));
                }
            }
    }

    private void AddDoor(Room r)
    {
        Point c1;
        Point c2;
        for(int i = 0; i<r.corners.Count-1; i++)
        {
            c1 = r.corners[i];
            c2 = r.corners[i + 1];
            bool noDoor = (c1.X == 0 && c2.X == 0) || (c1.Y == 0 && c2.Y == 0) || (c1.X + 1 == size.Width && c2.X + 1 == size.Width) ||
                          (c1.Y + 1 == size.Height && c2.Y + 1 == size.Height);

            for (int k = Math.Min((int)c1.Y, (int)c2.Y); k <= Math.Max((int)c1.Y, (int)c2.Y); k++)
            {
                for (int j = Math.Min((int)c1.X, (int)c2.X); j <= Math.Max((int)c1.X, (int)c2.X); j++)
                {
                    if (doorLocations.Contains(new Point(j, k)))
                    {
                        //Console.WriteLine("door exists");
                        noDoor = true;
                    }
                }
            }

            if (!noDoor)
            {
                Point location;
                if (c1.X == c2.X)
                {
                    location = new Point((int)c1.X,
                        random.Next(Math.Min((int)c1.Y, (int)c2.Y) + 2, Math.Max((int)c1.Y, (int)c2.Y) - 2));
                }
                else if (c1.Y == c2.Y)
                {
                    location = new Point(
                        random.Next(Math.Min((int)c1.X, (int)c2.X) + 2, Math.Max((int)c1.X, (int)c2.X) - 2),
                        (int)c1.Y);
                }
                
                Door door = new Door(location);
                door.roomA = r;
                foreach (Room r2 in rooms)
                {
                    if (RoomContainsDoor(r2, door))
                    {
                        if(r2!=door.roomA) door.roomB = r2;
                    }
                }
                doors.Add(door);
                doorLocations.Add(door.location);
            }
        }
    }

    private bool RoomContainsDoor(Room r, Door d)
    {
        return (d.location.X >= r.corners[0].X && d.location.Y >= r.corners[0].Y && d.location.X <= r.corners[2].X &&
                d.location.Y <= r.corners[2].Y);
    }
}