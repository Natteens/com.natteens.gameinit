using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public class DataFolderStructure : IFolderStructure
    {
        public string GetRootFolder() => "Data";
    
        public Dictionary<string, List<string>> GetStructure()
        {
            return new Dictionary<string, List<string>>
            {
                { "", new List<string> { "ScriptableObjects", "Configs", "Levels", "Saves", "JSON" } },
                { "ScriptableObjects", new List<string> { "Characters", "Items", "Weapons", "Enemies", "Skills", "Quests" } },
                { "Configs", new List<string> { "Game", "Audio", "Graphics", "Input" } },
                { "Levels", new List<string> { "LevelData", "LevelConfigs" } }
            };
        }
    }
}