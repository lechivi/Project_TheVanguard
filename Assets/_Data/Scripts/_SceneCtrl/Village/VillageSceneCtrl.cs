using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VillageSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private Transform startTransform1;
    [SerializeField] private Transform startTransform2;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private CutoutObject cutoutObject;
    [SerializeField] private GameObject ignoreRaycastZone;
    [SerializeField] private Transform interactableRaycastObject;
    [SerializeField] private Transform crosshairTarget;
    [SerializeField] private Transform aimLookAt;
    [SerializeField] private Transform aimLookMain;
    [SerializeField] private CinemachineVirtualCamera fpsCamera;
    [SerializeField] private CinemachineFreeLook tpsCamera;
    [SerializeField] private List<Character> listCharacter = new List<Character>();

    [SerializeField] private bool isTutorial;

    public Camera MainCamer { get => this.mainCamera; }
    public GameObject IgnoreRaycastZone { get => this.ignoreRaycastZone; }
    public Transform InteractableRaycastObject { get => this.interactableRaycastObject; }
    public Transform CrosshairTarget { get => this.crosshairTarget; }
    public Transform AimLookAt { get => this.aimLookAt; }
    public Transform AimLookMain { get => this.aimLookMain; }
    public CinemachineVirtualCamera FPSCamera { get => this.fpsCamera; }
    public CinemachineFreeLook TPSCamera { get => this.tpsCamera; }
    public bool IsTutorial { get => this.isTutorial; set => this.isTutorial = value; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainCamera == null)
            this.mainCamera = Camera.main;

        if (this.cutoutObject == null)
            this.cutoutObject = this.mainCamera.GetComponent<CutoutObject>();

        if (this.ignoreRaycastZone == null)
            this.ignoreRaycastZone = this.mainCamera.transform.Find("IgnoreRaycastZone").gameObject;

        if (this.crosshairTarget == null)
            this.crosshairTarget = this.mainCamera.transform.Find("CrosshairTarget");

        if (this.interactableRaycastObject == null)
            this.interactableRaycastObject = this.mainCamera.transform.Find("InteractableRaycastObject");

        if (this.aimLookAt == null)
            this.aimLookAt = this.mainCamera.transform.Find("AimLookAt");

        if (this.aimLookMain == null)
            this.aimLookMain = GameObject.Find("AimLookMain").transform;

        if (this.fpsCamera == null)
            this.fpsCamera = GameObject.Find("FPSCamera").GetComponent<CinemachineVirtualCamera>();

        if (this.tpsCamera == null)
            this.tpsCamera = GameObject.Find("TPSCamera").GetComponent<CinemachineFreeLook>();
    }

    protected override void Awake()
    {
        base.Awake();
        foreach (Character chr in this.listCharacter)
        {
            chr.gameObject.SetActive(false);
        }
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
            GameManager.Instance.ResumeGame();

            PlayerCtrl playerCtrl = GameManager.Instance.PlayerCtrl;

            PlayerCamera playerCamera = playerCtrl.PlayerCamera;
            playerCamera.MainCamera = this.mainCamera;
            playerCamera.FPSCamera = this.fpsCamera;
            playerCamera.TPSCamera = this.tpsCamera;
            playerCamera.IgnoreRaycastZone = this.ignoreRaycastZone;

            playerCtrl.PlayerInteract.InteractableRaycastObject = this.interactableRaycastObject;

            playerCtrl.PlayerAim.AimLookMain = this.aimLookMain;
            playerCtrl.PlayerAim.AimLookAt = this.aimLookAt;

            playerCtrl.PlayerWeapon.PlayerWeaponActive.CrosshairTarget = this.crosshairTarget;

            //GameManager.Instance.GenerateCharacter(transform.position, transform.rotation);
            Vector3 pos = this.isTutorial ? this.startTransform1.position : this.startTransform2.position;
            Quaternion rot = this.isTutorial ? this.startTransform1.rotation : this.startTransform2.rotation;
            //GameManager.Instance.GenerateCharacter(pos, rot);
            foreach (Character chr in this.listCharacter)
            {
                if (chr.CharacterData == GameManager.Instance.CharacterData)
                {
                    this.cutoutObject.TargetObject = chr.GetComponentInChildren<AlliancePlayer_InfoScanner>().GetCenterPoint();
                    chr.transform.position = pos;
                    chr.transform.rotation = rot;
                    chr.gameObject.SetActive(true);

                    GameManager.Instance.GenerateCharacter(chr);
                    //PlayerCtrl.Instance.PlayerWeapon.PlayerWeaponManager.LoadWeapon();
                    break;
                }
            }
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.Enable_UI_InGamePanel();
        }
    }
}
