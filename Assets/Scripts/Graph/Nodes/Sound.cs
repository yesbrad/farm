using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeTint("#0000FF")]
public class Sound : OceanNode
{
    public AudioClip audioClip;

	public override void Use(Interactable interactable)
	{
        base.Use(interactable);

        if (audioClip != null)
        {
            AudioController.instance.Play(audioClip);
        }
        else
            Debug.Log("Missing AUDIO From Node: " + id);
        
        base.NextNode();
    }
}
