using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    const int START_LIFE = 3;
    const int START_MOVEMENT = 8;

    public Vector3 GetPosition3d(float z = 0f)
    {
        return new Vector3(Position.x, Position.y, z);
    }

    public void Init(Vector2 position, MapModel map)
    {
        this.Position = position;
        Life = START_LIFE;
        Movement = START_MOVEMENT;

        Board = map;
    }

    // Move the character into the given direction. 
    // TODO : Management of movement.
    public void MoveTo(Vector2 direction)
    {
        direction.Normalize();

        // Potential Wall
        Vector2 potentialWallPosition = new Vector2(Position.x + direction.x / 2, Position.y + direction.y / 2);

        // If we don't find wall, 
        // And we stay into the limit of the board
        if (!Board.Maze.IsWallExistAt(potentialWallPosition)
            && IsInsideBoard(potentialWallPosition))
        {
            Position += direction;

            if (Dream == default)
            {
                Dream = Board.SearchTreasureAt(this.Position);
            }
            else
            {
                Dream.Position = this.Position;
            }
        }
        // TODO : Replace debug.log to UI to signal a wall encounter.
        else
        {
            if (Board.Maze.IsWallExistAt(potentialWallPosition))
                Debug.Log("Il y a un obstacle dans cette direction.");
            else
                Debug.Log("On va trop loin la.");
        }


    }
    
    // Call MoveTo(Vector2). 
    public void MoveTo(Vector3 direction)
    {
        MoveTo(new Vector2(direction.x, direction.y));
    }

    // Return true if the position is inside the board, 
    // Else return false. 
    private bool IsInsideBoard(Vector2 position)
    {
        return (position.x <= MapModel.BOARD_SIZE - 1
                && position.y <= MapModel.BOARD_SIZE - 1
                && position.x >= 0
                && position.y >= 0);
    }

    private Vector2 Position;
    private int Life;
    private int Movement;

    private MapModel Board;
    private TreasureModel Dream = default;
}
