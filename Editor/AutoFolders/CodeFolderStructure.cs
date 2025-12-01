using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public class CodeFolderStructure : IFolderStructure
    {
        public string GetRootFolder() => "Code";
    
        public Dictionary<string, List<string>> GetStructure()
        {
            return new Dictionary<string, List<string>>
            {
                { "", new List<string> { "Scripts", "Shaders", "Editor" } },
                { "Scripts", new List<string> { "Managers", "Controllers", "UI", "Player", "Enemy", "Utils", "Systems", "Gameplay", "Data", "Interfaces", "Events" } },
                { "Scripts/Managers", new List<string> { "Game", "Audio", "Scene", "Save", "Input" } },
                { "Scripts/Controllers", new List<string> { "Camera", "Character", "AI", "Physics" } },
                { "Scripts/UI", new List<string> { "Menus", "HUD", "Popups", "Elements" } },
                { "Scripts/Gameplay", new List<string> { "Combat", "Movement", "Inventory", "Quests", "Dialogue" } },
                { "Scripts/Systems", new List<string> { "SaveSystem", "EventSystem", "PoolSystem", "LevelSystem" } },
                { "Scripts/Utils", new List<string> { "Helpers", "Extensions", "Tools" } },
                { "Editor", new List<string> { "Tools", "Inspectors", "Windows" } }
            };
        }
    }
}