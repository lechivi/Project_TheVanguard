using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_PlayerInteract : MonoBehaviour
{
    [SerializeField] private PlayerInteract playerInteract;
    [SerializeField] private GameObject container;
    [SerializeField] private TMP_Text text;

    private void Update()
    {
        if (this.playerInteract.GetInteractableObjectByRaycast() != null)
        {
            this.Show();
        }
        else
        {
            this.Hide();
        }
    }

    private void Show()
    {
        this.container.SetActive(true);
        this.text.SetText(this.playerInteract.GetInteractableObjectByRaycast().GetInteractableText());
    }

    private void Hide()
    {
        this.container.SetActive(false);
    }
}
