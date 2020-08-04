using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#4b40e6")]
public class SetFlag : OceanNode
{
	[Header("USES FLAG ID")]
    public string flag;
    
    [Header("OR SCENE ID")]
    public bool useSceneIdFlag;
    
    public override void Use(Interactable interactable)
    {
        base.Use(interactable);

        if (useSceneIdFlag && !string.IsNullOrEmpty(interactable.sceneID))
        {
            SaveManager.SetFlag(interactable.sceneID);
        }
        else
        {
            SaveManager.SetFlag(flag);
        }

        NextNode();
    }
}
