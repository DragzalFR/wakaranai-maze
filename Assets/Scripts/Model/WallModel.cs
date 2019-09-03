using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WallModel
{
    public TileModel Tile1 { get; set; }
    public TileModel Tile2 { get; set; }

    // Return the wall position
    // A wall is at midway between the 2 tiles it separate.
    public Vector2 GetPosition()
    {
        float x = (Tile1.Position.x + Tile2.Position.x) / 2.0f;
        float y = (Tile1.Position.y + Tile2.Position.y) / 2.0f;
        return new Vector2(x, y);
    }

    // Return the wall position
    // A wall is at midway between the two tiles it separates.
    // The value in z is given, or 0 by default.
    public Vector3 GetPosition3d(float z = 0f)
    {
        float x = (Tile1.Position.x + Tile2.Position.x) / 2.0f;
        float y = (Tile1.Position.y + Tile2.Position.y) / 2.0f;
        return new Vector3(x, y, z);
    }
}
