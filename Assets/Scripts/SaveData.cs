using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static string c_cropData = "cropSave";
    public static string c_inData = "inSave";
    public static string c_player = "playerSave";

    public static bool HasKey(string _id)
    {
        return PlayerPrefs.HasKey(_id);
    }

    public static void NewGame()
    {
        PlayerPrefs.DeleteAll();
    }
}
