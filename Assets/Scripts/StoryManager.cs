using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public string currentStoryID;
    public string defaultID = "Start";

	private void Start()
	{
        currentStoryID = defaultID;
	}
}

