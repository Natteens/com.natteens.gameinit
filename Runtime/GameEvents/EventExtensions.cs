using System;
using GameInit.GameEvents.Channels;
using UnityEngine;

namespace GameInit.GameEvents {
    /// <summary>
    /// Extensões para facilitar uso do sistema de eventos
    /// </summary>
    public static class EventExtensions {
        /// <summary>
        /// Inscreve MonoBehaviour em canal de eventos com cleanup automático
        /// </summary>
        /// <param name="mono">MonoBehaviour a ser inscrito</param>
        /// <param name="channel">Canal de eventos</param>
        /// <param name="callback">Método a ser chamado</param>
        public static void SubscribeTo<T>(this MonoBehaviour mono, EventChannel<T> channel, Action<T> callback) {
            if (channel != null && callback != null) {
                channel.Subscribe(callback);
                mono.StartCoroutine(AutoUnsubscribe(mono, channel, callback));
            }
        }
        
        private static System.Collections.IEnumerator AutoUnsubscribe<T>(MonoBehaviour mono, EventChannel<T> channel, Action<T> callback) {
            yield return new WaitUntil(() => mono == null);
            channel?.Unsubscribe(callback);
        }
        
        /// <summary>
        /// Obtém valor atual de um canal
        /// </summary>
        /// <param name="channel">Canal de eventos</param>
        /// <returns>Valor atual ou padrão</returns>
        public static T GetValue<T>(this EventChannel<T> channel) {
            return channel != null && channel.HasValue ? channel.CurrentValue : default(T);
        }
    }
}