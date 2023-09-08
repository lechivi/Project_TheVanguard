using System.Collections.Generic;
using UnityEngine;
using CodeStage.AntiCheat.Storage;

public class AudioManager : BaseManager<AudioManager>
{
    //Separate audio sources for bgm and sfx
    [SerializeField] private AudioSource attachBgmSource;
    [SerializeField] private AudioSource attachSeSource;
    [SerializeField] private AudioSource attachVcSource;

    private Dictionary<string, AudioClip> bgmDic, seDic, vcDic; //Keep all audio

    private bool canPlaySe = true;
    private bool canPlayVc = true;
    private bool isFadeOut = false; //Is the hightlight bgm fading out?
    private float bgmFadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_HIGH;
    private string nextBgmName;
    private string nextSeName;
    private string nextVcName;

    public AudioSource AttachBgmSource { get => this.attachBgmSource; }
    public AudioSource AttachSfxSource { get => this.attachSeSource; }
    public AudioSource AttachVcSource { get => this.attachVcSource; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.attachBgmSource == null)
            this.attachBgmSource = transform.Find("BgmSource").GetComponent<AudioSource>();

        if (this.attachSeSource == null)
            this.attachSeSource = transform.Find("SeSource").GetComponent<AudioSource>();

        if (this.attachVcSource == null)
            this.attachVcSource = transform.Find("VcSource").GetComponent<AudioSource>();
    }

    protected override void Awake()
    {
        base.Awake();
        //Load all bgm & sfx files from resource folder
        this.bgmDic = new Dictionary<string, AudioClip>();
        this.seDic = new Dictionary<string, AudioClip>();
        this.vcDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll(@"Audio/BGM");
        object[] seList = Resources.LoadAll(@"Audio/SE");
        object[] vcList = Resources.LoadAll(@"Audio/VC");

        foreach (AudioClip bgm in bgmList)
        {
            this.bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            this.seDic[se.name] = se;
        }
        foreach (AudioClip vc in vcList)
        {
            this.vcDic[vc.name] = vc;
        }
    }

    private void Start()
    {
        this.SetupAudio();
    }

    private void Update()
    {
        this.HandleChangeBgm();
    }

    private void SetupAudio()
    {
        float masterVolume = ObscuredPrefs.GetFloat(CONST.MAS_VOLUME_KEY, CONST.MAS_VOLUME_DEFAULT);
        this.attachBgmSource.volume = ObscuredPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT) * masterVolume;
        this.attachSeSource.volume = ObscuredPrefs.GetFloat(CONST.SE_VOLUME_KEY, CONST.SE_VOLUME_DEFAULT) * masterVolume;

        bool masterMute = ObscuredPrefs.GetBool(CONST.MAS_MUTE_KEY, CONST.MAS_MUTE_DEFAULT);
        this.attachBgmSource.mute = masterMute ? false : ObscuredPrefs.GetBool(CONST.BGM_MUTE_KEY, CONST.BGM_MUTE_DEFAULT);
        this.attachSeSource.mute = masterMute ? false : ObscuredPrefs.GetBool(CONST.SE_MUTE_KEY, CONST.SE_MUTE_DEFAULT);
    }

    private void HandleChangeBgm()
    {
        if (!this.isFadeOut) return;

        //Gradually lower the volume, and when the volume reaches 0
        //return the volume and play the next song
        this.attachBgmSource.volume -= Time.deltaTime * this.bgmFadeSpeedRate;
        if (this.attachBgmSource.volume <= 0)
        {
            this.attachBgmSource.Stop();
            this.attachBgmSource.volume = ObscuredPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);
            this.isFadeOut = false;

            if (!string.IsNullOrEmpty(this.nextBgmName))
            {
                this.PlayBgm(this.nextBgmName);
            }
        }
    }

    public void PlaySe(AudioClip audio)
    {
        this.attachSeSource.PlayOneShot(audio);
    }

    //public void PlaySe_DelayFirst(string seName, float delay = 0f)
    //{
    //    if (!this.seDic.ContainsKey(seName))
    //    {
    //        Debug.Log($"'{seName}' There is no SE named");
    //        return;
    //    }

    //    this.nextSeName = seName;
    //    Invoke("DelayPlaySe", delay);
    //}
    //private void DelayPlaySe()
    //{
    //    this.attachSeSource.PlayOneShot(this.seDic[this.nextSeName] as AudioClip);
    //}

    public void PlaySe(string seName, float delay = 0f)
    {
        if (!this.seDic.ContainsKey(seName))
        {
            Debug.Log($"'{seName}' There is no SE named");
            return;
        }

        if (this.canPlaySe)
        {
            this.canPlaySe = false;
            this.nextSeName = seName;
            this.attachSeSource.PlayOneShot(this.seDic[this.nextSeName] as AudioClip);
            Invoke("DelayCanPlaySe", delay);
        }
    }

    private void DelayCanPlaySe()
    {
        this.canPlaySe = true;
    }

    public void PlayVc(string vcName, float delay = 0f)
    {
        if (!this.vcDic.ContainsKey(vcName))
        {
            Debug.Log($"'{vcName}' There is no VC named");
            return;
        }

        if (this.canPlayVc)
        {
            this.canPlayVc = false;
            this.nextVcName = vcName;
            this.attachVcSource.PlayOneShot(this.vcDic[this.nextVcName] as AudioClip);
            Invoke("DelayCanPlayVc", delay);
        }
    }

    private void DelayCanPlayVc()
    {
        this.canPlayVc = true;
    }

    public void PlayBgm(string bgmName, float fadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_HIGH)
    {
        if (!this.bgmDic.ContainsKey(bgmName))
        {
            Debug.Log($"'{bgmName}' There is no BGM named");
            return;
        }

        //If bgm is not currently playing, play it as is
        if (!this.attachBgmSource.isPlaying)
        {
            this.nextBgmName = "";
            this.attachBgmSource.clip = this.bgmDic[bgmName] as AudioClip;
            this.attachBgmSource.Play();
        }
        //When a different bgm is playing, fade out the bgm that is playing before playing the next one
        //Throught when the same bgm is playing
        else if (this.attachBgmSource.clip.name != bgmName)
        {
            this.nextBgmName = bgmName;
            this.FadeOutBgm(fadeSpeedRate);
        }
    }

    public void FadeOutBgm(float fadeSpeedRate = CONST.BGM_FADE_SPEED_RATE_LOW)
    {
        this.bgmFadeSpeedRate = fadeSpeedRate;
        this.isFadeOut = true;
    }

    public void ChangeMasterVolume(float masterVolume)
    {
        float multi = Mathf.Clamp01(masterVolume);
        this.attachBgmSource.volume = multi * ObscuredPrefs.GetFloat(CONST.BGM_VOLUME_KEY, CONST.BGM_VOLUME_DEFAULT);
        this.attachSeSource.volume = multi * ObscuredPrefs.GetFloat(CONST.SE_VOLUME_KEY, CONST.SE_VOLUME_DEFAULT);
        ObscuredPrefs.SetFloat(CONST.MAS_VOLUME_KEY, masterVolume);
    }

    public void ChangeBgmVolume(float bgmVolume)
    {
        this.attachBgmSource.volume = bgmVolume * ObscuredPrefs.GetFloat(CONST.MAS_VOLUME_KEY, CONST.MAS_VOLUME_DEFAULT);
        ObscuredPrefs.SetFloat(CONST.BGM_VOLUME_KEY, bgmVolume);
    }

    public void ChangeSeVolume(float seVolume)
    {
        this.attachSeSource.volume = seVolume * ObscuredPrefs.GetFloat(CONST.MAS_VOLUME_KEY, CONST.MAS_VOLUME_DEFAULT);
        ObscuredPrefs.SetFloat(CONST.SE_VOLUME_KEY, seVolume);
    }

    public void ChangeVcVolume(float vcVolume)
    {
        this.attachVcSource.volume = vcVolume * ObscuredPrefs.GetFloat(CONST.MAS_VOLUME_KEY, CONST.MAS_VOLUME_DEFAULT);
        ObscuredPrefs.SetFloat(CONST.VC_VOLUME_KEY, vcVolume);
    }

    public void SetMuteMaster(bool isMute)
    {
        this.attachBgmSource.mute = isMute ? true : ObscuredPrefs.GetBool(CONST.BGM_MUTE_KEY, CONST.BGM_MUTE_DEFAULT);
        this.attachSeSource.mute = isMute ? true : ObscuredPrefs.GetBool(CONST.SE_MUTE_KEY, CONST.SE_MUTE_DEFAULT);
        this.attachVcSource.mute = isMute ? true : ObscuredPrefs.GetBool(CONST.VC_MUTE_KEY, CONST.VC_MUTE_DEFAULT);
        ObscuredPrefs.SetBool(CONST.MAS_MUTE_KEY, isMute);
    }

    public void SetMuteBgm(bool isMute)
    {
        this.attachBgmSource.mute = ObscuredPrefs.GetBool(CONST.MAS_MUTE_KEY, CONST.MAS_MUTE_DEFAULT) ? true : isMute;
        ObscuredPrefs.SetBool(CONST.BGM_MUTE_KEY, isMute);
    }

    public void SetMuteSe(bool isMute)
    {
        this.attachSeSource.mute = ObscuredPrefs.GetBool(CONST.MAS_MUTE_KEY, CONST.MAS_MUTE_DEFAULT) ? true : isMute;
        ObscuredPrefs.SetBool(CONST.SE_MUTE_KEY, isMute);
    }

    public void SetMuteVc(bool isMute)
    {
        this.attachVcSource.mute = ObscuredPrefs.GetBool(CONST.MAS_MUTE_KEY, CONST.MAS_MUTE_DEFAULT) ? true : isMute;
        ObscuredPrefs.SetBool(CONST.VC_MUTE_KEY, isMute);
    }
}
