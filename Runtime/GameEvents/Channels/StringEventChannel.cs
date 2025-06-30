using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos string
    /// </summary>
    [CreateAssetMenu(fileName = "StringEventChannel", menuName = "GameInit/Events/String Event")]
    public class StringEventChannel : EventChannel<string> { }
}