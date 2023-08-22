using UnityEngine;

public class BaseUIElement : SaiMonoBehaviour
{
    [Header("BASE UI")]
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected bool isSetActiveGameObjec;

    protected bool isHide;
    protected bool isInited;

    public CanvasGroup CanvasGroup { get => this.canvasGroup; }
    public bool IsHide { get => this.isHide; }
    public bool IsInited { get => this.isInited; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.canvasGroup == null)
            this.canvasGroup = GetComponent<CanvasGroup>();
    }

    public virtual void Init()
    {
        this.isInited = true;
        if (!this.gameObject.GetComponent<CanvasGroup>())
        {
            this.gameObject.AddComponent<CanvasGroup>();
        }

        this.canvasGroup = this.gameObject.GetComponent<CanvasGroup>();
        this.gameObject.SetActive(true);
    }

    public virtual void Show(object data)
    {
        if (this.isSetActiveGameObjec)
            this.gameObject.SetActive(true);

        this.isHide = false;
        this.SetActiveCanvasGroup(true);
    }

    public virtual void Hide()
    {
        if (this.isSetActiveGameObjec)
            this.gameObject.SetActive(false);

        this.isHide = true;
        this.SetActiveCanvasGroup(false);
    }

    protected void SetActiveCanvasGroup(bool isActive)
    {
        if (this.canvasGroup != null)
        {
            this.canvasGroup.alpha = isActive ? 1 : 0;
            this.canvasGroup.blocksRaycasts = isActive;
        }
    }
}
