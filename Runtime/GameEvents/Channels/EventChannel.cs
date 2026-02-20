using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Classe base para canais de eventos tipados.
    /// Suporta broadcast e eventos direcionados por entidade.
    /// </summary>
    /// <typeparam name="T">Tipo de dados do evento</typeparam>
    public abstract class EventChannel<T> : ScriptableObject
    {
        [Header("Channel Behavior")]
        [Tooltip("Se true, novos subscribers recebem o último valor ao se inscrever.")]
        [SerializeField] private bool replayLastValue = false;

        private event Action<T> onEventRaised;
        private T lastValue;
        private bool hasValue;

        private Dictionary<int, Action<T>> targetedListeners;
        private Dictionary<int, T> targetedLastValues;

        /// <summary>
        /// Último valor disparado no canal (broadcast)
        /// </summary>
        public T LastValue => lastValue;

        /// <summary>
        /// Indica se o canal já disparou algum valor
        /// </summary>
        public bool HasValue => hasValue;

        /// <summary>
        /// Dispara evento para TODOS os listeners (broadcast)
        /// </summary>
        /// <param name="value">Valor a ser enviado</param>
        public void RaiseEvent(T value)
        {
            lastValue = value;
            hasValue = true;
            onEventRaised?.Invoke(value);
        }

        /// <summary>
        /// Dispara evento para uma entidade específica pelo ID. O(1).
        /// </summary>
        /// <param name="targetId">ID da entidade alvo</param>
        /// <param name="value">Valor a ser enviado</param>
        public void RaiseEvent(int targetId, T value)
        {
            if (targetedListeners != null && targetedListeners.TryGetValue(targetId, out var callback))
            {
                if (replayLastValue)
                {
                    targetedLastValues ??= new Dictionary<int, T>();
                    targetedLastValues[targetId] = value;
                }

                callback.Invoke(value);
            }
        }

        /// <summary>
        /// Dispara evento para uma entidade específica via GameObject.
        /// </summary>
        /// <param name="target">GameObject alvo</param>
        /// <param name="value">Valor a ser enviado</param>
        public void RaiseEvent(GameObject target, T value)
        {
            RaiseEvent(target.GetInstanceID(), value);
        }

        /// <summary>
        /// Dispara evento para uma entidade específica via Component.
        /// </summary>
        /// <param name="target">Component alvo</param>
        /// <param name="value">Valor a ser enviado</param>
        public void RaiseEvent(Component target, T value)
        {
            RaiseEvent(target.gameObject.GetInstanceID(), value);
        }

        /// <summary>
        /// Inscreve callback para receber eventos broadcast
        /// </summary>
        /// <param name="callback">Método a ser chamado quando o evento for disparado</param>
        public void Subscribe(Action<T> callback)
        {
            onEventRaised += callback;

            if (replayLastValue && hasValue && callback != null)
            {
                callback.Invoke(lastValue);
            }
        }

        /// <summary>
        /// Inscreve callback associado a um ID de entidade
        /// </summary>
        /// <param name="entityId">ID da entidade</param>
        /// <param name="callback">Método a ser chamado quando o evento for disparado</param>
        public void Subscribe(int entityId, Action<T> callback)
        {
            targetedListeners ??= new Dictionary<int, Action<T>>();

            if (targetedListeners.ContainsKey(entityId))
                targetedListeners[entityId] += callback;
            else
                targetedListeners[entityId] = callback;

            if (replayLastValue && targetedLastValues != null &&
                targetedLastValues.TryGetValue(entityId, out var cached))
            {
                callback.Invoke(cached);
            }
        }

        /// <summary>
        /// Remove inscrição de eventos broadcast
        /// </summary>
        /// <param name="callback">Método a ser removido</param>
        public void Unsubscribe(Action<T> callback)
        {
            onEventRaised -= callback;
        }

        /// <summary>
        /// Remove inscrição de uma entidade específica
        /// </summary>
        /// <param name="entityId">ID da entidade</param>
        /// <param name="callback">Método a ser removido</param>
        public void Unsubscribe(int entityId, Action<T> callback)
        {
            if (targetedListeners == null) return;

            if (targetedListeners.ContainsKey(entityId))
            {
                targetedListeners[entityId] -= callback;

                if (targetedListeners[entityId] == null)
                {
                    targetedListeners.Remove(entityId);
                    targetedLastValues?.Remove(entityId);
                }
            }
        }

        /// <summary>
        /// Obtém o último valor de uma entidade específica
        /// </summary>
        /// <param name="entityId">ID da entidade</param>
        /// <param name="value">Valor retornado</param>
        /// <returns>True se existe valor armazenado</returns>
        public bool TryGetValue(int entityId, out T value)
        {
            if (targetedLastValues != null && targetedLastValues.TryGetValue(entityId, out value))
                return true;

            value = default;
            return false;
        }

        /// <summary>
        /// Reseta todo o estado do canal
        /// </summary>
        public void ResetValue()
        {
            lastValue = default;
            hasValue = false;
            onEventRaised = null;
            targetedListeners?.Clear();
            targetedLastValues?.Clear();
        }

        private void OnEnable()
        {
            if (Application.isPlaying)
            {
                ResetValue();
            }
        }
    }
}