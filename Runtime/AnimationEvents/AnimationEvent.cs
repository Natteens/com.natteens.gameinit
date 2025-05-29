using System;
using UnityEngine.Events;

namespace GameInit.AnimationEvents
{
    [Serializable]
    public class AnimationEvent {
        public string eventName;
        public UnityEvent OnAnimationEvent;
        
        public AnimationEvent(string name)
        {
            eventName = name;
            OnAnimationEvent = new UnityEvent();
        }
    }
}