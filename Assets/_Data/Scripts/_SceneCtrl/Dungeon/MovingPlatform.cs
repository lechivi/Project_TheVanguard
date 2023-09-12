using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] List<Transform> listWaypoint = new List<Transform>();
    [SerializeField] private Transform targetPlatform;
    [SerializeField] private float moveSpeed;

    private bool isFinish;
    private bool isRunAnimation;
    private int curIndex;

    public bool IsFinish { get => this.isFinish; }
    public bool IsRunAnimation { get => this.isRunAnimation; }

    private void FixedUpdate()
    {
        this.Move();
    }

    private void Move()
    {
        if (!this.isRunAnimation || this.targetPlatform == null) return;

        int nextIndex = this.curIndex + 1;
        if (nextIndex >= listWaypoint.Count)
            nextIndex = 0;
        if (nextIndex < 0)
            nextIndex = this.listWaypoint.Count - 1;

        //this.targetPlatform.localPosition = Vector3.Slerp(this.targetPlatform.localPosition,
        //    this.listWaypoint[nextIndex].localPosition, this.moveSpeed * Time.fixedDeltaTime);
        this.targetPlatform.Translate((
            this.listWaypoint[nextIndex].position -
            this.targetPlatform.position).normalized * this.moveSpeed * Time.fixedDeltaTime);

        if (Vector3.Distance(this.targetPlatform.localPosition, this.listWaypoint[nextIndex].localPosition) < 0.25f)
        {
            this.curIndex = nextIndex;
            this.isRunAnimation = false;
        }
    }

    public bool TriggerMove()
    {
        if (!this.isRunAnimation)
        {
            this.isRunAnimation = true;
            return true;
        }
        return false;
    }
}
