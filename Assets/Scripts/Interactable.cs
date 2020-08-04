using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public StoryGraph graph;

    public string sceneID;
    
    [Header("Sprites")]
    public bool willFace;
    public LookSpites lookSpites;
    public Sprite faceSprite;
    
    public struct LookSpites
    {
        public Sprite forward;
        public Sprite backward;
        public Sprite left;
        public Sprite right;
    }
    
    public bool isEngaged;

    public void Interact ()
    {
        graph.StartEvent(this);
    }

    void ChangeLookDirection(Vector3 direction)
    {

    }

    public void OnEndEvent()
    {
        
    }
}
