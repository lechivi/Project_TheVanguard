using CodeStage.AntiCheat.Storage;
using UnityEngine;
using UnityEngine.UI;

public class AudioTab : BaseUIElement
{
    [Header("AUDIO TAB")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;
    [SerializeField] private Slider vcSlider;
    [SerializeField] private Toggle masterMuteToggle;
    [SerializeField] private Toggle bgmMuteToggle;
    [SerializeField] private Toggle seMuteToggle;
    [SerializeField] private Toggle vcMuteToggle;

    private float masterValue;
    private float bgmValue;
    private float seValue;
    private float vcValue;
    private bool isMasterMute;
    private bool isBgmMute;
    private bool isSeMute;
    private bool isVcMute;

    private bool canPlaySe;
    private bool canPlayVc;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.masterSlider == null)
            this.masterSlider = transform.Find("ListLabel/Master").GetComponentInChildren<Slider>();

        if (this.bgmSlider == null)
            this.bgmSlider = transform.Find("ListLabel/BackgroundMusic").GetComponentInChildren<Slider>();

        if (this.seSlider == null)
            this.seSlider = transform.Find("ListLabel/SoundEffect").GetComponentInChildren<Slider>();

        if (this.vcSlider == null)
            this.vcSlider = transform.Find("ListLabel/VoiceCharacter").GetComponentInChildren<Slider>();

        if (this.masterMuteToggle == null)
            this.masterMuteToggle = transform.Find("ListLabel/Master").GetComponentInChildren<Toggle>();

        if (this.bgmMuteToggle == null)
            this.bgmMuteToggle = transform.Find("ListLabel/BackgroundMusic").GetComponentInChildren<Toggle>();

        if (this.seMuteToggle == null)
            this.seMuteToggle = transform.Find("ListLabel/SoundEffect").GetComponentInChildren<Toggle>();

        if (this.vcMuteToggle == null)
            this.vcMuteToggle = transform.Find("ListLabel/VoiceCharacter").GetComponentInChildren<Toggle>();
    }

    private void SetupValueAudio()
    {
        this.masterValue = ObscuredPrefs.GetFloat(CONST.MAS_VOLUME_KEY, CONST.MAS_VOLUME_DEFAULT);
        this.bgmValue = ObscuredPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);
        this.seValue = ObscuredPrefs.GetFloat(CONST.SE_VOLUME_KEY, CONST.SE_VOLUME_DEFAULT);
        this.vcValue = ObscuredPrefs.GetFloat(CONST.VC_VOLUME_KEY, CONST.VC_VOLUME_DEFAULT);
        this.masterSlider.value = this.masterValue;
        this.bgmSlider.value = this.bgmValue;
        this.seSlider.value = this.seValue;
        this.vcSlider.value = this.vcValue;

        this.isMasterMute = ObscuredPrefs.GetBool(CONST.MAS_MUTE_KEY, CONST.MAS_MUTE_DEFAULT);
        this.isBgmMute = ObscuredPrefs.GetBool(CONST.BGM_MUTE_KEY, CONST.BGM_MUTE_DEFAULT);
        this.isSeMute = ObscuredPrefs.GetBool(CONST.SE_MUTE_KEY, CONST.SE_MUTE_DEFAULT);
        this.isVcMute = ObscuredPrefs.GetBool(CONST.VC_MUTE_KEY, CONST.VC_MUTE_DEFAULT);
        this.masterMuteToggle.isOn = this.isMasterMute;
        this.bgmMuteToggle.isOn = this.isBgmMute;
        this.seMuteToggle.isOn = this.isSeMute;
        this.vcMuteToggle.isOn = this.isVcMute;
        this.masterMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = this.isMasterMute ? 0 : 1;
        this.bgmMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = this.isBgmMute ? 0 : 1;
        this.seMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = this.isSeMute ? 0 : 1;
        this.vcMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = this.isVcMute ? 0 : 1;

        this.canPlaySe = false;
        this.canPlayVc = false;
    }

    public override void Show(object data)
    {
        base.Show(data);

        this.SetupValueAudio();
        this.canPlaySe = true;
        this.canPlayVc = true;
    }

    public void OnSliderChangeMasterValue(float value)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeMasterVolume(value);
        }
    }

    public void OnSliderChangeBgmValue(float value)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.ChangeBgmVolume(value);
        }
    }

    public void OnSliderChangeSeValue(float value)
    {
        if (AudioManager.HasInstance && this.canPlaySe)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS, 0.2f);

            AudioManager.Instance.ChangeSeVolume(value);
        }
    }

    public void OnSliderChangeVcValue(float value)
    {
        if (AudioManager.HasInstance && this.canPlayVc)
        {
            AudioManager.Instance.PlayVc(AUDIO.VC_MALE_TAKECARE_SOLDIER_HUNTER_TAKE_CARE, 0.5f);

            AudioManager.Instance.ChangeVcVolume(value);
        }
    }

    public void OnToggelMuteMaster(bool value)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.SetMuteMaster(value);
        }

        this.masterMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = value ? 0 : 1;
    }

    public void OnToggelMuteBgm(bool value)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.SetMuteBgm(value);
        }

        this.bgmMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = value ? 0 : 1;
    }

    public void OnToggelMuteSe(bool value)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.SetMuteSe(value);
        }

        this.seMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = value ? 0 : 1;
    }

    public void OnToggelMuteVc(bool value)
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.SetMuteVc(value);
        }

        this.vcMuteToggle.targetGraphic.GetComponent<CanvasGroup>().alpha = value ? 0 : 1;
    }

    public void OnClickDefaultButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);

            AudioManager.Instance.ChangeMasterVolume(CONST.MAS_VOLUME_DEFAULT);
            AudioManager.Instance.ChangeBgmVolume(CONST.BGM_VOLUME_DEFAULT);
            AudioManager.Instance.ChangeSeVolume(CONST.SE_VOLUME_DEFAULT);
            AudioManager.Instance.ChangeVcVolume(CONST.VC_VOLUME_DEFAULT);
            AudioManager.Instance.SetMuteMaster(CONST.MAS_MUTE_DEFAULT);
            AudioManager.Instance.SetMuteBgm(CONST.BGM_MUTE_DEFAULT);
            AudioManager.Instance.SetMuteSe(CONST.SE_MUTE_DEFAULT);
            AudioManager.Instance.SetMuteVc(CONST.VC_MUTE_DEFAULT);


            this.canPlaySe = false;
            this.canPlayVc = false;
            this.SetupValueAudio();

            this.canPlaySe = true;
            this.canPlayVc = true;
        }
    }
}
