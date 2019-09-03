using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    CharacterModel Model;
    public void SetModel( CharacterModel characterModel)
    {
        Model = characterModel;
    }

    GameObject go;

    private void Start()
    {
        Object prefab;
        string path = "Assets/Prefabs/Character.prefab";

#if UNITY_EDITOR
        prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
#else
        prefab = Resources.Load<GameObject>(path);
#endif
        //prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Character.prefab", typeof(GameObject));
        go = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.Rotate(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        go.transform.position = Model.GetPosition3d();
    }
}
