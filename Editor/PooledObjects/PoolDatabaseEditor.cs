using GameInit.PooledObjects;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace GameInit.Editor.PooledObjects
{
    /// <summary>
    /// Editor simplificado para PoolDatabase
    /// Interface minimalista para configuração básica de pools
    /// </summary>
    [CustomEditor(typeof(PoolDatabase))]
    public class PoolDatabaseEditor : UnityEditor.Editor
    {
        #region Campos Privados
        private PoolDatabase database;
        private SerializedProperty poolsProperty;
        private bool showValidation = true;
        private bool showSettings = false;
        #endregion

        #region Unity Lifecycle
        void OnEnable()
        {
            database = (PoolDatabase)target;
            poolsProperty = serializedObject.FindProperty("pools");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawHeader();
            DrawMainSettings();
            DrawPoolsList();
            DrawValidation();

            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        #region Interface Principal
        /// <summary>
        /// Desenha cabeçalho simples
        /// </summary>
        void DrawHeader()
        {
            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("Pool Database", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);
            
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("+ Novo Pool", GUILayout.Height(25)))
                {
                    AddNewPool();
                }
                
                GUILayout.FlexibleSpace();
                
                EditorGUILayout.LabelField($"Total: {poolsProperty.arraySize}", EditorStyles.miniLabel);
            }
            
            EditorGUILayout.Space(5);
        }

        /// <summary>
        /// Desenha configurações principais
        /// </summary>
        void DrawMainSettings()
        {
            showSettings = EditorGUILayout.Foldout(showSettings, "Configurações", true);
            
            if (!showSettings) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty("enableDebugMode"),
                    new GUIContent("Debug Mode")
                );

                EditorGUILayout.PropertyField(
                    serializedObject.FindProperty("useJobsForBatching"),
                    new GUIContent("Jobs System")
                );

                if (database.useJobsForBatching)
                {
                    EditorGUILayout.PropertyField(
                        serializedObject.FindProperty("jobsThreshold"),
                        new GUIContent("Jobs Threshold")
                    );
                }
            }

            EditorGUILayout.Space(5);
        }

        /// <summary>
        /// Desenha lista de pools
        /// </summary>
        void DrawPoolsList()
        {
            EditorGUILayout.LabelField("Pools", EditorStyles.boldLabel);

            if (poolsProperty.arraySize == 0)
            {
                EditorGUILayout.HelpBox("Nenhum pool configurado.", MessageType.Info);
                return;
            }

            for (int i = 0; i < poolsProperty.arraySize; i++)
            {
                DrawPool(i);
            }
        }

        /// <summary>
        /// Desenha um pool individual
        /// </summary>
        void DrawPool(int index)
        {
            var poolElement = poolsProperty.GetArrayElementAtIndex(index);
            var config = database.pools[index];

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                // Cabeçalho do pool
                using (new EditorGUILayout.HorizontalScope())
                {
                    var displayName = !string.IsNullOrEmpty(config.poolTag) ? config.poolTag : $"Pool {index + 1}";
                    EditorGUILayout.LabelField(displayName, EditorStyles.boldLabel);
                    
                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        RemovePool(index);
                        return;
                    }
                }

                // Configurações do pool
                EditorGUILayout.PropertyField(poolElement.FindPropertyRelative("poolTag"), new GUIContent("Tag"));
                EditorGUILayout.PropertyField(poolElement.FindPropertyRelative("prefab"), new GUIContent("Prefab"));

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PropertyField(
                        poolElement.FindPropertyRelative("initialSize"),
                        new GUIContent("Inicial"),
                        GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.45f)
                    );

                    EditorGUILayout.PropertyField(
                        poolElement.FindPropertyRelative("maxSize"),
                        new GUIContent("Máximo"),
                        GUILayout.Width(EditorGUIUtility.currentViewWidth * 0.45f)
                    );
                }

                using (new EditorGUILayout.HorizontalScope())
                {
                    EditorGUILayout.PropertyField(poolElement.FindPropertyRelative("preWarm"), new GUIContent("Pre-Warm"));
                    EditorGUILayout.PropertyField(poolElement.FindPropertyRelative("clearOnSceneLoad"), new GUIContent("Limpar na Cena"));
                }

                // Validação inline
                ValidatePoolInline(config, index);
            }

            EditorGUILayout.Space(3);
        }

        /// <summary>
        /// Desenha seção de validação
        /// </summary>
        void DrawValidation()
        {
            showValidation = EditorGUILayout.Foldout(showValidation, "Validação", true);

            if (!showValidation) return;

            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                var errors = GetValidationErrors();
                var warnings = GetValidationWarnings();

                if (errors.Count == 0 && warnings.Count == 0)
                {
                    EditorGUILayout.HelpBox("Todas as configurações estão válidas!", MessageType.Info);
                }
                else
                {
                    foreach (var error in errors)
                        EditorGUILayout.HelpBox(error, MessageType.Error);

                    foreach (var warning in warnings)
                        EditorGUILayout.HelpBox(warning, MessageType.Warning);
                }

                EditorGUILayout.Space(5);

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Auto-detectar Tags"))
                    {
                        AutoDetectTags();
                    }

                    if (GUILayout.Button("Corrigir Problemas"))
                    {
                        AutoFix();
                    }
                }
            }
        }
        #endregion

        #region Validação
        /// <summary>
        /// Valida pool e mostra erros inline
        /// </summary>
        void ValidatePoolInline(PoolDatabase.PoolConfiguration config, int index)
        {
            // Tag vazia
            if (string.IsNullOrEmpty(config.poolTag))
            {
                EditorGUILayout.HelpBox("Tag é obrigatória!", MessageType.Error);
            }

            // Prefab nulo
            if (config.prefab == null)
            {
                EditorGUILayout.HelpBox("Prefab é obrigatório!", MessageType.Error);
            }

            // Tamanhos inválidos
            if (config.maxSize <= 0)
            {
                EditorGUILayout.HelpBox("Tamanho máximo deve ser > 0!", MessageType.Error);
            }
            else if (config.initialSize > config.maxSize)
            {
                EditorGUILayout.HelpBox("Tamanho inicial > máximo!", MessageType.Warning);
            }

            // Tag duplicada
            var duplicateTags = database.pools
                .Where((p, i) => i != index && !string.IsNullOrEmpty(p.poolTag) && p.poolTag == config.poolTag)
                .Any();

            if (duplicateTags)
            {
                EditorGUILayout.HelpBox("Tag duplicada encontrada!", MessageType.Error);
            }
        }

        /// <summary>
        /// Obtém lista de erros de validação
        /// </summary>
        System.Collections.Generic.List<string> GetValidationErrors()
        {
            var errors = new System.Collections.Generic.List<string>();

            if (database?.pools == null) return errors;

            // Tags vazias
            var emptyTags = database.pools.Where((p, i) => string.IsNullOrEmpty(p.poolTag)).Count();
            if (emptyTags > 0)
                errors.Add($"{emptyTags} pool(s) sem tag");

            // Prefabs nulos
            var nullPrefabs = database.pools.Where(p => p.prefab == null).Count();
            if (nullPrefabs > 0)
                errors.Add($"{nullPrefabs} pool(s) sem prefab");

            // Tamanhos inválidos
            var invalidSizes = database.pools.Where(p => p.maxSize <= 0).Count();
            if (invalidSizes > 0)
                errors.Add($"{invalidSizes} pool(s) com tamanho máximo inválido");

            // Tags duplicadas
            var duplicates = database.pools
                .Where(p => !string.IsNullOrEmpty(p.poolTag))
                .GroupBy(p => p.poolTag)
                .Where(g => g.Count() > 1)
                .Count();

            if (duplicates > 0)
                errors.Add($"{duplicates} tag(s) duplicada(s)");

            return errors;
        }

        /// <summary>
        /// Obtém lista de avisos de validação
        /// </summary>
        System.Collections.Generic.List<string> GetValidationWarnings()
        {
            var warnings = new System.Collections.Generic.List<string>();

            if (database?.pools == null) return warnings;

            // Initial > Max
            var invalidInitial = database.pools.Where(p => p.initialSize > p.maxSize).Count();
            if (invalidInitial > 0)
                warnings.Add($"{invalidInitial} pool(s) com tamanho inicial > máximo");

            // Pools muito grandes
            var largePools = database.pools.Where(p => p.maxSize > 1000).Count();
            if (largePools > 0)
                warnings.Add($"{largePools} pool(s) muito grande(s) (>1000)");

            return warnings;
        }
        #endregion

        #region Ações
        /// <summary>
        /// Adiciona novo pool
        /// </summary>
        void AddNewPool()
        {
            Undo.RecordObject(database, "Adicionar Pool");

            poolsProperty.arraySize++;
            var newPool = poolsProperty.GetArrayElementAtIndex(poolsProperty.arraySize - 1);

            newPool.FindPropertyRelative("poolTag").stringValue = $"NovoPool_{poolsProperty.arraySize}";
            newPool.FindPropertyRelative("prefab").objectReferenceValue = null;
            newPool.FindPropertyRelative("initialSize").intValue = 10;
            newPool.FindPropertyRelative("maxSize").intValue = 50;
            newPool.FindPropertyRelative("preWarm").boolValue = true;
            newPool.FindPropertyRelative("clearOnSceneLoad").boolValue = false;

            EditorUtility.SetDirty(database);
        }

        /// <summary>
        /// Remove pool
        /// </summary>
        void RemovePool(int index)
        {
            if (EditorUtility.DisplayDialog("Confirmar", "Remover este pool?", "Sim", "Não"))
            {
                Undo.RecordObject(database, "Remover Pool");
                poolsProperty.DeleteArrayElementAtIndex(index);
                EditorUtility.SetDirty(database);
            }
        }

        /// <summary>
        /// Auto-detecta tags dos prefabs
        /// </summary>
        void AutoDetectTags()
        {
            Undo.RecordObject(database, "Auto-detectar Tags");

            var existingTags = new System.Collections.Generic.HashSet<string>();

            for (int i = 0; i < database.pools.Length; i++)
            {
                if (database.pools[i].prefab != null && string.IsNullOrEmpty(database.pools[i].poolTag))
                {
                    var baseName = database.pools[i].prefab.name.Replace("(Clone)", "").Trim();
                    var uniqueName = GetUniqueTag(baseName, existingTags);
                    
                    database.pools[i].poolTag = uniqueName;
                    existingTags.Add(uniqueName);
                }
                else if (!string.IsNullOrEmpty(database.pools[i].poolTag))
                {
                    existingTags.Add(database.pools[i].poolTag);
                }
            }

            EditorUtility.SetDirty(database);
        }

        /// <summary>
        /// Corrige problemas automaticamente
        /// </summary>
        void AutoFix()
        {
            Undo.RecordObject(database, "Corrigir Problemas");

            var existingTags = new System.Collections.Generic.HashSet<string>();

            for (int i = 0; i < database.pools.Length; i++)
            {
                // Corrige tamanho máximo
                if (database.pools[i].maxSize <= 0)
                {
                    database.pools[i].maxSize = 50;
                }

                // Corrige tamanho inicial
                if (database.pools[i].initialSize > database.pools[i].maxSize)
                {
                    database.pools[i].initialSize = database.pools[i].maxSize;
                }
                
                if (database.pools[i].initialSize < 0)
                {
                    database.pools[i].initialSize = 0;
                }

                // Corrige tag vazia
                if (string.IsNullOrEmpty(database.pools[i].poolTag))
                {
                    var baseName = database.pools[i].prefab != null ? database.pools[i].prefab.name : "Pool";
                    database.pools[i].poolTag = GetUniqueTag(baseName, existingTags);
                }

                // Corrige tag duplicada
                if (existingTags.Contains(database.pools[i].poolTag))
                {
                    database.pools[i].poolTag = GetUniqueTag(database.pools[i].poolTag, existingTags);
                }

                existingTags.Add(database.pools[i].poolTag);
            }

            EditorUtility.SetDirty(database);
        }

        /// <summary>
        /// Gera tag única
        /// </summary>
        string GetUniqueTag(string baseTag, System.Collections.Generic.HashSet<string> existingTags)
        {
            if (!existingTags.Contains(baseTag))
                return baseTag;

            int counter = 1;
            string candidateTag;

            do
            {
                candidateTag = $"{baseTag}_{counter}";
                counter++;
            }
            while (existingTags.Contains(candidateTag));

            return candidateTag;
        }
        #endregion
    }
}