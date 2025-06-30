using GameInit.GameEvents.Channels;
using UnityEngine;
using UnityEngine.Events;

namespace GameInit.GameEvents.EventListeners {
    /// <summary>
    /// Classe base para listeners de eventos via Inspector
    /// </summary>
    /// <typeparam name="T">Tipo de dados do evento</typeparam>
    public abstract class EventListener<T> : MonoBehaviour, IEventListener<T> {
        [Header("Event Configuration")]
        [SerializeField] protected EventChannel<T> eventChannel;
        
        [Header("Response")]
        [SerializeField] protected UnityEvent<T> onEventRaised;
        
        /// <summary>
        /// Verifica se o listener ainda é válido
        /// </summary>
        public bool IsValid => this != null && gameObject != null;
        
        protected virtual void OnEnable() {
            if (eventChannel != null) {
                eventChannel.Subscribe(OnEventRaised);
            }
        }
        
        protected virtual void OnDisable() {
            if (eventChannel != null) {
                eventChannel.Unsubscribe(OnEventRaised);
            }
        }
        
        /// <summary>
        /// Processa evento recebido
        /// </summary>
        /// <param name="value">Valor do evento</param>
        public virtual void OnEventRaised(T value) {
            onEventRaised?.Invoke(value);
            OnEventReceived(value);
        }
        
        /// <summary>
        /// Método virtual para lógica customizada
        /// </summary>
        /// <param name="value">Valor recebido do evento</param>
        protected virtual void OnEventReceived(T value) { }
    }
}