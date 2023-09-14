using Cinemachine;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class DungeonSceneCtrl : SaiMonoBehaviour
{
    [SerializeField] private Transform startTransform;
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

    public Camera MainCamer { get => this.mainCamera; }
    public GameObject IgnoreRaycastZone { get => this.ignoreRaycastZone; }
    public Transform InteractableRaycastObject { get => this.interactableRaycastObject; }
    public Transform CrosshairTarget { get => this.crosshairTarget; }
    public Transform AimLookAt { get => this.aimLookAt; }
    public Transform AimLookMain { get => this.aimLookMain; }
    public CinemachineVirtualCamera FPSCamera { get => this.fpsCamera; }
    public CinemachineFreeLook TPSCamera { get => this.tpsCamera; }

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
            AudioManager.Instance.PlayBgm(AUDIO.BGM_BATTLE_DUNGEON_BOINC_BACK);
        }

        if (InputManager.HasInstance)
        {
            InputManager.Instance.Disable_Input_All();
        }

        if (GameManager.HasInstance)
        {
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

            foreach (Character chr in this.listCharacter)
            {
                if (chr.CharacterData == GameManager.Instance.CharacterData)
                {
                    Character newCharacter = Instantiate(chr);
                    this.chr = newCharacter;
                    newCharacter.name = newCharacter.CharacterData.CharacterName;

                    this.cutoutObject.TargetObject = newCharacter.GetComponentInChildren<AlliancePlayer_InfoScanner>().GetCenterPoint();
                    newCharacter.CharacterTransform.position = this.startTransform.position;
                    newCharacter.CharacterTransform.rotation = this.startTransform.rotation;
                    newCharacter.gameObject.SetActive(true);

                    GameManager.Instance.GenerateCharacter(newCharacter);
                    break;
                }
            }
        }

        if (UIManager.HasInstance)
        {
            UIManager.Instance.Enable_UI_InGamePanel();
        }
    }

    private Character chr;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            chr.gameObject.SetActive(false);
            chr.CharacterTransform.position = this.startTransform.position;
            chr.CharacterTransform.rotation = this.startTransform.rotation;
            chr.gameObject.SetActive(true);

        }
    }
}
