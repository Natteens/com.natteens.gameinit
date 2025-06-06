#if UNITY_EDITOR
using GameInit.Hierarchy;
using UnityEditor;
using UnityEngine;

namespace GameInit.Editor.Hierarchy
{
    /// <summary>
    /// Comandos de menu para criação de elementos customizados na hierarchy
    /// </summary>
    public static class HierarchyMenuCommands
    {
        private const string MENU_PATH = "GameObject/Hierarchy/Create Custom Header";
        private const string DEFAULT_HEADER_NAME = "HEADER";

        /// <summary>
        /// Cria um GameObject com componente HierarchyHeader através do menu de contexto
        /// </summary>
        [MenuItem(MENU_PATH)]
        private static void CreateCustomHeader(MenuCommand command)
        {
            var headerObject = CreateHeaderGameObject(command.context as GameObject);
            RegisterUndoAndSelect(headerObject);
        }

        /// <summary>
        /// Cria e configura o GameObject para o header customizado
        /// </summary>
        private static GameObject CreateHeaderGameObject(GameObject parent)
        {
            var obj = new GameObject(DEFAULT_HEADER_NAME);
            GameObjectUtility.SetParentAndAlign(obj, parent);
            obj.AddComponent<HierarchyHeader>();
            
            return obj;
        }

        /// <summary>
        /// Registra a operação no sistema de Undo e seleciona o objeto criado
        /// </summary>
        private static void RegisterUndoAndSelect(GameObject obj)
        {
            Undo.RegisterCreatedObjectUndo(obj, "Create Hierarchy Header");
            Selection.activeObject = obj;
        }
    }
}
#endif