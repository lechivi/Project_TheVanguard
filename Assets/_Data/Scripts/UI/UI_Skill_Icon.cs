using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class UI_Skill_Icon : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private Animator animator;
    [SerializeField] private CanvasGroup readyPanel;
    [SerializeField] private CanvasGroup cooldownPanel;
    [SerializeField] private Image readyIconImage;
    [SerializeField] private Image cooldownIconImage;
    [SerializeField] private Image cooldownFillImage;
    [SerializeField] private TMP_Text cooldownText;

    [Space(10)]
    [SerializeField] private Sprite iconSprite;

    private bool check;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();

        if (this.readyPanel == null)
            this.readyPanel = transform.Find("ReadyPanel").GetComponent<CanvasGroup>();

        if (this.cooldownPanel == null)
            this.cooldownPanel = transform.Find("CooldownPanel").GetComponent<CanvasGroup>();

        if (this.readyIconImage == null)
            this.readyIconImage = this.readyPanel.transform.Find("IconContainer").Find("Icon").GetComponent<Image>();

        if (this.cooldownIconImage == null)
            this.cooldownIconImage = this.cooldownPanel.transform.Find("IconContainer").Find("Icon").GetComponent<Image>();

        if (this.cooldownFillImage == null)
            this.cooldownFillImage = this.cooldownPanel.transform.Find("IconContainer").Find("Fill_Image").GetComponent<Image>();

        if (this.cooldownText == null)
            this.cooldownText = this.cooldownPanel.transform.Find("Cooldown_Text").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        this.SetCooldownFill(false);
    }

    private void Update()
    {
        Character character = PlayerCtrl.Instance.Character;
        if (character.IsCoolingDownSpecicalSkill)
        {
            this.SetCooldownFill(true);
            float time = character.CharacterData.CooldownSkillTime - character.TimerSpecialSkill;
            this.cooldownText.SetText(time.ToString("F1"));
            this.cooldownFillImage.fillAmount = time / character.CharacterData.CooldownSkillTime;
        }

        if (character.IsReadySpecialSkill && this.check)
        {
            this.check = false;
            this.SetCooldownFill(false);
        }

        if (!character.IsReadySpecialSkill && !character.IsCoolingDownSpecicalSkill)
        {
            this.readyIconImage.GetComponent<CanvasGroup>().alpha = 0.3f;
        }
        else
        {
            this.readyIconImage.GetComponent<CanvasGroup>().alpha = 1f;
        }

    }

    private void SetCooldownFill(bool isCoolingDown)
    {
        if (isCoolingDown)
        {
            this.readyPanel.alpha = 0;
            this.cooldownPanel.alpha = 1;
            this.animator.enabled = false;
            this.check = true;
        }
        else
        {
            this.readyPanel.alpha = 1;
            this.cooldownPanel.alpha = 0;
            this.animator.enabled = true;
            this.animator.SetTrigger("Ready");
        }
    }

    public void OnAnimationEvent()
    {
        this.animator.enabled = false;
    }

    public void SetSkill(Sprite iconSprite)
    {
        this.readyIconImage.sprite = iconSprite;
        this.cooldownIconImage.sprite = iconSprite;
    }
}
