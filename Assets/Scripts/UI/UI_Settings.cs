using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Settings : Panel
{
    public void Save()
    {
        GameManager.instance.Save();
    }

    public void Load()
    {
        GameManager.instance.Load();
    }
}
