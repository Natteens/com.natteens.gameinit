using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos inteiros
    /// </summary>
    [CreateAssetMenu(fileName = "IntEventChannel", menuName = "GameInit/Events/Int Event")]
    public class IntEventChannel : EventChannel<int> { }
}