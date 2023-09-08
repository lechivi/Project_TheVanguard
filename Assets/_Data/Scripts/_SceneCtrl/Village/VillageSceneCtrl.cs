using UnityEngine;
using Cinemachine;

public class VillageSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private Transform startTransform1;
    [SerializeField] private Transform startTransform2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform interactableRaycastObject;
    [SerializeField] private GameObject ignoreRaycastZone;
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private CinemachineFreeLook tpsCamera;

    private bool isTutorial;

    public bool IsTutorial { get => this.isTutorial; set => this.isTutorial = value; }
    public Camera MainCamer { get => this.mainCamera; }
    public Transform InteractableRaycastObject { get => this.interactableRaycastObject; }
    public GameObject IgnoreRaycastZone { get => this.ignoreRaycastZone; }
    public CinemachineVirtualCamera FPSCamera { get => this.fpsCamera; }
    public CinemachineFreeLook TPSCamera { get => this.tpsCamera; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainCamera == null)
            this.mainCamera = Camera.main;

        if (this.interactableRaycastObject == null)
            this.interactableRaycastObject = this.mainCamera.transform.Find("InteractableRaycastObject");

        if (this.ignoreRaycastZone == null)
            this.ignoreRaycastZone = this.mainCamera.transform.Find("IgnoreRaycastZone").gameObject;

        if (this.fpsCamera == null)
            this.fpsCamera = GameObject.Find("FPSCamera").GetComponent<CinemachineVirtualCamera>();

        if (this.tpsCamera == null)
            this.tpsCamera = GameObject.Find("TPSCamera").GetComponent<CinemachineFreeLook>();
    }

    private void Start()
    {
        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlayBgm(AUDIO.BGM_VILLAGE_TAVERN_LAROULETTE);
        }

        if (InputManager.HasInstance)
        {
            InputManager.Instance.Disable_Input_All();
        }

        if (GameManager.HasInstance)
        {
            PlayerCamera playerCamera = GameManager.Instance.PlayerCtrl.PlayerCamera;
            playerCamera.MainCamera = this.mainCamera;
            playerCamera.FPSCamera = this.fpsCamera;
            playerCamera.TPSCamera = this.tpsCamera;
            playerCamera.IgnoreRaycastZone = this.ignoreRaycastZone;

            PlayerCtrl.Instance.PlayerInteract.InteractableRaycastObject = this.interactableRaycastObject;

            Vector3 pos = this.isTutorial ? this.startTransform1.position : this.startTransform2.position;
            Quaternion rot = this.isTutorial ? this.startTransform1.rotation : this.startTransform2.rotation;
            GameManager.Instance.GenerateCharacter(pos, rot);
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.Enable_UI_InGamePanel();
        }
    }
}
