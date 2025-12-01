using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public class ResourcesFolderStructure : IFolderStructure
    {
        public string GetRootFolder() => "Resources";
    
        public Dictionary<string, List<string>> GetStructure()
        {
            return new Dictionary<string, List<string>>
            {
                { "", new List<string> { "Audio", "Localization", "Data" } },
                { "Audio", new List<string> { "Music", "SFX", "Ambience", "UI", "Voice" } },
                { "Audio/Music", new List<string> { "Menu", "Gameplay", "Battle", "Ambient" } },
                { "Audio/SFX", new List<string> { "Character", "Environment", "UI", "Combat", "Items" } },
                { "Localization", new List<string> { "EN", "PT", "ES", "FR" } }
            };
        }
    }
}