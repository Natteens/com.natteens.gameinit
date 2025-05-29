using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameInit.AnimationEvents
{
    public class AnimationEventReceiver : MonoBehaviour {
        [SerializeField] List<AnimationEvent> animationEvents = new();
        [SerializeField] private bool debugMode;
        
        public void OnAnimationEventTriggered(string eventName) {
            AnimationEvent matchingEvent = animationEvents.Find(se => se.eventName == eventName);
            matchingEvent?.OnAnimationEvent?.Invoke();
            if(debugMode)
                Debug.Log($"Evento de animação {eventName} disparado");
        }
        
        public void AddEvent(string eventName, UnityAction callback)
        {
            var existingEvent = animationEvents.Find(e => e.eventName == eventName);

            if (existingEvent != null)
            {
                existingEvent.OnAnimationEvent.AddListener(callback);
                if(debugMode)
                    Debug.Log($"Evento {eventName} que caiu no if e foi adicionado");
            }
            else
            {
                var newEvent = new AnimationEvent(eventName);
                newEvent.OnAnimationEvent.AddListener(callback);
                animationEvents.Add(newEvent);
                if (debugMode)
                    Debug.Log($"Evento {eventName} que caiu no else e foi adicionado");
            }
        }
    }
}