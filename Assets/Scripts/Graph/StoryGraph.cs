using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEditor;
using System;

[CreateAssetMenu]
public class StoryGraph : NodeGraph
{
    private DefaultNode defaultNode;
    private List<Start> startNodes = new List<Start>();

    public void StartEvent()
    {
        startNodes.Clear();

        for (int i = 0; i < nodes.Count; i++)
        {
            Start startNode = (nodes[i] as Start);

            if (startNode != null)
                startNodes.Add(startNode);

            if ((nodes[i] as DefaultNode) != null)
                defaultNode = (nodes[i] as DefaultNode);
        }

        for (int z = 0; z < startNodes.Count; z++)
        {
            if (startNodes[z].eventIDs == SaveManager.CurrentID)
            {
                startNodes[z].Use();
                return;
            }
        }

		if (defaultNode != null)
			defaultNode.Use();
    }
}
