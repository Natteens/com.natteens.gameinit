using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameInit.Editor.AutoFolders
{
    public class ProjectFolderGenerator : EditorWindow
    {
        private Dictionary<string, bool> selectedStructures;
    
        [MenuItem("Tools/Generate Project Folders")]
        public static void ShowWindow()
        {
            GetWindow<ProjectFolderGenerator>("Folder Generator");
        }
    
        private void OnEnable()
        {
            selectedStructures = new Dictionary<string, bool>
            {
                { "Art", true },
                { "Code", true },
                { "Data", true },
                { "Prefabs", true },
                { "Resources", true },
                { "Scenes", true }
            };
        }
    
        private void OnGUI()
        {
            GUILayout.Label("Project Folder Structure Generator", EditorStyles.boldLabel);
            GUILayout.Space(10);
        
            GUILayout.Label("Selecione as estruturas que deseja criar:", EditorStyles.label);
            GUILayout.Space(5);
        
            DrawStructureToggles();
        
            GUILayout.Space(20);
        
            if (GUILayout.Button("Gerar Pastas Selecionadas", GUILayout.Height(40)))
            {
                GenerateSelectedFolders();
            }
        
            GUILayout.Space(10);
        
            if (GUILayout.Button("Gerar Estrutura Completa", GUILayout.Height(30)))
            {
                GenerateAllFolders();
            }
        }
    
        private void DrawStructureToggles()
        {
            var keys = new List<string>(selectedStructures.Keys);
        
            foreach (var key in keys)
            {
                selectedStructures[key] = EditorGUILayout.Toggle(key, selectedStructures[key]);
            }
        }
    
        private void GenerateSelectedFolders()
        {
            var orchestrator = CreateOrchestrator();
        
            foreach (var kvp in selectedStructures)
            {
                if (kvp.Value)
                {
                    var structure = FolderStructureFactory.CreateStructure(kvp.Key);
                    orchestrator.AddStructure(structure);
                }
            }
        
            orchestrator.GenerateAll();
            ShowSuccessDialog();
        }
    
        private void GenerateAllFolders()
        {
            var orchestrator = CreateOrchestrator();
            var allStructures = FolderStructureFactory.CreateAllStructures();
        
            foreach (var structure in allStructures)
            {
                orchestrator.AddStructure(structure);
            }
        
            orchestrator.GenerateAll();
            ShowSuccessDialog();
        }
    
        private FolderGenerationOrchestrator CreateOrchestrator()
        {
            var folderCreator = new UnityFolderCreator("Assets");
            return new FolderGenerationOrchestrator(folderCreator);
        }
    
        private void ShowSuccessDialog()
        {
            EditorUtility.DisplayDialog("Sucesso", "Estrutura de pastas criada com sucesso!", "OK");
        }
    }
}