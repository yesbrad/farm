using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum CropState
{
    Nothing,
    Normal,
    Dead,
}

public class Crop : MonoBehaviour
{
    internal CropItem cropData;

	private CropStage currentStage;
    private float curAgeLevel;
    private int currentStageIndex;
    private CropState cropState;
    private TileBase prevTile;

    internal Vector3 placePosition;

    public bool StateEquals (CropState _state) { return _state == cropState;}
    public void SetCropState(CropState _state) {cropState = _state;}
    public bool IsDead { get { return cropState == CropState.Dead; }}
    public int GetIndex { get { return currentStageIndex; } }

    public void Init (Vector3 _position , bool _loaded = false)
    {
        transform.position = FarmUtilites.GetCenterOfCell(TilemapGroup.plant, _position);

        placePosition = transform.position;

        prevTile = DesignManager.instance.grassTile; //TilemapGroup.normal.GetTile(TilemapGroup.normal.WorldToCell(transform.position));

        if(!_loaded)
		    SetCropState(CropState.Nothing);

        TilemapGroup.normal.SetTile(TilemapGroup.normal.WorldToCell(transform.position), DesignManager.instance.fertileTile);

        BoxCollider2D newBox = (BoxCollider2D)gameObject.AddComponent(typeof(BoxCollider2D));
        newBox.size = Vector2.one * 0.05f;
        newBox.isTrigger = true;

        if (CropManager.instance != null && !_loaded)
            CropManager.instance.AddCrop(this);
    }

    public void LoadCrop (int _newIndex, CropItem _cropItem)
    {
        cropData = _cropItem;
        currentStageIndex = _newIndex;
        SetCropState(CropState.Normal);
        InitStage(currentStageIndex);
    }

    void Update ()
    {
        if (IsDead)
            return;
        
        UpdateCropVitals();

        if(!StateEquals(CropState.Nothing))
            UpdateLifeCycle();
    }

    public void UpdateLifeCycle ()
    {
        curAgeLevel += Time.deltaTime;

        if(curAgeLevel > currentStage.lifeTime)
            NextStage();
    }

    public void NextStage ()
    {
        //Debug.Log("NextStage");
        
        if(currentStageIndex > cropData.lifeStages.Length - 2)
        {
            Die();
            //Debug.Log("Crop JUST DIED");
            return;
        }
        
        currentStageIndex++;
        InitStage(currentStageIndex);
    }

    private void InitStage (int _stage)
    {
        if(_stage > cropData.lifeStages.Length - 1)
        {
            Debug.LogError("Crop stage was higher than the crops stage data: " + _stage);
            DeleteCrop();
            return;
        }

        currentStage = cropData.lifeStages[_stage];
        curAgeLevel = 0;

        Tilemap tilemap = TilemapGroup.plant;

        tilemap.SetTile(tilemap.WorldToCell(transform.position), currentStage.stageTile);
        TilemapGroup.normal.SetTile(TilemapGroup.normal.WorldToCell(transform.position), DesignManager.instance.fertileTile);
    }

    public void UpdateCropVitals ()
    {
        //waterLevel -= Time.deltaTime * currentStage.waterUsage;
    }

    public void Die ()
    {
        SetCropState(CropState.Dead);
    }

    public void Plant (CropItem _data)
    {
        cropData = _data;
        SetCropState(CropState.Normal);
        InitStage(0);
    }

    public void TimeSkip()
    {
        if (cropData == null)
            return;
        
        if(cropState == CropState.Normal)
            InitStage(cropData.lifeStages.Length - 2);
    }

    public void Harvest ()
    {
        if(currentStage.isAdult)
        {
            int amt = Random.Range(cropData.minDropAmount, cropData.maxDropAmount);

            for (int i = 0; i < amt; i++)
				Inventory.instance.AddItem(cropData.harvestItem);
        }
        else
            Inventory.instance.AddItem(cropData.failedItem);

        CropManager.instance.RemoveCrop(this);

        DeleteCrop();
    }

    public void DeleteCrop()
    {
        TilemapGroup.normal.SetTile(TilemapGroup.normal.WorldToCell(transform.position), prevTile);
        TilemapGroup.plant.SetTile(TilemapGroup.plant.WorldToCell(transform.position), null);
        Destroy(this.gameObject);
    }
}
