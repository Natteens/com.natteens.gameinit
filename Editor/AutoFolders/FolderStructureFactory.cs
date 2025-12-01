using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public static class FolderStructureFactory
    {
        public static List<IFolderStructure> CreateAllStructures()
        {
            return new List<IFolderStructure>
            {
                new ArtFolderStructure(),
                new CodeFolderStructure(),
                new DataFolderStructure(),
                new PrefabsFolderStructure(),
                new ResourcesFolderStructure(),
                new ScenesFolderStructure()
            };
        }
    
        public static IFolderStructure CreateStructure(string type)
        {
            switch (type)
            {
                case "Art": return new ArtFolderStructure();
                case "Code": return new CodeFolderStructure();
                case "Data": return new DataFolderStructure();
                case "Prefabs": return new PrefabsFolderStructure();
                case "Resources": return new ResourcesFolderStructure();
                case "Scenes": return new ScenesFolderStructure();
                default: return null;
            }
        }
    }
}