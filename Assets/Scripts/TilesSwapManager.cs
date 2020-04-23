using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class TileSave
{
    public List<TileSaveData> tileSaveData = new List<TileSaveData>();
    
    [System.Serializable]
    public class TileSaveData
    {
        public TileSwapIDs id;
        public bool swapped;

        public TileSaveData (TileSwapIDs tileID, bool isSwapped)
        {
            id = tileID;
            swapped = isSwapped;
        }
    }
    

}

public class TilesSwapManager : MonoBehaviour
{
    public static TilesSwapManager instance;
    public List<TileSwapEvent> tiles = new List<TileSwapEvent>();
    
    private TileSave tileSave = new TileSave();
    
    void Start()
    {
        instance = this;
    }

    public TileSave.TileSaveData RegisterTile(TileSwapEvent swapEvent)
    {
        tiles.Add(swapEvent);

        for (int i = 0; i < tileSave.tileSaveData.Count; i++)
        {
            if (swapEvent.tileSwapId == tileSave.tileSaveData[i].id)
            {
                return tileSave.tileSaveData[i];
            }
        }

        return null;
    }

    public void SwapTile(TileSwapIDs id, bool swap)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].tileSwapId == id)
            {
                tiles[i].SwapTile(swap);   
            }
        }
    }

    public void Save()
    {
        tileSave.tileSaveData.Clear();
        
        for (int i = 0; i < tiles.Count; i++)
        {
            tileSave.tileSaveData.Add(new TileSave.TileSaveData(tiles[i].tileSwapId, tiles[i].swapped));
        }
        
        PlayerPrefs.SetString(SaveManager.c_tileSwap, JsonUtility.ToJson(tileSave));
    }

    public void Load()
    {
        tileSave.tileSaveData.Clear();
        
        if (PlayerPrefs.HasKey(SaveManager.c_tileSwap))
        {
            tileSave = JsonUtility.FromJson<TileSave>(PlayerPrefs.GetString(SaveManager.c_tileSwap));
        }
    }
}
