using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos string
    /// </summary>
    [CreateAssetMenu(fileName = "StringEventChannel", menuName = "Scriptable Objects/GameInit/Events/String Event")]
    public class StringEventChannel : EventChannel<string> { }
}