using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class Chat : OceanNode
{
    
    [TextArea(5,100)] public string message;

	public override void Use()
	{
        base.Use();
        AddMessage(message);
	}
}
