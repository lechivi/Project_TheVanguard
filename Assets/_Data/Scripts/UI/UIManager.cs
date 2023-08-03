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

        this.alwaysOnUICanvas.SetOpenClose(true);
        this.pauseMenuCanvas.SetOpenClose(false);
        this.otherUICanvas.SetOpenClose(false);
    }

    public void SetAlwaysOnUICanvasOpen()
    {
        this.alwaysOnUICanvas.SetOpenClose(true);
        this.pauseMenuCanvas.SetOpenClose(false);
        this.otherUICanvas.SetOpenClose(false);
    }

    public void SetPauseMenuCanvasOpen()
    {
        this.alwaysOnUICanvas.SetOpenClose(false);
        this.pauseMenuCanvas.SetOpenClose(true);
        this.otherUICanvas.SetOpenClose(false);
    }

    public void SetOtherUICanvasOpen()
    {
        this.alwaysOnUICanvas.SetOpenClose(false);
        this.pauseMenuCanvas.SetOpenClose(false);
        this.otherUICanvas.SetOpenClose(true);
    }
}
