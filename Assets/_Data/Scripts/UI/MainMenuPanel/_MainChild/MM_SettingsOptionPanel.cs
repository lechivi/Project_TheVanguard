using UnityEngine;

public class MM_SettingsOptionPanel : BaseUIElement
{
    [Header("SETTINGS OPTION")]
    [SerializeField] private MainMenuWpPanel mainMenuWpPanel;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainMenuWpPanel == null ) 
            this.mainMenuWpPanel = GetComponentInParent<MainMenuWpPanel>();
    }


    public void OnClickAudioButton()
    {
        this.mainMenuWpPanel.GraphicTab.Hide();
        this.mainMenuWpPanel.ControlTab.Hide();
        this.mainMenuWpPanel.AudioTab.Show(null);
    }  
    
    public void OnClickGraphicButton()
    {
        this.mainMenuWpPanel.AudioTab.Hide();
        this.mainMenuWpPanel.ControlTab.Hide();
        this.mainMenuWpPanel.GraphicTab.Show(null);
    }
    
    public void OnClickControlButton()
    {
        this.mainMenuWpPanel.AudioTab.Hide();
        this.mainMenuWpPanel.GraphicTab.Hide();
        this.mainMenuWpPanel.ControlTab.Show(null);

        if (UIManager.HasInstance)
        {
            UIManager.Instance.MainMenuPanel.ControlGuidePanel.Show(null);
        }
    }   
    
    public void OnClickBackButton()
    {
        this.mainMenuWpPanel.AudioTab.Hide();
        this.mainMenuWpPanel.GraphicTab.Hide();
        this.mainMenuWpPanel.ControlTab.Hide();
        this.mainMenuWpPanel.SettingsOptionPanel.Hide();
        this.mainMenuWpPanel.MenuOptionsPanel.Show(null);
    }   
}
