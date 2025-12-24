using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos booleanos
    /// </summary>
    [CreateAssetMenu(fileName = "BoolEventChannel", menuName = "Scriptable Objects/GameInit/Events/Bool Event")]
    public class BoolEventChannel : EventChannel<bool> { }
}