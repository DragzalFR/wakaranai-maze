using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreasureView : MonoBehaviour
{
    TreasureModel Model;
    public void SetModel(TreasureModel treasureModel)
    {
        Model = treasureModel;
    }

    GameObject go;

    private void Start()
    {
        Object prefab;
        string path = "Assets/Prefabs/Treasure.prefab";

#if UNITY_EDITOR
        prefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
#else
        prefab = Resources.Load<GameObject>(path);
#endif
        go = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        go.transform.Rotate(new Vector3(90, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        go.transform.position = new Vector3(Model.Position.x, Model.Position.y, -0.5f);
    }
}
