using System;
using UnityEngine;

namespace GameInit.GameEvents.Channels {
    /// <summary>
    /// Classe base para canais de eventos tipados
    /// </summary>
    /// <typeparam name="T">Tipo de dados do evento</typeparam>
    public abstract class EventChannel<T> : ScriptableObject {
        private event Action<T> onEventRaised;
        private T currentValue;
        private bool hasValue;
        
        /// <summary>
        /// Valor atual armazenado no canal
        /// </summary>
        public T CurrentValue => currentValue;
        
        /// <summary>
        /// Indica se o canal possui um valor válido
        /// </summary>
        public bool HasValue => hasValue;
        
        /// <summary>
        /// Dispara um evento com o valor especificado
        /// </summary>
        /// <param name="value">Valor a ser enviado</param>
        public void RaiseEvent(T value) {
            currentValue = value;
            hasValue = true;
            onEventRaised?.Invoke(value);
        }
        
        /// <summary>
        /// Inscreve um callback para receber eventos
        /// </summary>
        /// <param name="callback">Método a ser chamado quando o evento for disparado</param>
        public void Subscribe(Action<T> callback) {
            onEventRaised += callback;
            
            // Só invoca valor anterior se houver valor válido E não estiver em modo reset
            if (hasValue && callback != null) {
                callback.Invoke(currentValue);
            }
        }
        
        /// <summary>
        /// Remove a inscrição de um callback
        /// </summary>
        /// <param name="callback">Método a ser removido</param>
        public void Unsubscribe(Action<T> callback) {
            onEventRaised -= callback;
        }
        
        /// <summary>
        /// Reseta o valor armazenado
        /// </summary>
        public void ResetValue() {
            currentValue = default(T);
            hasValue = false;
            onEventRaised = null;
        }
        
        private void OnEnable() {
            // Limpa quando o SO é habilitado no play mode
            if (Application.isPlaying) {
                ResetValue();
            }
        }
    }
}