using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}

public class GraphicTab : BaseUIElement
{
    [Header("GRAPHIC TAB")]
    [SerializeField] private List<ResItem> listResolution = new List<ResItem>();
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    private bool isFullscreen;
    private int currentIndexRes;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.resolutionDropdown == null)
            this.resolutionDropdown = transform.Find("ListLabel/Resolution").GetComponentInChildren<TMP_Dropdown>();

        if (this.fullscreenToggle == null)
            this.fullscreenToggle = transform.Find("ListLabel/DisplayMode").GetComponentInChildren<Toggle>();

        if (this.resolutionDropdown.options.Count != this.listResolution.Count)
        {
            this.SetDropdownOptions();
        }
    }

    private void OnEnable()
    {
        this.DetectFullscreen();
        this.DetectResolution();
    }

    private void DetectFullscreen()
    {
        this.fullscreenToggle.isOn = Screen.fullScreen;
        this.isFullscreen = this.fullscreenToggle.isOn;
    }
    private void DetectResolution()
    {
        bool foundRes = false;
        for (int i = 0; i < this.listResolution.Count; i++)
        {
            if (Screen.width == this.listResolution[i].horizontal && Screen.height == this.listResolution[i].vertical)
            {
                foundRes = true;
                this.currentIndexRes = i;

                this.resolutionDropdown.value = this.currentIndexRes;
                this.resolutionDropdown.RefreshShownValue();
                break;
            }
        }

        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            this.listResolution.Add(newRes);
            this.currentIndexRes = this.listResolution.Count - 1;
            this.SetDropdownOptions();
        }
    }

    private void SetDropdownOptions()
    {
        List<string> options = new List<string>();
        for (int i = 0; i < this.listResolution.Count; i++)
        {
            string resOption = this.listResolution[i].horizontal + "x" + this.listResolution[i].vertical;
            options.Add(resOption);
        }

        this.resolutionDropdown.ClearOptions();
        this.resolutionDropdown.AddOptions(options);
        this.resolutionDropdown.value = this.currentIndexRes;
        this.resolutionDropdown.RefreshShownValue();
    }

    private void SetResolution(int index)
    {
        this.currentIndexRes = index;
        ResItem resItem = this.listResolution[this.currentIndexRes];
        Screen.SetResolution(resItem.horizontal, resItem.vertical, this.isFullscreen);
    }

    public void OnDropdownResolution(int index)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.SetResolution(index);
    }

    public void OnToggelFullscreen(bool isFullscreen)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        this.isFullscreen = isFullscreen;
        Screen.fullScreen = this.isFullscreen;
    }
}
