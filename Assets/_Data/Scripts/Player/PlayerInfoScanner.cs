using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoScanner : PlayerAbstract
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private float scanDistance = 7.5f;
    [SerializeField] private LayerMask scanLayer;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        if (this.mainCamera == null)
            this.mainCamera = Camera.main.transform;
    }

    public Transform GetInfoScannerObjectByRaycast()
    {
        Physics.Raycast(this.mainCamera.transform.position, this.mainCamera.transform.forward, 
            out RaycastHit hitInfo, this.scanDistance, this.scanLayer);
        if (hitInfo.collider != null && hitInfo.transform.TryGetComponent(out Transform transform))
        {
            return transform;
        }
        return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(this.mainCamera.transform.position, this.mainCamera.transform.position + 
            (this.mainCamera.transform.forward * this.scanDistance));
    }
}
