using UnityEngine;
using Cinemachine;

public class SwitchCamera_MM : SwitchCamera
{
    [SerializeField] private CinemachineVirtualCamera vcamBook;

    public readonly int IndexBook = -2;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.vcamBook == null )
            this.vcamBook = GameObject.Find("VCamBook").GetComponent<CinemachineVirtualCamera>();

        if (this.listVcam.Count == 0)
        {
            this.listVcam.Add(GameObject.Find("VCam_Front").GetComponent<CinemachineVirtualCamera>());
            this.listVcam.Add(GameObject.Find("VCam_Left").GetComponent<CinemachineVirtualCamera>());
            this.listVcam.Add(GameObject.Find("VCam_Right").GetComponent<CinemachineVirtualCamera>());
        }
    }

    public override void SwitchPriority(int index)
    {
        base.SwitchPriority(index);

        if (index == -2)
        {
            this.vcamBook.Priority = 10;
            this.vcamMain.Priority = 0;
            foreach (var vcam in this.listVcam)
            {
                vcam.Priority = 0;
            }
            this.currentIndex = index;
        }
        else
        {
            this.vcamBook.Priority = 0;
        }
    }
}
