using UnityEngine;
using UnityEngine.UI;

public class AudioTab : BaseUIElement
{
    [Header("AUDIO TAB")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Toggle masterToggle;
    [SerializeField] private Toggle bgmToggle;
    [SerializeField] private Toggle sfxToggle;

    private float masterValue;
    private float bgmValue;
    private float sfxValue;
    private bool isMasterMute;
    private bool isBgmMute;
    private bool isSfxMute;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.masterSlider == null)
            this.masterSlider = transform.Find("ListLabel/Master").GetComponentInChildren<Slider>();

        if (this.bgmSlider == null)
            this.bgmSlider = transform.Find("ListLabel/BackgroundMusic").GetComponentInChildren<Slider>();

        if (this.sfxSlider == null)
            this.sfxSlider = transform.Find("ListLabel/SoundEffect").GetComponentInChildren<Slider>();    

        if (this.masterToggle == null)
            this.masterToggle = transform.Find("ListLabel/Master").GetComponentInChildren<Toggle>();

        if (this.bgmToggle == null)
            this.bgmToggle = transform.Find("ListLabel/BackgroundMusic").GetComponentInChildren<Toggle>();

        if (this.sfxToggle == null)
            this.sfxToggle = transform.Find("ListLabel/SoundEffect").GetComponentInChildren<Toggle>();
    }

    //private void OnEnable()
    //{
    //    this.SetupValue();
    //}

    //private void SetupValue()
    //{
    //    if (AudioManager.HasInstance)
    //    {
    //        this.bgmValue = AudioManager.Instance.AttachBGMSource.volume;
    //        this.sfxValue = AudioManager.Instance.AttachSFXSource.volume;
    //        this.bgmSlider.value = this.bgmValue;
    //        this.sfxSlider.value = this.sfxValue;

    //        this.isBGMMute = AudioManager.Instance.AttachBGMSource.mute;
    //        this.isSFXMute = AudioManager.Instance.AttachSFXSource.mute;
    //        this.bgmMute.isOn = this.isBGMMute;
    //        this.sfxMute.isOn = this.isSFXMute;
    //    }
    //}

    public void OnSliderChangeMasterValue(float value)
    {
        this.masterValue = value;
    }

    public void OnSliderChangeBgmValue(float value)
    {
        this.bgmValue = value;
    }

    public void OnSliderChangeSfxValue(float value)
    {
        this.sfxValue = value;
    }

    public void OnToggelMuteMaster(bool value)
    {
        this.isMasterMute = value;
    }

    public void OnToggelMuteBgm(bool value)
    {
        this.isBgmMute = value;
    }

    public void OnToggelMuteSfx(bool value)
    {
        this.isSfxMute = value;
    }

    public void OnClickSubmitButton()
    {
        Debug.Log("Click Submit button");
    }

    public void OnClickDefaultButton()
    {
        Debug.Log("Click Default button");
    }

    //public void OnClickedCancelButton()
    //{
    //    if (AudioManager.HasInstance)
    //    {
    //        AudioManager.Instance.PlaySFX(AUDIO.SFX_BUTTON);
    //    }

    //    if (UIManager.HasInstance)
    //    {
    //        UIManager.Instance.ActiveSettingPanel(false);
    //    }

    //    if (GameManager.HasInstance)
    //    {
    //        if (!GameManager.Instance.IsPlaying && !UIManager.Instance.MenuPanel.gameObject.activeSelf)
    //        {
    //            UIManager.Instance.ActivePausePanel(true);
    //        }
    //    }
    //}

    //public void OnClickedSubmitButton()
    //{
    //    if (AudioManager.HasInstance)
    //    {
    //        AudioManager.Instance.PlaySFX(AUDIO.SFX_BUTTON);
    //    }

    //    if (AudioManager.HasInstance)
    //    {
    //        AudioManager.Instance.ChangeBGMVolume(this.bgmValue);
    //        AudioManager.Instance.ChangeSFXVolume(this.sfxValue);
    //        AudioManager.Instance.MuteBGM(this.isBGMMute);
    //        AudioManager.Instance.MuteSFX(this.isSFXMute);
    //    }

    //    if (UIManager.HasInstance)
    //    {
    //        UIManager.Instance.SettingPanel.SettingMainMenu(false);
    //        UIManager.Instance.ActiveSettingPanel(false);
    //    }

    //    if (GameManager.HasInstance)
    //    {
    //        if (!GameManager.Instance.IsPlaying && !UIManager.Instance.MenuPanel.gameObject.activeSelf)
    //        {
    //            UIManager.Instance.ActivePausePanel(true);
    //        }
    //    }
    //}
}
