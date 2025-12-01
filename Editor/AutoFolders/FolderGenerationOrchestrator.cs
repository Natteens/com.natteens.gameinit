using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameInit.Editor.AutoFolders
{
    public class FolderGenerationOrchestrator
    {
        private readonly IFolderCreator folderCreator;
        private readonly List<IFolderStructure> structures;
    
        public FolderGenerationOrchestrator(IFolderCreator folderCreator)
        {
            this.folderCreator = folderCreator;
            structures = new List<IFolderStructure>();
        }
    
        public void AddStructure(IFolderStructure structure)
        {
            structures.Add(structure);
        }
    
        public void GenerateAll()
        {
            foreach (var structure in structures)
            {
                folderCreator.CreateFolders(structure);
            }
        
            AssetDatabase.Refresh();
            Debug.Log($"✓ {structures.Count} estruturas de pastas criadas com sucesso!");
        }
    }
}