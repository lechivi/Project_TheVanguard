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

    private void OnEnable()
    {
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.Register(ListenType.UpdatePlayerHealth, this.SetSlider);
            Debug.Log("Register");
        }
    }

    //private void OnDisable()
    //{
    //    if (ListenerManager.HasInstance)
    //    {
    //        ListenerManager.Instance.Unregister(ListenerType.UpdatePlayerHealth, this.SetSlider);
    //        Debug.Log("Unregister");
    //    }
    //}
    //private void FixedUpdate()
    //{
    //    if (PlayerCtrl.HasInstance)
    //    {
    //        PlayerHealth health = PlayerCtrl.Instance.PlayerHealth;
    //        this.SetSlider(health.GetCurrentHealth(), health.GetMaxHealth());
    //    }
    //}

    public void Hit(Component sender, object data)
    {
        if (sender is PlayerHealth)
        {
            PlayerHealth playerHealth = sender as PlayerHealth;
            if (playerHealth == null) return;

            this.slider.maxValue = playerHealth.GetMaxHealth();
            this.slider.value = playerHealth.GetCurrentHealth();
        }
    }

    public void SetSlider(object value)
    {
        Debug.Log("assssssssss");
        if (value == null) return;

        if (value is PlayerHealth playerHealth)
        {
            int cur = playerHealth.GetCurrentHealth();
            int max = playerHealth.GetMaxHealth();
            if (cur != this.slider.value)
                this.slider.value = cur;
            if (max != this.slider.maxValue)
                this.slider.maxValue = max;

        }
    }

    public void OnSliderChangeValue(float value)
    {
        this.valueText.SetText(((int)value).ToString());
    }

}
