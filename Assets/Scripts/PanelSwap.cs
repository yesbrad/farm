using UnityEngine;

public class PanelSwap : MonoBehaviour
{
    public PanelID panelToEnable;
    public PanelID panelToDisable;

    public void Enable ()
    {
        UIManager.instance.EnablePanel(panelToEnable , true);
    }

    public void Disable()
    {
        UIManager.instance.EnablePanel(panelToDisable, false);
    }
}
    