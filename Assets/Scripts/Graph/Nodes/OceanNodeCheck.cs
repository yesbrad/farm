using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class OceanNodeCheck : Node 
{
    [Input(backingValue = ShowBackingValue.Never, typeConstraint = TypeConstraint.Inherited)] public OceanNode input;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode outputTrue;
    [Output(backingValue = ShowBackingValue.Never)] public OceanNode outputFalse;

    internal float id;

    public virtual void Use() {}

    public virtual void NextNode (bool _true)
    {
        if (_true)
            GoNextNode();
        else
            GoNextNodeB();
    }

    public void GoNextNode ()
    {
        NodePort port = null;

        port = GetOutputPort("outputTrue");

        if (port == null)
        {
            Debug.Log("No Output Found Stopping Here!");
            return;
        }

        NodePort connection = port.GetConnection(0);

        if ((connection.node as OceanNode) != null)
            (connection.node as OceanNode).Use();

        if ((connection.node as OceanNodeCheck) != null)
            (connection.node as OceanNodeCheck).Use();
    }

    public void GoNextNodeB()
    {
        NodePort port = null;

        port = GetOutputPort("outputFalse");

        if (port == null)
        {
            Debug.Log("No Output Found Stopping Here!");
            return;
        }

        NodePort connection = port.GetConnection(0);
       
        if ((connection.node as OceanNode) != null)
            (connection.node as OceanNode).Use();

        if ((connection.node as OceanNodeCheck) != null)
            (connection.node as OceanNodeCheck).Use();
    }

    public override object GetValue(NodePort port)
    {
        return null;
    }
}