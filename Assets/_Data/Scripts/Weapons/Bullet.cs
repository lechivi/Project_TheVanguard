using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    public float time;
    public Vector3 intitialPosition;
    public Vector3 intitialVelocity;
    public TrailRenderer tracer;
    private bool isActive = false;
    public bool IsActive => isActive;

    public void Deactive()
    {
        this.gameObject.SetActive(false);
        isActive = false;
        intitialPosition = Vector3.zero;
        intitialVelocity = Vector3.zero;
        tracer.emitting = false;
        tracer.Clear();

    }

    public void Active(Vector3 position, Vector3 velocity)
    {
        this.gameObject.SetActive(true);
        isActive = true;
        time = 0f;
        intitialPosition = position;
        intitialVelocity = velocity;
        tracer.emitting = true;
        tracer.AddPosition(position);
    }
}
