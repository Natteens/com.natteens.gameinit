using UnityEngine;

namespace GameInit.GameEvents.Channels {
    /// <summary>
    /// Responsável por resetar todos os EventChannels ao entrar em play mode
    /// Funciona mesmo com Domain Reload desabilitado
    /// </summary>
    public static class EventChannelResetter {
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void ResetAllChannels() {
            // Este método é chamado ANTES de qualquer Awake/Start
            // e funciona mesmo com Domain Reload desabilitado
            
#if UNITY_EDITOR
            // No editor, força reset de todos os EventChannels
            var allChannels = Resources.FindObjectsOfTypeAll<ScriptableObject>();
            
            foreach (var obj in allChannels) {
                // Verifica se o objeto é um EventChannel usando reflection
                var type = obj.GetType();
                if (IsEventChannel(type)) {
                    var resetMethod = type.BaseType?.GetMethod("ResetValue");
                    resetMethod?.Invoke(obj, null);
                }
            }
#endif
        }
        
        private static bool IsEventChannel(System.Type type) {
            while (type != null && type != typeof(ScriptableObject)) {
                if (type.IsGenericType && type.GetGenericTypeDefinition().Name.Contains("EventChannel")) {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }
    }
}