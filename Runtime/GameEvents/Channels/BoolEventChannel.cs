using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos booleanos
    /// </summary>
    [CreateAssetMenu(fileName = "BoolEventChannel", menuName = "GameInit/Events/Bool Event")]
    public class BoolEventChannel : EventChannel<bool> { }
}