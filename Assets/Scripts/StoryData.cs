using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(StoryData))]
//public class StoryDataEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        EditorUtility.SetDirty(target);
//        GUIStyle myStyle = new GUIStyle(GUI.skin.label);
//        GUIStyle messageStyle = new GUIStyle(GUI.skin.label);
//        myStyle.padding = new RectOffset(50, 50, 0, 10);
//        messageStyle.padding = new RectOffset(50, 50, 0,0);
//        GUIStyle addButton = new GUIStyle(GUI.skin.button);
//        addButton.padding = new RectOffset(0, 0, 10, 10);


//        StoryData a = (StoryData)target;

//        for (int i = 0; i < a.storyData.Count; i++)
//        {
//            StoryDataSet b = a.storyData[i];    

//            GUILayout.BeginVertical(myStyle); 

//            GUILayout.Space(10);

//            GUILayout.Label("SET: " + b.id);

//            GUILayout.Space(10);

//            b.id = GUILayout.TextField(b.id);
//            b.flag = GUILayout.TextField(b.flag);

//            GUILayout.Space(10);

//            GUILayout.Label("MESSAGES");

//            for (int x = 0; x < b.message.messages.Count; x++)
//            {
//                GUILayout.BeginVertical(messageStyle);

//                GUILayout.Space(5);
//                GUILayout.TextArea("MESSAGE");

//                if (GUILayout.Button("Remove Message"))
//                {
//                    b.message.messages.RemoveAt(x);
//                }

//                GUILayout.EndVertical();
//            }

//            if (GUILayout.Button("+"))
//            {
//                b.message.messages.Add("Im a message");
//            }

//            if (GUILayout.Button("Remove Set"))
//            {
//                a.storyData.RemoveAt(i);
//            }

//            GUILayout.EndVertical();
//        }

//        GUILayout.Space(10);

//        if(GUILayout.Button("ADD SET" , addButton))
//        {
//            a.storyData.Add(new StoryDataSet());
//        }
//        serializedObject.ApplyModifiedProperties();

//        SetDirty();
//        EditorUtility.SetDirty(a);
//        Undo.RecordObject(a, "Added Stoy");
//    }
//}

[CreateAssetMenu(fileName ="StoryData" , menuName ="StoryData")]
public class StoryData : ScriptableObject
{
    public List<StoryDataSet> storyData = new List<StoryDataSet>();
}

[System.Serializable]
public class StoryDataSet 
{
    public string id;
    public string flag;

    public Message message = new Message();

    public Message visitedMessage = new Message();

    public string newStoryID;

    public void Use ()
    {
        if(string.IsNullOrEmpty(PlayerPrefs.GetString(flag)))
        {
            //UI_Speech.instance.AddMessage(message);
            PlayerPrefs.SetString(flag, flag);
        }
        else
        {
            //UI_Speech.instance.AddMessage(visitedMessage);
        }
    }
}
