using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#000055")]
public class Fade : OceanNode
{
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode outputMiddle;
    public float speed = 1;
    public float delay = 0.5f;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);
        UI_Fade.instance.Fade(NextNode, speed, delay, () => {
            CallNode("outputMiddle");
        });
    }

	public override void NextNode()
	{
        base.NextNode();
	}
}
