using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class OptionItem
{
    public Image Image;
    public TMP_Text Text;

    public void SetColor(Color imageColor, Color textColor)
    {
        this.Image.color = imageColor;
        this.Text.color = textColor;
    }
}

public class OptionSelection : SaiMonoBehaviour
{
    [SerializeField] private List<OptionItem> listOption = new List<OptionItem>();
    [SerializeField] private Color normalImageColor;
    [SerializeField] private Color normalTextColor;
    [SerializeField] private Color selectImageColor;
    [SerializeField] private Color selectTextColor;

    public void SetSelectOption(int index)
    {
        if (this.listOption.Count == 0) return;

        for (int i = 0; i < this.listOption.Count; i++)
        {
            Color imageColor, textColor;
            if (index == i)
            {
                imageColor = this.selectImageColor;
                textColor = this.selectTextColor;
            }
            else
            {
                imageColor = this.normalImageColor;
                textColor = this.normalTextColor;
            }

            this.listOption[i].SetColor(imageColor, textColor);
        }
    }
}
