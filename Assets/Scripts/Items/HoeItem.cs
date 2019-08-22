using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Items/Hoe")]
public class HoeItem : Item
{
	public override bool Interact()
	{
        if(PlayerDetection.instance.CurrentCrop == null)
        {
            Crop a = Instantiate(DesignManager.instance.CropPrefab).GetComponent<Crop>();
            a.Init(PlayerDetection.instance.transform.position);
            PlayerDetection.instance.CurrentCrop = a;

            return false;
        }

        return false;
	}
}
