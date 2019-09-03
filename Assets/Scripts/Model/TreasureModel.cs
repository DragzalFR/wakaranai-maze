using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureModel
{
    public Vector2 Position;

    public TreasureModel()
    {
        Position = new Vector2(
            Random.Range(0, MapModel.BOARD_SIZE),
            Random.Range(0, MapModel.BOARD_SIZE)
            );
    }
}
