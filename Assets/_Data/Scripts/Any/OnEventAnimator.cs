using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnEventAnimator : MonoBehaviour
{
    public delegate void AnimatorMoveDelegate();
    public event AnimatorMoveDelegate OnAnimatorMoveEvent;

    private void OnAnimatorMove()
    {
        if(OnAnimatorMoveEvent != null)
        {
            OnAnimatorMoveEvent();
        }
    }
}
