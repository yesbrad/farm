using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Clippers", fileName = "New Item")]
public class Clippers : Item
{
	public override bool Interact()
	{
        Crop currentCrop = PlayerDetection.instance.CurrentCrop;

        if(currentCrop != null)
        {
            if(!currentCrop.StateEquals(CropState.Nothing))
            {
                currentCrop.Harvest();
                PlayerDetection.instance.CurrentCrop = null;
			}
        }

        return false;
	}
}
