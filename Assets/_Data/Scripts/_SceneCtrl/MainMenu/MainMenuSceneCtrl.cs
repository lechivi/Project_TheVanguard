using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private SwitchCamera_MM switchCamera;

    public SwitchCamera_MM SwitchCamera { get => this.switchCamera; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.switchCamera == null)
            this.switchCamera = GameObject.Find("SwitchCamera").GetComponent<SwitchCamera_MM>();
    }
}
