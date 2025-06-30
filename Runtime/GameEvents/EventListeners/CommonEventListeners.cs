using GameInit.GameEvents.Channels;
using UnityEngine;

namespace GameInit.GameEvents.EventListeners {
    /// <summary>
    /// Listener para eventos vazios
    /// </summary>
    public class VoidEventListener : EventListener<VoidEvent> { }
    
    /// <summary>
    /// Listener para eventos inteiros
    /// </summary>
    public class IntEventListener : EventListener<int> { }
    
    /// <summary>
    /// Listener para eventos float
    /// </summary>
    public class FloatEventListener : EventListener<float> { }
    
    /// <summary>
    /// Listener para eventos string
    /// </summary>
    public class StringEventListener : EventListener<string> { }
    
    /// <summary>
    /// Listener para eventos booleanos
    /// </summary>
    public class BoolEventListener : EventListener<bool> { }
    
    /// <summary>
    /// Listener para eventos Vector3
    /// </summary>
    public class Vector3EventListener : EventListener<Vector3> { }
    
    /// <summary>
    /// Listener padronizado para GameEventChannel
    /// </summary>
    public class GameEventListener : EventListener<GameEvent> { }
}