using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasCtrl : SaiMonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private CanvasGroup canvasGroup;

    private bool isOpen;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.canvasGroup == null)
            this.canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Show()
    {
        this.canvasGroup.alpha = 1.0f;
        this.canvasGroup.blocksRaycasts = true;
        transform.gameObject.SetActive(true);
    }

    private void Hide()
    {
        this.canvasGroup.alpha = 0f;
        this.canvasGroup.blocksRaycasts = false;
        transform.gameObject.SetActive(false);
    }

    public bool GetIsOpen()
    {
        return this.isOpen;
    }

    public void SetOpenClose(bool isOpen)
    {
        this.isOpen = isOpen;
        if (this.isOpen)
        {
            this.Show();
        }
        else
        {
            this.Hide();
        }
    }

}
