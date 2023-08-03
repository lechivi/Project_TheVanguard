using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas3dObject : MonoBehaviour
{
    private bool isVisible = true;
    private CanvasGroup parentCanvasGroup;

    public void Show()
    {
        isVisible = true;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        isVisible = false;
        gameObject.SetActive(false);
    }

    private void Start()
    {
        // Find the parent canvas group
        this.parentCanvasGroup = GetParentCanvasGroup(transform);

        // Subscribe to the OnCanvasGroupChanged event
        if (parentCanvasGroup != null)
        {
            //parentCanvasGroup.onCanvasGroupChanged += OnParentCanvasGroupChanged;
        }

        // Hide the object if the parent canvas group is initially transparent (alpha = 0)
        OnParentCanvasGroupChanged(parentCanvasGroup.alpha);
    }

    private CanvasGroup GetParentCanvasGroup(Transform childTransform)
    {
        Transform parentTransform = childTransform.parent;
        while (parentTransform != null)
        {
            CanvasGroup canvasGroup = parentTransform.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                return canvasGroup;
            }
            parentTransform = parentTransform.parent;
        }
        return null;
    }

    private void OnParentCanvasGroupChanged(float alpha)
    {
        // If the parent canvas group's alpha is 0, hide the 3D object.
        // If the parent canvas group's alpha is 1, show the 3D object.
        if (alpha <= 0f)
        {
            this.Hide();
        }
        else
        {
            this.Show();
        }
    }
}
