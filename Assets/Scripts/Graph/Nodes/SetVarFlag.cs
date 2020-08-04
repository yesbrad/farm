using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NodeTint("#4b40e6")]
public class SetVarFlag : OceanNode
{
    public enum FlagOperator
    {
        Add,
        Subtract
    }
    
    public VarIDs flag;
    public FlagOperator flagOperator;
    
    public override void Use(Interactable interactable)
    {
        base.Use(interactable);
        SaveManager.SetVarFlag(flag.ToString(), flagOperator == FlagOperator.Add ? 1 : -1);
        NextNode();
    }
}
