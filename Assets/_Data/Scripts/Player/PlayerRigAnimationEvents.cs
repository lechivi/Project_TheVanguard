
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : UnityEvent<string>
{

}

public class PlayerRigAnimationEvents : MonoBehaviour
{
    public AnimationEvent AnimationEvent = new AnimationEvent();
    
    public void OnAnimationEvent(string eventName)
    {
        AnimationEvent.Invoke(eventName);
    }
}
