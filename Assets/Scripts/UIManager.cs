using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelID
{
    Inventory,
    HUD,
    Settings,
    SOR
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Panel[] panels;

    void Awake () {
        instance = this;
    }

    public void EnablePanel (PanelID _panelID , bool _enabled)
    {
        for(int i = 0; i < panels.Length; i++)
        {
            if(panels[i].panelID == _panelID)
            {
                panels[i].Slide(_enabled);
            }
        }
    }

    public void LoadGameUI ()
    {
        EnablePanel(PanelID.HUD , true);
    }
}


