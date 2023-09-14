using TMPro;
using UnityEngine;

public class UI_Coin : BaseUIElement
{
    [Header("COIN")]
    [SerializeField] private TMP_Text coinText;

    protected override void LoadComponent()
    {
        base.LoadComponent();

        if (this.coinText == null)
            this.coinText = GetComponentInChildren<TMP_Text>();
    }

    public override void Show(object data)
    {
        base.Show(data);

        if (PlayerCtrl.HasInstance)
        {
            this.coinText.SetText(PlayerCtrl.Instance.PlayerCoin.CurrentCoin.ToString());
        }
    }


}
