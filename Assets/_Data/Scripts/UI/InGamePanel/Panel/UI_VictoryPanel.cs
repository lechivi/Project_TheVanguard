public class UI_VictoryPanel : BaseUIElement
{
    public override void Show(object data)
    {
        base.Show(data);
        //play bgm victory
    }

    public void OnClickMainMenuButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        if (GameManager.HasInstance)
        {
            GameManager.Instance.BackToMainMenu();
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
