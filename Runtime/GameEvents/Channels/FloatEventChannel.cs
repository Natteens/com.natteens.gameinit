using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos float
    /// </summary>
    [CreateAssetMenu(fileName = "FloatEventChannel", menuName = "GameInit/Events/Float Event")]
    public class FloatEventChannel : EventChannel<float> { }
}