using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos Vector3
    /// </summary>
    [CreateAssetMenu(fileName = "Vector3EventChannel", menuName = "GameInit/Events/Vector3 Event")]
    public class Vector3EventChannel : EventChannel<Vector3> { }
}