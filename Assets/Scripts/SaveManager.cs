﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static string c_cropData = "cropSave";
    public static string c_inData = "inSave";
    public static string c_player = "playerSave";
    public static string c_tileSwap = "tileSwapSave";

    private static EventIDs currentID;
    
    public static EventIDs CurrentID
    {
        get => currentID;

        set
        {
            if(GameManager.instance.isTest)
                UI_HUD.instance.UpdateTestIDText(value);

            currentID = value;
        }
    }

    public static bool HasKey(string _id)
    {
        return PlayerPrefs.HasKey(_id);
    }

    public static void NewGame()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SetFlag(string flag)
    {
        PlayerPrefs.SetInt(flag, 1);
    }
    
    public static void SetVarFlag(string flag, int amount)
    {
        PlayerPrefs.SetInt(flag, PlayerPrefs.GetInt(flag, 0) + amount);
    }

    public static int CheckVarFlag(string flag)
    {
        return PlayerPrefs.GetInt(flag, 0);
    }
    
    public static void RemoveFlag(string flag)
    {
        PlayerPrefs.SetInt(flag, 0);
    }
    
    public static bool CheckFlag(string flag)
    {
        return PlayerPrefs.GetInt(flag, 0) == 1;
    }

    public static void Save()
    {
        if (GameManager.instance.isTest)
        {
            Debug.LogWarning("GAME IS IN TEST MODE NOT SAVING");
            return;
        }
        
        Debug.Log("Game has been Saved!");
        CropManager.instance.Save();
        Inventory.instance.Save();
        PlayerController.instance.Save();
        TilesSwapManager.instance.Save();
        PlayerPrefs.SetString(StaticStrings.CurrentID, CurrentID.ToString());
    }

    public static void Load()
    {
        Debug.Log("Game has been Loaded!");
        CropManager.instance.Load();
        Inventory.instance.Load();
        PlayerController.instance.Load();
        TilesSwapManager.instance.Load();
        
        if (string.IsNullOrEmpty(PlayerPrefs.GetString(StaticStrings.CurrentID)))
            PlayerPrefs.SetString(StaticStrings.CurrentID, EventIDs.Z1Welcome.ToString());

        CurrentID = (EventIDs)System.Enum.Parse(typeof(EventIDs), PlayerPrefs.GetString(StaticStrings.CurrentID));
    }
}
