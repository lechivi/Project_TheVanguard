using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Skill : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private Animator animator;
    [SerializeField] private CanvasGroup readyPanel;
    [SerializeField] private CanvasGroup executionPanel;
    [SerializeField] private CanvasGroup cooldownPanel;
    [SerializeField] private Image readyIconImage;
    [SerializeField] private Image executionFillIconImage;
    [SerializeField] private Image cooldownIconImage;
    [SerializeField] private Image cooldownFillImage;
    [SerializeField] private TMP_Text timerText;

    [Space(10)]
    [SerializeField] private Sprite iconSprite;

    private bool check1;
    private bool check2;
    private bool check3;
    private float timerExecution;
    private SkillPhase currentPhase;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.animator == null)
            this.animator = GetComponent<Animator>();

        if (this.readyPanel == null)
            this.readyPanel = transform.Find("ReadyPanel").GetComponent<CanvasGroup>();

        if (this.executionPanel == null)
            this.executionPanel = transform.Find("ExecutionPanel").GetComponent<CanvasGroup>();

        if (this.cooldownPanel == null)
            this.cooldownPanel = transform.Find("CooldownPanel").GetComponent<CanvasGroup>();

        if (this.readyIconImage == null)
            this.readyIconImage = this.readyPanel.transform.Find("IconContainer").Find("Icon").GetComponent<Image>();

        if (this.executionFillIconImage == null)
            this.executionFillIconImage = this.executionPanel.transform.Find("IconContainer").Find("Fill_Icon").GetComponent<Image>();

        if (this.cooldownIconImage == null)
            this.cooldownIconImage = this.cooldownPanel.transform.Find("IconContainer").Find("Icon").GetComponent<Image>();

        if (this.cooldownFillImage == null)
            this.cooldownFillImage = this.cooldownPanel.transform.Find("IconContainer").Find("Fill_Image").GetComponent<Image>();

        if (this.timerText == null)
            this.timerText = transform.Find("Timer_Text").GetComponent<TMP_Text>();
    }

    private void Start()
    {
        this.SetPhase(SkillPhase.Ready);
    }

    private void Update()
    {
        Character character = PlayerCtrl.Instance.Character;

        if (character.IsReadySpecialSkill && this.check1)
        {
            this.check1 = false;
            this.SetPhase(SkillPhase.Ready);
        }
        if (!character.IsReadySpecialSkill && !character.IsCoolingDownSpecicalSkill && this.check2)
        {
            this.check2 = false;
            this.SetPhase(SkillPhase.Execution);
        }
        if (character.IsCoolingDownSpecicalSkill && this.check3)
        {
            this.check3 = false;
            this.SetPhase(SkillPhase.Cooldown);
        }

        if (this.currentPhase == SkillPhase.Execution)
        {
            this.timerExecution -= Time.deltaTime;
            this.timerText.SetText((character.CharacterData.ExecutionSkillTime + this.timerExecution).ToString("F1"));
            this.executionFillIconImage.fillAmount = this.timerExecution / character.ExecutionSpecialSkill;
        }
        else if (this.currentPhase == SkillPhase.Cooldown)
        {
            float time = character.CooldownSpecialSkill - character.TimerCD_SpecialSkill;
            this.timerText.SetText(time.ToString("F1"));
            this.cooldownFillImage.fillAmount = time / character.CooldownSpecialSkill;
            if (time <= 0.01f)
            {
                this.animator.enabled = true;
                this.animator.SetTrigger("Ready");
            }
        }
    }

    private void SetPhase(SkillPhase phase)
    {
        this.currentPhase = phase;

        if (phase == SkillPhase.Ready)
        {
            this.readyPanel.alpha = 1;
            this.executionPanel.alpha = 0;
            this.cooldownPanel.alpha = 0;
            this.timerText.GetComponent<CanvasGroup>().alpha = 0;
            this.check2 = true;
            this.check3 = true;
        }
        else if (phase == SkillPhase.Execution)
        {
            this.readyPanel.alpha = 0;
            this.executionPanel.alpha = 1;
            this.cooldownPanel.alpha = 0;
            this.timerText.GetComponent<CanvasGroup>().alpha = 0.75f;
            this.timerExecution = PlayerCtrl.Instance.Character.ExecutionSpecialSkill;
            this.animator.enabled = true;
            this.animator.SetTrigger("Execution");
            this.check1 = true;
            this.check3 = true;
        }
        else
        {
            this.readyPanel.alpha = 0;
            this.executionPanel.alpha = 0;
            this.cooldownPanel.alpha = 1;
            this.timerText.GetComponent<CanvasGroup>().alpha = 1;
            this.check1 = true;
            this.check2 = true;
        }
    }

    public void OnAnimationEvent()
    {
        this.animator.enabled = false;
    }

    public void SetSkill(Sprite iconSprite)
    {
        this.readyIconImage.sprite = iconSprite;
        this.executionFillIconImage.sprite = iconSprite;
        this.cooldownIconImage.sprite = iconSprite;
    }
}
