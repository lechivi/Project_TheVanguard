using UnityEngine;

public class ControlTab : BaseUIElement
{
    public void OnClickOpenGamepadGuideButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_GUIDEOPEN_SCROLLOPEN);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.ControlGuidePanel.Show(0);
        }
    }   
    
    public void OnClickOpenKeyboardGuideButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_GUIDEOPEN_SCROLLOPEN);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.ControlGuidePanel.Show(1);
        }
    }
}
