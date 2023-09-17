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

    //private void Start()
    //{
    //    if (UIManager.HasInstance)
    //    {
    //        UIManager.Instance.InGamePanel.Show(null);
    //        UIManager.Instance.InGamePanel.ShowPauseMenu(null);
    //        UIManager.Instance.LoadingPanel.Hide();
    //    }
    //    if (InputManager.HasInstance)
    //    {
    //        InputManager.Instance.Enable_Input_MainMenuScene();
    //    }

    //    if (AudioManager.HasInstance)
    //    {
    //        AudioManager.Instance.PlayBgm(AUDIO.BGM_MAINMENU_ZANFONAOFDOOM);
    //    }
    //}

    //public int damge = 10;
    //public int Constant = 50;

    //protected override void LoadComponent()
    //{
    //    base.LoadComponent();
    //    for (int i = 1; i <= 10; i++) 
    //    {
    //        int damageReduction = (int) ((float) i / (i + this.Constant) * this.damge);
    //        Debug.Log ("DEF_" + i + ": -" +  damageReduction + " => " + (int) (damge - damageReduction));
    //    }
    //}

    //public RagdollCtrl RagdollCtrl;

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.O))
    //    {
    //        this.RagdollCtrl.EnableRagdoll();
    //    }
    //    if (Input.GetKeyDown(KeyCode.L)) 
    //    {
    //        this.RagdollCtrl.DisableRagdoll() ;
    //    }
    //}

    //public MovingPlatform MovingPlatform;

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.J)) 
    //    {
    //        this.MovingPlatform.TriggerMove();
    //    }
    //}

    //public GameEvent OnPlayerHealthChanged;

    //protected override void LoadComponent()
    //{
    //    base.LoadComponent();
    //    this.OnPlayerHealthChanged.Raise(this, 10);
    //}

    //public void ShowInt(Component sender, object data)
    //{
    //    Debug.Log("Call");
    //    if (sender is TestUpdate)
    //    {
    //        int index = (int)data;
    //        Debug.Log("-" + data.ToString() + "HP");
    //    }
    //}

    //public GameEventListener gameEventListener;

    //private void Update()
    //{
    //    this.gameEventListener.Response.AddListener(this.Add);
    //}

    //private void Add(Component sender, object data)
    //{
    //}

    //private void Start()
    //{
    //    // Add a listener to the Response event
    //    gameEventListener.Response.AddListener(YourCustomMethod);
    //}

    //private void YourCustomMethod(Component sender, object data)
    //{
    //    // Handle the event here
    //    Debug.Log("Event received from " + sender + " with data: " + data);
    //}

    public int max = 10;
    public int cur;

    private void Start()
    {
        this.cur = this.max;
        if (ListenerManager.HasInstance)
        {
            ListenerManager.Instance.BroadCast(ListenType.UpdatePlayerHealth, this);
            Debug.Log("cast");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            this.cur -= 1;
            ListenerManager.Instance.BroadCast(ListenType.UpdatePlayerHealth, this);
            Debug.Log("cast");
        }
    }
}
