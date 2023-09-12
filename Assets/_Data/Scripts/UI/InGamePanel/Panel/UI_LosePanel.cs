using UnityEngine;

public class UI_LosePanel : BaseUIElement
{
    public override void Show(object data)
    {
        base.Show(data);
        //play bgm lose
    }

    public void OnClickVillage()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        if (GameManager.HasInstance)
        {
            GameManager.Instance.TravelToVillage();
        }
    }

    public void OnClickMainMenuButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        if (GameManager.HasInstance)
        {
            GameManager.Instance.MainMenu();
        }
    }

    public void OnClickQuitButton()
    {
        if (GameManager.HasInstance)
        {
            GameManager.Instance.QuitGame();
        }
    }
}
