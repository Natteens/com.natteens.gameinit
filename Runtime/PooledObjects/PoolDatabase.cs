using UnityEngine;

namespace GameInit.PooledObjects
{
    /// <summary>
    /// Database de configuração de pools simplificada
    /// </summary>
    [CreateAssetMenu(fileName = "PoolDatabase", menuName = "Pooling/Pool Database")]
    public class PoolDatabase : ScriptableObject
    {
        [System.Serializable]
        public struct PoolConfiguration
        {
            [Tooltip("Identificador único do pool")]
            public string poolTag;
            
            [Tooltip("GameObject base para instanciar")]
            public GameObject prefab;
            
            [Tooltip("Quantidade inicial de objetos")]
            public int initialSize;
            
            [Tooltip("Quantidade máxima de objetos")]
            public int maxSize;
            
            [Tooltip("Criar objetos na inicialização")]
            public bool preWarm;
            
            [Tooltip("Limpar objetos ao carregar nova cena")]
            public bool clearOnSceneLoad;
        }

        [Header("Pools")]
        public PoolConfiguration[] pools;

        [Header("Sistema")]
        [Tooltip("Ativa logs detalhados")]
        public bool enableDebugMode;
        
        [Tooltip("Usa Jobs System para operações em lote")]
        public bool useJobsForBatching;
        
        [Tooltip("Limite para ativar Jobs System")]
        public int jobsThreshold = 100;
    }
}