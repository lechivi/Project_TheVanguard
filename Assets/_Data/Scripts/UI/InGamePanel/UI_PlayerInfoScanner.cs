using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_PlayerInfoScanner : BaseUIElement
{
    [Header("UI_PLAYER INFO SCANNER")]
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    [SerializeField] private LayerMask allyLayer;
    [SerializeField] private LayerMask enemyLayer;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.text == null)
            this.text = transform.Find("Container").Find("TargetName_Text").GetComponent<TMP_Text>();

        if (this.slider == null)
            this.slider = transform.Find("Container").Find("TargetHP_Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        if (PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast() != null)
        {
            this.Show(null);
        }
        else
        {
            this.Hide();
        }
    }

    public override void Show(object data)
    {
        base.Show(data);

        IInfoScanner targetScan = PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast();
        this.text.SetText(targetScan.GetTargetName());
        if (targetScan.GetFactionType() == FactionType.Voidspawn)
        {
            this.fillImage.color = Color.red;
        }
        else if (targetScan.GetFactionType() == FactionType.Alliance)
        {
            this.fillImage.color= Color.green;
        }

        IHealth health = targetScan.GetHealth();
        this.slider.maxValue = health.GetMaxHealth();
        this.slider.value = health.GetCurrentHealth();
    }
}
