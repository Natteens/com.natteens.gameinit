using UnityEngine;

namespace GameInit.GameEvents.Channels
{
    /// <summary>
    /// Canal de eventos Vector2
    /// </summary>
    [CreateAssetMenu(fileName = "Vector3EventChannel", menuName = "GameInit/Events/Vector2 Event")]
    public class Vector2EventChannel : EventChannel<Vector2> { }
}