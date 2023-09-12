using System.Collections;
using UnityEngine;

public class ElevatorRemoteInteractable : SaiMonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Active the elevator";
    [SerializeField] private MovingPlatform movingPlatform;
    [SerializeField] private bool canActive = true;

    [Header("HANDLE")]
    [SerializeField] private Transform handle;
    [SerializeField] private bool isStartPoint = true;
    [SerializeField] private float startRotX = -45;
    [SerializeField] private float endRotX = 45;
    [SerializeField] private float rotSpeed = 100;
    private bool triggerHandle;
    public bool CanActive { get => this.canActive; set => this.canActive = value; }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.handle == null)
            this.handle = transform.Find("Handle");
    }

    void Start()
    {
        if (this.handle != null)
        {
            float rotX = this.isStartPoint ? this.startRotX : this.endRotX;
            this.handle.localRotation = Quaternion.Euler(rotX, this.handle.localRotation.y, this.handle.localRotation.z);
        }
    }

    private IEnumerator RotateBetweenPoints()
    {
        if (this.handle == null || !this.triggerHandle) 
            yield return null;

        while (this.triggerHandle)
        {
            float targetRotX = this.isStartPoint ? this.endRotX : this.startRotX;
            float currentRotation = this.handle.localRotation.x;

            while (Mathf.Abs(targetRotX - currentRotation) > 0.1f)
            {
                //Debug.Log(currentRotation);
                currentRotation = Mathf.MoveTowards(currentRotation, targetRotX, this.rotSpeed * Time.fixedDeltaTime);
                this.handle.localRotation = Quaternion.Euler(currentRotation, this.handle.localRotation.y, this.handle.localRotation.z);
                yield return null;
            }

            // Đảo hướng quay khi đạt điểm B hoặc A
            this.isStartPoint = !this.isStartPoint;
            this.triggerHandle = false;
            yield return null;
        }
    }

    public void Interact(Transform interactorTransfrom)
    {
        this.movingPlatform.TriggerMove();
        if (!this.triggerHandle)
        {
            this.triggerHandle = true;
            StartCoroutine(this.RotateBetweenPoints());
        }
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public bool CanInteract()
    {
        if (!this.canActive || this.movingPlatform == null)
            return false;

        return !this.movingPlatform.IsRunAnimation;
    }

    public string GetInteractableText()
    {
        return this.interactText;
    }
}
