using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MapView : MonoBehaviour
{
    const int BOARD_SIZE = 8;

    MapModel Model;
    public void SetModel(MapModel mapModel)
    {
        this.Model = mapModel;
    }

    // Start is called before the first frame update
    private void Start()
    {
        InstantiateBoard();
        InstantiateHeadQuarter();
        InstantiateMazeWall();
        InstantiateBoardLimit();
    }

    // TODO : Correct the visibility and tile asset.
    private void InstantiateBoard()
    {
        Object tileType1;
        Object tileType2;

        string path1 = "Assets/Prefabs/BlackTile.prefab";
        string path2 = "Assets/Prefabs/WhiteTile.prefab";

#if UNITY_EDITOR
        tileType1 = AssetDatabase.LoadAssetAtPath(path1, typeof(GameObject));
        tileType2 = AssetDatabase.LoadAssetAtPath(path2, typeof(GameObject));
#else
        tileType1 = Resources.Load<GameObject>(path1);
        tileType2 = Resources.Load<GameObject>(path2);
#endif
        // Temporary : more visibility for beginner with only one color.
        tileType2 = tileType1;

        for (var i = 0; i < Model.Tiles.Length; i++)
        {
            var x = Model.Tiles[i].Position.x;
            var y = Model.Tiles[i].Position.y;

            GameObject newTile;
            if ((x + y) % 2 == 1)
                newTile = Instantiate(tileType2, Vector3.zero, Quaternion.identity) as GameObject;
            else
                newTile = Instantiate(tileType1, Vector3.zero, Quaternion.identity) as GameObject;

            newTile.transform.position = new Vector3(x, y, 0);
        }
    }

    private void InstantiateHeadQuarter()
    {
        Object prefab;
        string path = "Assets/Prefabs/BaseTile.prefab";

#if UNITY_EDITOR
        prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
#else
        prefab = Resources.Load<GameObject>(path);
#endif
        GameObject headQuarter = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        headQuarter.transform.position = new Vector3(Model.HeadQuarter.x, Model.HeadQuarter.y, -0.1f);
    }

    private void InstantiateMazeWall()
    {
        Object prefab;
        string path = "Assets/Prefabs/Wall.prefab";

#if UNITY_EDITOR
        prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
#else
        prefab = Resources.Load<GameObject>(path);
#endif
        foreach (var wall in Model.Maze.MazeWalls)
        {
            GameObject newWall = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

            newWall.transform.position = wall.GetPosition3d(-0.5f);

            if (wall.Tile1.Position.x == wall.Tile2.Position.x)
                newWall.transform.localScale = new Vector3(1.1f, 0.1f, 1);
            else // if same y
                newWall.transform.localScale = new Vector3(0.1f, 1.1f, 1);
        }
    }

    private void InstantiateBoardLimit()
    {
        Object prefab;
        string path = "Assets/Prefabs/Limit.prefab";

#if UNITY_EDITOR
        prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
#else
        prefab = Resources.Load<GameObject>(path);
#endif

        GameObject borderBottom = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        borderBottom.transform.position = new Vector3(BOARD_SIZE / 2.0f - 0.5f, -0.5f, -1);
        borderBottom.transform.localScale = new Vector3(BOARD_SIZE + 0.2f, 0.2f, 1);

        GameObject borderTop = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        borderTop.transform.position = new Vector3(BOARD_SIZE / 2.0f - 0.5f, BOARD_SIZE - 0.5f, -1);
        borderTop.transform.localScale = new Vector3(BOARD_SIZE + 0.2f, 0.2f, 1);

        GameObject borderLeft = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        borderLeft.transform.position = new Vector3(-0.5f, BOARD_SIZE / 2.0f - 0.5f, -1);
        borderLeft.transform.localScale = new Vector3(0.2f, BOARD_SIZE + 0.2f, 1);

        GameObject borderRight = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        borderRight.transform.position = new Vector3(BOARD_SIZE - 0.5f, BOARD_SIZE / 2.0f - 0.5f, -1);
        borderRight.transform.localScale = new Vector3(0.2f, BOARD_SIZE + 0.2f, 1);
    }
}
