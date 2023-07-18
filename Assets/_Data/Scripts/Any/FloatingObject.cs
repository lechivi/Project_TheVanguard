using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    private float minY = 0f;
    private float maxY = 0.25f;
    private float moveSpeed = 0.25f;
    private float rotationSpeed = 30f;
    private float direction = 1f;

    private float randomMoveSpeed;
    private float randomRotationSpeed;

    private void Start()
    {
        this.randomMoveSpeed = Random.Range(this.moveSpeed - 0.1f, this.moveSpeed);
        this.randomRotationSpeed = Random.Range(this.rotationSpeed - 5f, this.rotationSpeed + 5f);
    }

    private void FixedUpdate()
    {
        float newY = transform.localPosition.y + this.direction * this.randomMoveSpeed * Time.fixedDeltaTime;
        newY = Mathf.Clamp(newY, this.minY, this.maxY);

        transform.localPosition = new Vector3(transform.localPosition.x, newY, transform.localPosition.z);

        transform.Rotate(Vector3.up * this.randomRotationSpeed * Time.fixedDeltaTime);

        if (Mathf.Approximately(newY, this.minY) || Mathf.Approximately(newY, this.maxY))
        {
            this.direction *= -1f;
        }
    }
}
