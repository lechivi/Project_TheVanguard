using UnityEngine;

public class ControlTab : BaseUIElement
{
    public void OnClickControlGuideButton()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.ControlGuidePanel.Show(null);
        }
    }
}
