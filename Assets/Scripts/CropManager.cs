using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropManager : MonoBehaviour
{
    public static CropManager instance;
    public CropSave crops;

    public List<Crop> currentCrops = new List<Crop>();

    [System.Serializable]
    public class CropSave
    {
        public List<CropSaveData> crops = new List<CropSaveData>();
    }

    [System.Serializable]
    public struct CropSaveData
    {
        public Vector3 position;
        public int stageIndex;
        public CropItem cropItem;

        public CropSaveData (Vector3 _position , int _stageIndex , CropItem _cropItem)
        {
            position = _position;
            stageIndex = _stageIndex;
            cropItem = _cropItem;
        }
    }

	private void Awake()
	{
        instance = this;
	}

    public void TimeSkip ()
    {
        for (int i = 0; i < currentCrops.Count; i++)
        {
            currentCrops[i].TimeSkip();
        }
    }

    public void AddCrop (Crop _crop) 
    {
        //CropSaveData a = new CropSaveData( _crop.placePosition , _crop.GetIndex , _crop.cropData);
        //print(a.cropItem.id);
        //crops.crops.Add(a);

        currentCrops.Add(_crop);
    }

    public void RemoveCrop (Crop _crop)
    {
        //CropSaveData a = new CropSaveData( _crop.placePosition, _crop.GetIndex, _crop.cropData);
        //crops.crops.Remove(a);

        currentCrops.Remove(_crop);
    }

    public bool HasGrowingCrop(CropItem crop)
    {
        for (int i = 0; i < currentCrops.Count; i++)
        {
            if(currentCrops[i].cropData != null && currentCrops[i].cropData.id == crop.id && !currentCrops[i].IsDead)
                return true;
        }

        return false;
    }
    
    public void Save ()
    {
        if (crops == null)
            return;

        crops.crops.Clear();

        for (int i = 0; i < currentCrops.Count; i++)
        {
            CropSaveData a = new CropSaveData( currentCrops[i].placePosition, currentCrops[i].GetIndex, currentCrops[i].cropData);
            crops.crops.Add(a);
        }

        string json = JsonUtility.ToJson(crops);
        PlayerPrefs.SetString(SaveData.c_cropData, json);
    }

    public void Load ()
    {
        for (int x = 0; x < currentCrops.Count; x++)
        {
            currentCrops[x].DeleteCrop();
        }

        currentCrops.Clear();

        string json = PlayerPrefs.GetString(SaveData.c_cropData);

        if (string.IsNullOrEmpty(json))
            return;

        crops = (CropSave)JsonUtility.FromJson(json, typeof(CropSave));

        if (crops.crops == null)
            return;

        for (int i = 0; i < crops.crops.Count; i++)
        {
            Crop a = Instantiate(DesignManager.instance.CropPrefab, crops.crops[i].position, Quaternion.identity).GetComponent<Crop>();

            if(crops.crops[i].cropItem != null)
			    a.LoadCrop(crops.crops[i].stageIndex, crops.crops[i].cropItem);

            a.Init(crops.crops[i].position , true);

            AddCrop(a);
        }
    }
}
