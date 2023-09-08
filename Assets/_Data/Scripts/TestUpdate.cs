using System.Collections.Generic;
using UnityEngine;

public class TestUpdate : SaiMonoBehaviour
{
    //[SerializeField] private EnemyHealth enemyHealth;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.B))
    //    {
    //        if (this.enemyHealth == null) return;
    //        this.enemyHealth.TakeDamage(2);
    //    }
    //}

    //[SerializeField] private GameObject prefab;
    //[SerializeField] private Material mats;
    //protected override void LoadComponent()
    //{
    //    base.LoadComponent();
    //    if (this.prefab == null)
    //        this.prefab = Instantiate(Resources.Load<GameObject>(@"Prefabs/Character/Darlene/Character_Darlene"));

    //    if (this.mats == null )
    //        this.mats = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
    //

    //[SerializeField] private SwitchCamera switchCamera;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.W)) 
    //    {
    //        this.switchCamera.SwitchPriority(0);
    //    }     
    //    if (Input.GetKeyDown(KeyCode.A)) 
    //    {
    //        this.switchCamera.SwitchPriority(1);
    //    }     
    //    if (Input.GetKeyDown(KeyCode.D)) 
    //    {
    //        this.switchCamera.SwitchPriority(2);
    //    }
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        this.switchCamera.SwitchPriority(-1);
    //    }
    //}

    private void Start()
    {
        if (UIManager.HasInstance)
        {
            UIManager.Instance.InGamePanel.Show(null);
            UIManager.Instance.InGamePanel.ShowPauseMenu(null);
            UIManager.Instance.LoadingPanel.Hide();
        }
        if (InputManager.HasInstance)
        {
            InputManager.Instance.Enable_Input_MainMenuScene();
        }

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBgm(AUDIO.BGM_MAINMENU_ZANFONAOFDOOM);
        }
    }
}
