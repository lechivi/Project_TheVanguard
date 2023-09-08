using UnityEngine;

public class PopoutContainer : BaseUIElement
{
    [Header("POPOUT CONTAINER")]
    [SerializeField] private CanvasGroup mm_PlayPopout;
    [SerializeField] private CanvasGroup mm_QuitPopout;
    [SerializeField] private CanvasGroup cs_StartPopout;
    [SerializeField] private CanvasGroup ig_MainMenuPopout;
    [SerializeField] private CanvasGroup ig_QuitPopout;

    protected virtual void OnEnable()
    {
        this.Hide();
    }

    public override void Show(object data)
    {
        base.Show(data);
        if (InputManager.HasInstance)
        {
            if (InputManager.Instance.Input_ChrSelScene.enabled)
            {
                InputManager.Instance.Input_ChrSelScene.CanSelect = false;

            }
        }

    }

    public override void Hide()
    {
        base.Hide();
        this.HideAllPopout();
        if (InputManager.HasInstance)
        {
            if (InputManager.Instance.Input_ChrSelScene.enabled)
            {
                InputManager.Instance.Input_ChrSelScene.CanSelect = true;

            }
        }
    }

    private void HideAllPopout()
    {
        this.SetActiveCanvasGroup(this.mm_PlayPopout, false);
        this.SetActiveCanvasGroup(this.mm_QuitPopout, false);
        this.SetActiveCanvasGroup(this.cs_StartPopout, false);
        this.SetActiveCanvasGroup(this.ig_MainMenuPopout, false);
        this.SetActiveCanvasGroup(this.ig_QuitPopout, false);
    }

    protected virtual void SetActiveCanvasGroup(CanvasGroup canvasGroup, bool isActive)
    {
        canvasGroup.alpha = isActive ? 1 : 0;
        canvasGroup.blocksRaycasts = isActive;
    }

    public void ShowMM_StartPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.mm_PlayPopout, true);
    }
    public void ShowMM_QuitPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.mm_QuitPopout, true);
    }
    public void ShowCS_StartPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.cs_StartPopout, true);
    }
    public void ShowIG_MainMenuPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.ig_MainMenuPopout, true);
    }
    public void ShowIG_QuitPopout()
    {
        this.Show(null);
        this.SetActiveCanvasGroup(this.ig_QuitPopout, true);
    }

    public virtual void OnClickNoButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.Hide();
    }

    public virtual void OnClickPlay_YesButton() //MainMenu
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.Hide();

        if (GameManager.HasInstance)
        {
            GameManager.Instance.StartGame();
        }
    }

    public virtual void OnClickMainMenu_YesButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.Hide();

        if (GameManager.HasInstance)
        {
            GameManager.Instance.BackToMainMenu();
        }
    }

    public virtual void OnClickStart_YesButton() //Character Selection
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.Hide();

        if (GameManager.HasInstance)
        {
            StartCoroutine(GameManager.Instance.LoadScene(2));
        }
    }

    public virtual void OnClickQuit_YesButton()
    {
        this.Hide();

        if (GameManager.HasInstance)
        {
            GameManager.Instance.QuitGame();
        }
    }
}
