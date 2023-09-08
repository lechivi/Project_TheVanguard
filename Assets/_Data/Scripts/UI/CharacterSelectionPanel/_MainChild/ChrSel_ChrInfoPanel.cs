using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChrSel_ChrInfoPanel : BaseUIElement
{
    [Header("CHR INFO PANEL")]
    [SerializeField] private CharacterSelectionPanel chrSelPanel;
    [SerializeField] private TMP_Text chrName_Text;
    [SerializeField] private TMP_Text chrClass_Text;
    [SerializeField] private TMP_Text chrSpecies_Text;
    [SerializeField] private TMP_Text chrDescription_Text;

    [Space(10)]
    [SerializeField] private Slider hp_Slider;
    [SerializeField] private Slider power_Slider;
    [SerializeField] private Slider defence_Slider;

    [Space(10)]
    [SerializeField] private Image skill_Image;
    [SerializeField] private TMP_Text skillName_Text;
    [SerializeField] private TMP_Text skillDescription_Text;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.chrSelPanel == null ) 
            this.chrSelPanel = GetComponentInParent<CharacterSelectionPanel>();

        if (this.chrName_Text == null)
            this.chrName_Text = transform.Find("TOP/ChrName_Text").GetComponent<TMP_Text>();

        if (this.chrClass_Text == null)
            this.chrClass_Text = transform.Find("TOP/ChrClass_Text").GetComponent<TMP_Text>();

        if (this.chrSpecies_Text == null)
            this.chrSpecies_Text = transform.Find("MID/MID_RIGHT/ChrSpecies_Text").GetComponent<TMP_Text>();

        if (this.chrDescription_Text == null)
            this.chrDescription_Text = transform.Find("MID/MID_RIGHT/ChrDescription_Text").GetComponent<TMP_Text>();

        if (this.hp_Slider == null)
            this.hp_Slider = transform.Find("MID/MID_LEFT/HPStat").GetComponentInChildren<Slider>();

        if (this.power_Slider == null)
            this.power_Slider = transform.Find("MID/MID_LEFT/PowerStat").GetComponentInChildren<Slider>();

        if (this.defence_Slider == null)
            this.defence_Slider = transform.Find("MID/MID_LEFT/DefenceStat").GetComponentInChildren<Slider>();

        if (this.skill_Image == null)
            this.skill_Image = transform.Find("BOT/Skill_Image").GetComponent<Image>();

        if (this.skillName_Text == null)
            this.skillName_Text = transform.Find("BOT/SkillName_Text").GetComponent<TMP_Text>();

        if (this.skillDescription_Text == null)
            this.skillDescription_Text = transform.Find("BOT/SkillDescription_Text").GetComponent<TMP_Text>();
    }

    public void DisplayChrInfo(CharacterDataSO characterData)
    {
        this.chrName_Text.SetText(characterData.CharacterName.ToUpper());
        this.chrClass_Text.SetText(characterData.CharacterClass.ToString());
        this.chrSpecies_Text.SetText("Species: " + characterData.Species.ToString());
        this.chrDescription_Text.SetText(characterData.CharacterDescription.ToString());

        this.hp_Slider.value = (float) characterData.HitPoint / 10;
        this.power_Slider.value = (float) characterData.Power / 10;
        this.defence_Slider.value = (float) characterData.Defence / 10;

        this.skill_Image.sprite = characterData.SpecialSkillIcon;
        this.skillName_Text.SetText(characterData.SpecialSkillName);
        this.skillDescription_Text.SetText(characterData.SpecialSkillDescription);
    }


    public void OnClickNextButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        CharacterSelectionSceneCtrl chrSelSceneCtrl = this.chrSelPanel.ChrSelSceneCtrl;
        if (chrSelSceneCtrl == null) return;

        chrSelSceneCtrl.SwitchCamera.SwitchVirtualCamera(1);
    }

    public void OnClickPrevButton()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_BTN_CLICKS);
        }

        CharacterSelectionSceneCtrl chrSelSceneCtrl = this.chrSelPanel.ChrSelSceneCtrl;
        if (chrSelSceneCtrl == null) return;

        chrSelSceneCtrl.SwitchCamera.SwitchVirtualCamera(-1);
    }
}
