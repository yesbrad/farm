using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

    public class OceanNode : Node 
{
    [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public OceanNode input;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode output;

    internal float id;

    public virtual void Use() {}

    public void AddMessage(string _message)
    {
        UI_Speech.instance.AddMessage(_message, NextNode);
    }

    public virtual void NextNode ()
    {
        CallNode("output");
    }

    public virtual void CallNode (string _outputPort)
    {
        NodePort port = null;

        port = GetOutputPort(_outputPort);

        if (port == null) 
        {
            Debug.Log("No Output Found Stopping Here!");
			return;
        }

        for (int i = 0; i < port.ConnectionCount; i++)
        {
            NodePort connection = port.GetConnection(i);

            if((connection.node as OceanNode) != null)
                (connection.node as OceanNode).Use();

            if ((connection.node as OceanNodeCheck) != null)
                (connection.node as OceanNodeCheck).Use();
        }
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }
}