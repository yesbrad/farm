using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using XNode;

[NodeTint("#00A100")]
public class YesOrNoChat : OceanNode
{
	[Output(backingValue = ShowBackingValue.Never)] public OceanNode yes;
	[Output(backingValue = ShowBackingValue.Never)] public OceanNode no;

	[TextArea(5,100)] public string message;	

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);
		List<Action> yesNo = new List<Action>();
		yesNo.Add(() => NextNode("no"));
		yesNo.Add(() => NextNode("yes"));
		UI_Speech.instance.AddMessage(message, yesNo);
	}
}
