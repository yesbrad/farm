using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SOR : Panel
{
    public Button continueButton;

    public override void Slide (bool _isIn)
    {
        continueButton.interactable = SaveData.HasKey(SaveData.c_cropData);
            
        base.Slide(_isIn);
    }

    public void ContinueGame ()
    {
        GameManager.instance.ChangeState(GameState.Game);
        GameManager.instance.Load();
    }

    public void NewGame ()
    {
        GameManager.instance.NewGame();
    }
}
