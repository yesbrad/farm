using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#40e682")]
public class CheckFlag : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode set;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode notSet;

    public string flag;
    
    [Header("USE SCENE ID")]
    public bool useSceneID;
    
    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        NextNode(SaveManager.CheckFlag(useSceneID ? interactable.sceneID : flag) ? "set" : "notSet");
    }
}
