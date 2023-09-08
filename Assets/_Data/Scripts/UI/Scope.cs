using UnityEngine;
using UnityEngine.UI;

public class Scope : BaseUIElement
{
    [SerializeField] private Image scopeImage;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.scopeImage == null ) 
            this.scopeImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        this.Hide();
    }

    public void SetScope(Image scopeImage)
    {
        this.scopeImage = scopeImage;
    }
}
