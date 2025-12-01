using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace GameInit.Editor.AutoFolders
{
    public class UnityFolderCreator : IFolderCreator
    {
        private readonly string basePath;
    
        public UnityFolderCreator(string basePath = "Assets")
        {
            this.basePath = basePath;
        }
    
        public void CreateFolders(IFolderStructure structure)
        {
            string rootFolder = structure.GetRootFolder();
            string rootPath = Path.Combine(basePath, rootFolder);
        
            CreateFolder(basePath, rootFolder);
        
            Dictionary<string, List<string>> folderStructure = structure.GetStructure();
        
            foreach (var kvp in folderStructure)
            {
                string parentFolder = string.IsNullOrEmpty(kvp.Key) 
                    ? rootPath 
                    : Path.Combine(rootPath, kvp.Key);
            
                foreach (string folder in kvp.Value)
                {
                    CreateFolder(parentFolder, folder);
                }
            }
        }
    
        private void CreateFolder(string parentPath, string folderName)
        {
            string fullPath = Path.Combine(parentPath, folderName);
        
            if (!AssetDatabase.IsValidFolder(fullPath))
            {
                AssetDatabase.CreateFolder(parentPath, folderName);
            }
        }
    }
}