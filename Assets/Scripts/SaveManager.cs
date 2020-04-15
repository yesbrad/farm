using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static string c_cropData = "cropSave";
    public static string c_inData = "inSave";
    public static string c_player = "playerSave";

    public static EventIDs CurrentID;

    public static bool HasKey(string _id)
    {
        return PlayerPrefs.HasKey(_id);
    }

    public static void NewGame()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void Save()
    {
        Debug.Log("Game has been Saved!");
        CropManager.instance.Save();
        Inventory.instance.Save();
        PlayerController.instance.Save();
        PlayerPrefs.SetString(StaticStrings.CurrentID, CurrentID.ToString());
    }

    public static void Load()
    {
        Debug.Log("Game has been Loaded!");
        CropManager.instance.Load();
        Inventory.instance.Load();
        PlayerController.instance.Load();

        if (string.IsNullOrEmpty(PlayerPrefs.GetString(StaticStrings.CurrentID)))
            PlayerPrefs.SetString(StaticStrings.CurrentID, EventIDs.ZWelcome.ToString());

        CurrentID = (EventIDs)System.Enum.Parse(typeof(EventIDs), PlayerPrefs.GetString(StaticStrings.CurrentID));
    }
}
