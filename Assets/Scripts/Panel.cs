using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public PanelID panelID;
    public Animator animatorPanel;
    public string transitionIdIn = "SlideIn";
    public string transitionIdOut = "SlideOut";

    public virtual void Slide (bool _isIn)
    {
        animatorPanel.SetTrigger(_isIn ? transitionIdIn : transitionIdOut);
    }
}
