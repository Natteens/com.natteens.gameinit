using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos vazios
    /// </summary>
    [CreateAssetMenu(fileName = "VoidEventChannel", menuName = "GameInit/Events/Void Event")]
    public class VoidEventChannel : EventChannel<VoidEvent> { }
}