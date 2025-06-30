namespace GameInit.GameEvents.EventListeners {
    /// <summary>
    /// Interface para listeners de eventos
    /// </summary>
    /// <typeparam name="T">Tipo de dados do evento</typeparam>
    public interface IEventListener<T> {
        /// <summary>
        /// Indica se o listener ainda é válido
        /// </summary>
        bool IsValid { get; }
        
        /// <summary>
        /// Método chamado quando um evento é disparado
        /// </summary>
        /// <param name="value">Valor recebido do evento</param>
        void OnEventRaised(T value);
    }
}