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
        public string id;
        public bool swapped;

        public TileSaveData (string tileID, bool isSwapped)
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

    public void RegisterTile(TileSwapEvent swapEvent)
    {
        tiles.Add(swapEvent);
    }

    public void SwapTile(string id, bool swap)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i].GetTileID() == id)
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
            tileSave.tileSaveData.Add(new TileSave.TileSaveData(tiles[i].GetTileID(), tiles[i].swapped));
        }

		string jsonSave = JsonUtility.ToJson(tileSave, true);

		PlayerPrefs.SetString(SaveManager.c_tileSwap, jsonSave);
    }

    public void Load()
    {
        tileSave.tileSaveData.Clear();
        
        if (PlayerPrefs.HasKey(SaveManager.c_tileSwap))
        {
			tileSave = JsonUtility.FromJson<TileSave>(PlayerPrefs.GetString(SaveManager.c_tileSwap));

			for (int i = 0; i < tileSave.tileSaveData.Count; i++)
			{
				for (int x = 0; i < tiles.Count; i++) 
				{
					if (tiles[x].GetTileID() == tileSave.tileSaveData[i].id)
					{
						tiles[x].LoadTile(tileSave.tileSaveData[i]);
					}
				}
			}
        }
    }
}
