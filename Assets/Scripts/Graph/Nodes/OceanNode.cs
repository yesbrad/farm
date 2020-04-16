using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class OceanNode : Node 
{
    [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public OceanNode input;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode output;

    internal float id;

    private Interactable nodeInteractable;

    public virtual void Use(Interactable interactable)
    {
        nodeInteractable = interactable;
        nodeInteractable.isEngaged = true;
    }

    public void AddMessage(string _message)
    {
        UI_Speech.instance.AddMessage(_message, NextNode);
    }

    public virtual void NextNode ()
    {
        NextNode(0);
    }

    public virtual void NextNode (int node = 0)
    {
        if(node == 0)
        {
            CallNode("output");
            return;
        }

        CallNode("output" + node);
    }

    public virtual void CallNode (string _outputPort)
    {
        NodePort port = null;

        port = GetOutputPort(_outputPort);

        if (port == null) 
        {
            Debug.Log("No Output Found Stopping Here!");
            nodeInteractable.isEngaged = false;
            nodeInteractable = null;
            return;
        }

        for (int i = 0; i < port.ConnectionCount; i++)
        {
            NodePort connection = port.GetConnection(i);

            if((connection.node as OceanNode) != null)
                (connection.node as OceanNode).Use(nodeInteractable);
        }
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }
}