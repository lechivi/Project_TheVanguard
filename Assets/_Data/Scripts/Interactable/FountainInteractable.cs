using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainInteractable : SaiMonoBehaviour, IInteractable
{
    [SerializeField] private string interactText = "Make a wish";
    [SerializeField] private ParticleSystem fxFountain;

    private bool isReady = true;
    private float delay = 10f;
    private float timer;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.fxFountain == null)
            this.fxFountain = GetComponentInChildren<ParticleSystem>();
    }
    public bool CanInteract()
    {
        return this.isReady;
    }

    public string GetInteractableText()
    {
        return this.interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform interactorTransfrom)
    {
        this.timer = 0;
        this.isReady = false;
        this.fxFountain.gameObject.SetActive(true);

        if (AudioManager.HasInstance)
        {
            AudioManager.Instance.PlaySe(AUDIO.SE_WIN_NOTIFICATIONS_9);
        }
    }

    void FixedUpdate()
    {
        if (!this.isReady)
        {
            this.timer += Time.fixedDeltaTime;
            if (this.timer > this.delay)
            {
                this.timer = 0;
                this.isReady = true;
                this.fxFountain.gameObject.SetActive(false);
            }
        }
    }
}
