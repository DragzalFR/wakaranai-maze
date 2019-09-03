using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct MazeModel
{
    //public List<WallModel> MazeWalls { get; private set; }
    public WallModel[] MazeWalls { get; private set; }

    public MazeModel(in TileModel[] tiles)
    {
        // From Wiki : Number of wall in a perfect labyrinthe is e = (m-1)(n-1) 
        // here m = n = boardSize so number of wall is (boardSize - 1)^2
        //MazeWalls = new List<WallModel>((MapModel.BOARD_SIZE - 1) * (MapModel.BOARD_SIZE - 1));
        MazeWalls = new WallModel[(MapModel.BOARD_SIZE - 1) * (MapModel.BOARD_SIZE - 1)];

        GenerateMaze(tiles);
    }

    // https://fr.wikipedia.org/wiki/Modélisation_mathématique_de_labyrinthe 
    // Maze generation following wikipedia "Fusion aléatoire de chemins" algorithm.
    private void GenerateMaze(in TileModel[] tiles)
    {
        List<WallModel> originWalls = CreateWalls(in tiles);

        List<List<TileModel>> allRooms = new List<List<TileModel>>();

        // Used to add wall at the last index of MazeWalls. 
        int wallCount = 0;

        // From Wiki : We get an unique path when number of wall removed reach m*n-1
        // Here mn-1 => boardSize * boardSize - 1
        int numberOfWallToRemove = MapModel.BOARD_SIZE * MapModel.BOARD_SIZE - 1;
        for (var k = 0; k < numberOfWallToRemove; k++)
        {
            while (originWalls.Count > 0)
            {
                int r = Random.Range(0, originWalls.Count);

                WallModel wall = originWalls[r];

                originWalls.RemoveAt(r);
                if (IsSameRoom(allRooms, wall.Tile1, wall.Tile2))
                {
                    MazeWalls[wallCount] = wall;
                    wallCount++;
                }
                else
                {
                    ConcatRooms(ref allRooms, wall);
                    break;
                }
            }
        }

        foreach(var wall in originWalls)
        {
            MazeWalls[wallCount] = wall;
            wallCount++;
        }
    }

    // Return true if a wall exist at the given position, 
    // Else return false. 
    public bool IsWallExistAt(in Vector2 position)
    {
        foreach (var wall in MazeWalls)
        {
            if (position == wall.GetPosition())
            {
                return true;
            }
        }

        return false;
    }



    // Return true if tile1 and tile2 are in the same room, 
    // Else return false. 
    private bool IsSameRoom(in List<List<TileModel>> rooms, in TileModel tile1, in TileModel tile2)
    {
        foreach (var room in rooms)
        {
            if (room.Contains(tile1) && room.Contains(tile2))
            {
                return true;
            }
        }

        return false;
    }

    // Modify allRooms to concat 2 rooms separated by a wall. 
    private void ConcatRooms(ref List<List<TileModel>> allRooms, in WallModel wall)
    {
        // if it's already the same room, we do nothing and leave. 
        if (IsSameRoom(allRooms, wall.Tile1, wall.Tile2))
        {
            return;
        }

        List<TileModel> room1 = default;
        List<TileModel> room2 = default;

        // We search into rooms to set the value of room1 and room2. 
        // room1 contains tile1 and room2 contains tile2. 
        foreach(var room in allRooms)
        {
            if (room.Contains(wall.Tile1))
            {
                room1 = room;
            }

            if (room.Contains(wall.Tile2))
            {
                room2 = room;
            }
        }

        // If no room contain our tile we initialise a new room.
        if (room1 == default)
        {
            room1 = new List<TileModel>(1) { wall.Tile1 };
        }
        if (room2 == default)
        {
            room2 = new List<TileModel>(1) { wall.Tile2 };
        }

        // We create a new room. This room is the addition of room1 and room2. 
        List <TileModel> unionRoom = new List<TileModel>(room1.Count + room2.Count);
        unionRoom.AddRange(room1);
        unionRoom.AddRange(room2);

        // We take our list of rooms, remove room1 and room2.
        // Then we add the room whose concat both of them.
        allRooms.Remove(room1);
        allRooms.Remove(room2);
        allRooms.Add(unionRoom);
    }

    // We create a list of all possible walls given a array of Tiles. 
    private List<WallModel> CreateWalls(in TileModel[] Tiles)
    {
        List<WallModel> walls = new List<WallModel>(MapModel.BOARD_SIZE * (MapModel.BOARD_SIZE - 1) * 2);
        // j = posX ; i = posY
        // wall in axis X
        for (var i = 0; i < MapModel.BOARD_SIZE; i++)
        {
            for (var j = 0; j < MapModel.BOARD_SIZE - 1; j++)
            {
                WallModel wall = new WallModel();
                wall.Tile1 = Tiles[j + i * MapModel.BOARD_SIZE];
                wall.Tile2 = Tiles[(j + 1) + i * MapModel.BOARD_SIZE];
                walls.Add(wall);
            }
        }

        // j = posX ; i = posY
        // wall in axis Y
        for (var i = 0; i < MapModel.BOARD_SIZE - 1; i++)
        {
            for (var j = 0; j < MapModel.BOARD_SIZE; j++)
            {
                WallModel wall = new WallModel();
                wall.Tile1 = Tiles[j + i * MapModel.BOARD_SIZE];
                wall.Tile2 = Tiles[j + (i + 1) * MapModel.BOARD_SIZE];
                walls.Add(wall);
            }
        }

        return walls;
    }
}
