using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerInfoScanner : SaiMonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TMP_Text text;
    [SerializeField] private Slider slider;
    [SerializeField] private Image fillImage;

    [SerializeField] private LayerMask allyLayer;
    [SerializeField] private LayerMask enemyLayer;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.canvasGroup == null)
            this.canvasGroup = GetComponent<CanvasGroup>();

        if (this.text == null)
            this.text = transform.Find("Container").Find("TargetName_Text").GetComponent<TMP_Text>();

        if (this.slider == null)
            this.slider = transform.Find("Container").Find("TargetHP_Slider").GetComponent<Slider>();
    }

    private void Update()
    {
        if (PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast() != null)
        {
            this.Show();
        }
        else
        {
            this.Hide();
        }
    }

    private void Show()
    {
        Transform target = PlayerCtrl.Instance.PlayerInfoScanner.GetInfoScannerObjectByRaycast();
        this.text.SetText(target.name);
        if (this.IsInLayerMask(target.gameObject, this.allyLayer))
        {
            this.fillImage.color = Color.green;
        }
        else if (this.IsInLayerMask(target.gameObject, this.enemyLayer))
        {
            this.fillImage.color = Color.red;
        }

        this.canvasGroup.alpha = 1;
    }

    private void Hide()
    {
        this.canvasGroup.alpha = 0;
    }

    private bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        int objLayer = obj.layer;
        return (layerMask.value & (1 << objLayer)) != 0;
    }

}
