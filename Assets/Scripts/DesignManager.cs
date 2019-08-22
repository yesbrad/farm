using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DesignManager : MonoBehaviour
{
    public static DesignManager instance;

    [Header("Base Tiles")]
    public TileBase fertileTile;
    public TileBase grassTile;

    [Header("Base Prefabs")]
    [SerializeField] GameObject cropPrefab;

    public GameObject CropPrefab 
    {
        get
        {   
            if(cropPrefab == null)
            {
                Debug.LogError("THERE IS NO BASE CROP PREFAB ASSIGNED! FIX NOW!");
                return null;
            }

            return cropPrefab;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
