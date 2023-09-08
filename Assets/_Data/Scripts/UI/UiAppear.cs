using UnityEngine;

public class UiAppear : MonoBehaviour
{
    [SerializeField] protected Vector3 startPos = new Vector3(0, -1000, 0);
    [SerializeField] protected Vector3 endPos = new Vector3(0, 0, 0);
    [SerializeField] protected float moveSpeed = 10f;
    [SerializeField] protected bool isRunAnimation;

    protected void FixedUpdate()
    {
        this.Showing();
    }

    protected virtual void SetStartPos()
    {
        transform.localPosition = this.startPos;
    }

    protected virtual void Showing()
    {
        if (!this.isRunAnimation) return;

        transform.localPosition = Vector3.Lerp(transform.localPosition, this.endPos, this.moveSpeed * Time.fixedDeltaTime);
        if (transform.localPosition == this.endPos)
            this.isRunAnimation = false;
    }

    public virtual void Appear()
    {
        this.SetStartPos();
        this.isRunAnimation = true;
    }
}
