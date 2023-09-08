using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerInfoScanner : BaseUIElement
{
    [Header("UI_PLAYER INFO SCANNER")]
    [SerializeField] private InGame_AlwaysOnUi alwaysOnUi;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    [SerializeField] private Color allyColor;
    [SerializeField] private Color enemyColor;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.alwaysOnUi == null)
            this.alwaysOnUi = GetComponentInParent<InGame_AlwaysOnUi>();

        if (this.text == null)
            this.text = transform.Find("Container/TargetName_Text").GetComponent<TMP_Text>();

        if (this.slider == null)
            this.slider = transform.Find("Container/TargetHP_Slider").GetComponent<Slider>();

        if (this.fillImage == null)
            this.fillImage = transform.Find("Container/TargetHP_Slider/Fill Area/Fill").GetComponent<Image>();
    }

    private void Update()
    {
        if (PlayerCtrl.HasInstance)
        {
            if (PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast() != null)
            {
                this.Show(null);
            }
            else
            {
                this.Hide();
                this.alwaysOnUi.Crosshair.SetCrosshairTarget(FactionType.Unknow);
            }
        }
        else
        {
            this.Hide();
            this.alwaysOnUi.Crosshair.SetCrosshairTarget(FactionType.Unknow);
        }
    }

    public override void Show(object data)
    {
        base.Show(data);

        if (PlayerCtrl.HasInstance)
        {
            IInfoScanner targetScan = PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast();
            this.text.SetText(targetScan.GetTargetName());
            if (targetScan.GetFactionType() == FactionType.Voidspawn)
            {
                this.fillImage.color = this.enemyColor;
                this.alwaysOnUi.Crosshair.SetCrosshairTarget(FactionType.Voidspawn);
            }
            else if (targetScan.GetFactionType() == FactionType.Alliance)
            {
                this.fillImage.color = this.allyColor;
                this.alwaysOnUi.Crosshair.SetCrosshairTarget(FactionType.Alliance);
            }

            IHealth health = targetScan.GetHealth();
            this.slider.maxValue = health.GetMaxHealth();
            this.slider.value = health.GetCurrentHealth();
        }
    }
}
