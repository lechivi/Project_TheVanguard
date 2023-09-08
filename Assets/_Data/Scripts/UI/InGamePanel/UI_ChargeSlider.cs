using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChargeSlider : BaseUIElement
{
    [Header("HEALTH BAR")]
    [SerializeField] private List<Color> listColor = new List<Color>();
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.slider == null)
            this.slider = GetComponent<Slider>();

        if (this.fillImage == null)
            this.fillImage = this.slider.fillRect.GetComponent<Image>();
    }

    private void FixedUpdate()
    {
        if (this.slider.value >= 0 && this.slider.value < this.slider.maxValue / 4)
        {
            this.fillImage.color = this.listColor[0];
        }
        else if (this.slider.value >= this.slider.maxValue / 4 && this.slider.value < this.slider.maxValue / 4 * 2)
        {
            this.fillImage.color = Color.Lerp(this.listColor[0], this.listColor[1], 1f);
        }
        else if (this.slider.value >= this.slider.maxValue / 4 * 2 && this.slider.value < this.slider.maxValue / 4 * 3)
        {
            this.fillImage.color = Color.Lerp(this.listColor[1], this.listColor[2], 1f);
        }
        else if (this.slider.value >= this.slider.maxValue / 4 * 3 && this.slider.value < this.slider.maxValue)
        {
            this.fillImage.color = Color.Lerp(this.listColor[2], this.listColor[3], 1f);
        }
        else if (this.slider.value >= this.slider.maxValue)
        {
            this.fillImage.color = this.listColor[4];
        }
    }

    public override void Hide()
    {
        base.Hide();
        this.slider.value = 0;
    }

    public void SetSlider(float value, float max)
    {
        if (value != this.slider.value)
            this.slider.value = value;
        if (max != this.slider.maxValue)
            this.slider.maxValue = max;
    }
}
