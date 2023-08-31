using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GraphicTab : BaseUIElement
{
    [Header("GRAPHIC TAB")]
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.resolutionDropdown == null )
            this.resolutionDropdown = transform.Find("ListLabel/Resolution").GetComponentInChildren<TMP_Dropdown>();    
        
        if (this.fullscreenToggle == null )
            this.fullscreenToggle = transform.Find("ListLabel/DisplayMode").GetComponentInChildren<Toggle>();
    }
}
