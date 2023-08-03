using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ItemAttribute : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private TMP_Text attributeNameText;
    [SerializeField] private TMP_Text attributeValueText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private LayoutElement layoutElement;

    public TMP_Text AttributeNameText { get => this.attributeNameText; set => this.attributeNameText = value; }
    public TMP_Text AttributeValueText { get => this.attributeValueText; set => this.attributeValueText = value; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.attributeNameText == null)
        this.attributeNameText = transform.Find("NameText").GetComponent<TMP_Text>();

        if (this.attributeValueText == null)
        this.attributeValueText = transform.Find("ValueText").GetComponent<TMP_Text>();

        if (this.canvasGroup == null)
        this.canvasGroup = GetComponent<CanvasGroup>();

        if (this.layoutElement == null)
        this.layoutElement = GetComponent<LayoutElement>();
    }


    public void SetAttributeText(string name, string value)
    {
        this.attributeNameText.SetText(name);
        this.attributeValueText.SetText(value);
    }

    public void Show()
    {
        this.canvasGroup.alpha = 1;
        this.layoutElement.ignoreLayout = false;
    }

    public void Hide()
    {
        this.canvasGroup.alpha = 0;
        this.layoutElement.ignoreLayout = true;
    }
}
