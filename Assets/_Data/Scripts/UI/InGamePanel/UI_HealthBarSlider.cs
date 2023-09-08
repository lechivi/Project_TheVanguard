using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBarSlider : BaseUIElement
{
    [Header("HEALTH BAR")]
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text valueText;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.slider == null)
            this.slider = GetComponent<Slider>();

        if (this.valueText == null)
            this.valueText = GetComponentInChildren<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if (PlayerCtrl.HasInstance)
        {
            PlayerHealth health = PlayerCtrl.Instance.PlayerHealth;
            this.SetSlider(health.GetCurrentHealth(), health.GetMaxHealth());
        }
    }

    public void SetSlider(int value, int max)
    {
        if (value != this.slider.value)
            this.slider.value = value;
        if (max != this.slider.maxValue)
            this.slider.maxValue = max;
    }

    public void OnSliderChangeValue(float value)
    {
        this.valueText.SetText(((int)value).ToString());
    }

}
