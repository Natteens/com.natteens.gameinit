using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public class ScenesFolderStructure : IFolderStructure
    {
        public string GetRootFolder() => "Scenes";
    
        public Dictionary<string, List<string>> GetStructure()
        {
            return new Dictionary<string, List<string>>
            {
                { "", new List<string> { "Menus", "Gameplay", "Test", "Loading" } }
            };
        }
    }
}