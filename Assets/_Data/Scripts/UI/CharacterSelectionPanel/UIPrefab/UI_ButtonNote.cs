using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ButtonNote : BaseUIElement
{
    [Header("BUTTON NOTE")]
    [SerializeField] private LayoutElement layoutElement;
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text text;

    public LayoutElement LayoutElement { get => this.layoutElement; }
    public Image Image { get => this.image; }
    public TMP_Text Text { get => this.text; }

    //protected override void LoadComponent()
    //{
    //    base.LoadComponent();
    //    if (this.image == null)
    //        this.image = transform.Find("Image").GetComponent<Image>();

    //    if (this.text == null)
    //        this.text = GetComponentInChildren<TMP_Text>();
    //}


}
