using UnityEngine;

public class MenuBookClose : SaiMonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Outline outline;
    [SerializeField] private MainMenuSceneCtrl mainMenuSceneCtrl;

    private bool canClick = true;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();

        if (this.outline == null)
            this.outline = GetComponent<Outline>();

        if (this.mainMenuSceneCtrl == null)
            this.mainMenuSceneCtrl = GameObject.Find("MainMenuSceneCtrl").GetComponent<MainMenuSceneCtrl>();
    }

    private void OnEnable()
    {
        this.outline.enabled = false;
        this.canClick = true;
    }

    private void OnMouseEnter()
    {
        this.outline.enabled = true;
    }

    private void OnMouseExit()
    {
        this.outline.enabled = false;
    }

    private void OnMouseDown()
    {
        if (this.canClick)
        {
            this.canClick = false;

            this.animator.SetTrigger("Open");

            SwitchCamera_MM switchCamera = this.mainMenuSceneCtrl.SwitchCamera;
            switchCamera.SwitchPriority(switchCamera.IndexBook);
        }

    }

    public void OnAnimationOpenBook()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_BOOK_00_PAGE1);
        }

        this.mainMenuSceneCtrl.SetBook(true);
    }
}
