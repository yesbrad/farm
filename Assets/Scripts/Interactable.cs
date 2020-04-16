using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public StoryGraph graph;

    public bool isEngaged;

    public void Interact ()
    {
        graph.StartEvent(this);
    }
}
