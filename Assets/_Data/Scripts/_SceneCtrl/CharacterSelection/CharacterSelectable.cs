using UnityEngine;

public class CharacterSelectable : SaiMonoBehaviour
{
    [SerializeField] private Outline outline;
    [SerializeField] private ParticleSystem selectedFx;
    [SerializeField] private SwitchCamera_ChrSel switchCamera;

    [SerializeField] private int indexCharacter;

    public ParticleSystem SelectedFx { get => this.selectedFx; }
    public int IndexCharacter { get => this.indexCharacter; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.outline == null)
            this.outline = GetComponent<Outline>();

        if (this.selectedFx == null)
            this.selectedFx = GetComponentInChildren<ParticleSystem>();

        if (this.switchCamera == null )
            this.switchCamera = GameObject.Find("SwitchCamera").GetComponent<SwitchCamera_ChrSel>();

    }

    private void OnEnable()
    {
        this.outline.enabled = false;
    }

    private void OnMouseEnter()
    {
        if (this.switchCamera.CurrentIndex == this.switchCamera.IndexMain)
        {
            this.outline.enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if (this.switchCamera.CurrentIndex == this.switchCamera.IndexMain)
        {
            this.outline.enabled = false;
        }
    }

    protected void OnMouseDown()
    {
        if (this.switchCamera.CurrentIndex == this.switchCamera.IndexMain)
        {
            this.outline.enabled = false;
            this.switchCamera.SwitchPriority(this.indexCharacter);
        }
    }


}
