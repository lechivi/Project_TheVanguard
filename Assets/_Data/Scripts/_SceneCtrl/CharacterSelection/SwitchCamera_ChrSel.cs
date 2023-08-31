using UnityEngine;
using Cinemachine;

public class SwitchCamera_ChrSel : SwitchCamera
{
    [SerializeField] private CharacterSelectionSceneCtrl chrSelSceneCtrl;

    private bool isCameraMoving = false;
    private int intCheck = 0;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.chrSelSceneCtrl == null)
            this.chrSelSceneCtrl = GameObject.Find("CharacterSelectionSceneCtrl").GetComponent<CharacterSelectionSceneCtrl>();

        if (this.listVcam.Count == 0)
        {
            this.listVcam.Add(GameObject.Find("VCam_Darlene").GetComponent<CinemachineVirtualCamera>());
            this.listVcam.Add(GameObject.Find("VCam_Xerath").GetComponent<CinemachineVirtualCamera>());
            this.listVcam.Add(GameObject.Find("VCam_Sera").GetComponent<CinemachineVirtualCamera>());
            this.listVcam.Add(GameObject.Find("VCam_Juggernaut").GetComponent<CinemachineVirtualCamera>());
            this.listVcam.Add(GameObject.Find("VCam_Ironstone").GetComponent<CinemachineVirtualCamera>());
        }
    }

    private void Update()
    {
        this.DisplayCharacterSelectionPanel();
    }

    private void DisplayCharacterSelectionPanel()
    {
        if (UIManager.HasInstance)
        {
            CharacterSelectionPanel chrSelPanel = UIManager.Instance.ChrSelPanel;

            if (this.brain.IsBlending == true && this.intCheck == 0)
            {
                this.intCheck = 1;
                this.isCameraMoving = true;
                chrSelPanel.Hide();
            }

            if (this.isCameraMoving == true)
            {
                if (this.brain.IsBlending == false)
                {
                    this.isCameraMoving = false;
                    this.intCheck = 0;
                    this.chrSelSceneCtrl.CheckUI_ChrInfoPanel(this.currentIndex);

                    chrSelPanel.Show(null);
                    chrSelPanel.DisplayChrInfo(this.currentIndex);
                }
            }
        }
    }

}
