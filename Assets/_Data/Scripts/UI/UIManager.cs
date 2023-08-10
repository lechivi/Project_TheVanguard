using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SaiMonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private CanvasCtrl alwaysOnUICanvas;
    [SerializeField] private CanvasCtrl pauseMenuCanvas;
    [SerializeField] private CanvasCtrl otherUICanvas;

    protected override void Awake()
    {
        base.Awake();
        UIManager.Instance = this;

        this.alwaysOnUICanvas.SetAlphaCanvasGroup(true);

        this.pauseMenuCanvas.SetActiveGameobject(false);
        this.otherUICanvas.SetActiveGameobject(false);
    }

    public void SetAlwaysOnUICanvasOpen()
    {
        this.alwaysOnUICanvas.SetAlphaCanvasGroup(true);

        this.pauseMenuCanvas.SetActiveGameobject(false);
        this.otherUICanvas.SetActiveGameobject(false);
    }

    public void SetPauseMenuCanvasOpen()
    {
        this.alwaysOnUICanvas.SetAlphaCanvasGroup(false);

        this.pauseMenuCanvas.SetActiveGameobject(true);
        this.otherUICanvas.SetActiveGameobject(false);
    }

    public void SetOtherUICanvasOpen()
    {
        this.alwaysOnUICanvas.SetAlphaCanvasGroup(false);

        this.pauseMenuCanvas.SetActiveGameobject(false);
        this.otherUICanvas.SetActiveGameobject(true);
    }
}
