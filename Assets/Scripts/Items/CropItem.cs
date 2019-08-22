using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class CropStage
{
    public string name;

    public int lifeTime = 5;

    public TileBase stageTile;

    public bool isAdult;
}

[CreateAssetMenu(fileName="New Crop", menuName="Items/Crop")]
public class CropItem : Item
{
    public CropStage[] lifeStages;
    public Item harvestItem;
    public int minDropAmount = 2;
    public int maxDropAmount = 5;
    public Item failedItem;

    public override bool Interact ()
    {
        Crop currentCrop = PlayerDetection.instance.CurrentCrop;

        if (currentCrop != null)
        {
            if (currentCrop.StateEquals(CropState.Nothing))
            {
                currentCrop.Plant(this);
                return true;
            }
		}

        return false;
    }
}
