public class UI_VictoryPanel : BaseUIElement
{
    public override void Show(object data)
    {
        base.Show(data);
        //play bgm victory
    }

    public void OnClickVillageButton()
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
