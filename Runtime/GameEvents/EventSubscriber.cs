using System;
using GameInit.GameEvents.Channels;
using UnityEngine;

namespace GameInit.GameEvents {
    /// <summary>
    /// Componente para facilitar inscrição em eventos
    /// </summary>
    public class EventSubscriber : MonoBehaviour {
        /// <summary>
        /// Inscreve-se em um canal de eventos
        /// </summary>
        /// <param name="channel">Canal de eventos</param>
        /// <param name="callback">Método a ser chamado</param>
        public void Subscribe<T>(EventChannel<T> channel, Action<T> callback) {
            if (channel != null) {
                channel.Subscribe(callback);
            }
        }
        
        /// <summary>
        /// Remove inscrição de um canal de eventos
        /// </summary>
        /// <param name="channel">Canal de eventos</param>
        /// <param name="callback">Método a ser removido</param>
        public void Unsubscribe<T>(EventChannel<T> channel, Action<T> callback) {
            if (channel != null) {
                channel.Unsubscribe(callback);
            }
        }
        
        /// <summary>
        /// Obtém o valor atual de um canal
        /// </summary>
        /// <param name="channel">Canal de eventos</param>
        /// <returns>Valor atual ou padrão</returns>
        public T GetCurrentValue<T>(EventChannel<T> channel) {
            return channel != null && channel.HasValue ? channel.CurrentValue : default(T);
        }
    }
}