using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    public EventSystem eventSystem;

    void Awake () {
        instance = this;
        eventSystem = EventSystem.current;
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

    public void SetCurrentButton (GameObject button)
    {
        eventSystem.SetSelectedGameObject(button.gameObject);
    }
}


