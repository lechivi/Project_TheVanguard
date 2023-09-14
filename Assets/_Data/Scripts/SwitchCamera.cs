using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwitchCamera : SaiMonoBehaviour
{
    [SerializeField] protected CinemachineBrain brain;
    [SerializeField] protected CinemachineVirtualCamera vcamMain;
    [SerializeField] protected List<CinemachineVirtualCamera> listVcam = new List<CinemachineVirtualCamera>();

    protected int currentIndex = -1;

    public readonly int IndexMain = -1;

    public int CurrentIndex { get => this.currentIndex; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.brain == null)
            this.brain = GameObject.Find("MainCamera").GetComponent<CinemachineBrain>();

        if (this.vcamMain == null)
            this.vcamMain = GameObject.Find("VCamMain").GetComponent<CinemachineVirtualCamera>();
    }

    protected virtual void OnEnable()
    {
        this.SwitchPriority(this.IndexMain);
    }

    public virtual void SwitchVirtualCamera(int offset)
    {
        this.currentIndex += offset;
        if (this.currentIndex >= this.listVcam.Count)
            this.currentIndex = 0;
        if (this.currentIndex < 0)
            this.currentIndex = this.listVcam.Count - 1;

        this.SwitchPriority(this.currentIndex);
    }

    public virtual void SwitchPriority(int index)
    {
        //Debug.Log(index);
        this.currentIndex = index;

        if (this.vcamMain == null || this.listVcam == null) return;

        if (this.currentIndex == this.IndexMain)
        {
            this.vcamMain.Priority = 10;
            for (int i = 0; i < this.listVcam.Count; i++)
            {
                this.listVcam[i].Priority = 0;
            }
        }
        else if (this.currentIndex > this.IndexMain && this.currentIndex < this.listVcam.Count)
        {
            this.vcamMain.Priority = 0;
            for (int i = 0; i < this.listVcam.Count; i++)
            {
                this.listVcam[i].Priority = this.currentIndex == i ? 10 : 0;
            }
        }
        else
        {
            this.currentIndex = this.IndexMain;
        }
    }

}
