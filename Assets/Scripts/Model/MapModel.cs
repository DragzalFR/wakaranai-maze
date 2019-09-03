using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    public const int BOARD_SIZE = 8;

    public Vector2 HeadQuarter { get; private set; }
    public CharacterModel Character { get; private set; }

    public TileModel[] Tiles;
    public MazeModel Maze { get; private set; }

    public TreasureModel Treasure { get; private set; }

    public MapModel(int characterPositionX = 0, int characterPositionY = 0)
    {
        HeadQuarter = new Vector2(characterPositionX, characterPositionY);

        Character = new CharacterModel();
        Character.Init(HeadQuarter, this);

        GenerateBoardTile();
        Maze = new MazeModel(in Tiles);

        Treasure = new TreasureModel();
    }

    // Return the treasure if he is at the given position, 
    // Else return default.
    public TreasureModel SearchTreasureAt(Vector2 position)
    {
        if(position == Treasure.Position)
        {
            return Treasure;
        }
        else
        {
            return default;
        }
    }

    public bool IsGameWon()
    {
        return (Treasure.Position == HeadQuarter);
    }



    // Create all the tile into the board. 
    private void GenerateBoardTile()
    {
        Tiles = new TileModel[BOARD_SIZE * BOARD_SIZE];
        // j = posX ; i = posY
        for (var y = 0; y < BOARD_SIZE; y++)
        {
            for (var x = 0; x < BOARD_SIZE; x++)
            {
                Tiles[x + y * BOARD_SIZE].Position.x = x;
                Tiles[x + y * BOARD_SIZE].Position.y = y;
            }
        }
    }
}
