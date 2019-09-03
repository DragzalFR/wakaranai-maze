using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private MapModel MapModel;
    [SerializeField]
    private GameObject Map = default;

    private CharacterModel CharacterModel;
    [SerializeField]
    private GameObject Character = default;

    private TreasureModel TreasureModel;
    [SerializeField]
    private GameObject Treasure = default;

    private bool InMove = false;

    private void Awake()
    {
        MapModel = new MapModel();
        Map.GetComponent<MapView>().SetModel(MapModel);

        CharacterModel = MapModel.Character;
        Character.GetComponent<CharacterView>().SetModel(CharacterModel);

        TreasureModel = MapModel.Treasure;
        Treasure.GetComponent<TreasureView>().SetModel(TreasureModel);
    }

    // Update is called once per frame
    private void Update()
    {
        if (MapModel.IsGameWon())
            SceneManager.LoadScene(0);

        if (!InMove)
        {
            Vector3 moveDirection = GetDirectionInput();
            if(moveDirection != Vector3.zero)
            {
                CharacterModel.MoveTo(moveDirection);
            }
        }
    }

    private Vector3 GetDirectionInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            return Vector3.up;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            return Vector3.down;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            return Vector3.left;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            return Vector3.right;

        return Vector3.zero;
    }

    private void OnApplicationQuit()
    {
        //EditorUtility.DisplayDialog("Goodbye", "Thank you for playing this game.", "Leave");
    }
}
