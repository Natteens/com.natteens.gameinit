using UnityEngine;

namespace GameInit.GameEvents.Channels {
    /// <summary>
    /// Estrutura vazia para eventos simples
    /// </summary>
    public readonly struct GameEvent { }
    
    /// <summary>
    /// Canal de eventos universal padronizado
    /// </summary>
    [CreateAssetMenu(fileName = "GameEventChannel", menuName = "GameInit/Events/Game Event Channel")]
    public class GameEventChannel : EventChannel<GameEvent> {
        [Header("Event Information")]
        [SerializeField] private string eventName;
        [SerializeField, TextArea(2, 4)] private string eventDescription;
        
        /// <summary>
        /// Nome do evento
        /// </summary>
        public string EventName => eventName;
        
        /// <summary>
        /// Descrição do evento
        /// </summary>
        public string EventDescription => eventDescription;
        
        /// <summary>
        /// Dispara evento simples usando a estrutura GameEvent
        /// </summary>
        public void RaiseEvent() {
            RaiseEvent(new GameEvent());
        }
    }
}