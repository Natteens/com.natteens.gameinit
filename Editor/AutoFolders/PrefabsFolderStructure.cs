using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public class PrefabsFolderStructure : IFolderStructure
    {
        public string GetRootFolder() => "Prefabs";
    
        public Dictionary<string, List<string>> GetStructure()
        {
            return new Dictionary<string, List<string>>
            {
                { "", new List<string> { "Characters", "Environment", "UI", "Effects", "Weapons", "Items", "Props", "Audio", "Gameplay" } },
                { "Characters", new List<string> { "Player", "Enemies", "NPCs" } },
                { "Environment", new List<string> { "Terrain", "Buildings", "Nature", "Decoration" } },
                { "UI", new List<string> { "Menus", "HUD", "Popups", "Buttons", "Panels" } },
                { "Effects", new List<string> { "Particles", "VFX", "PostProcessing", "Lights" } },
                { "Weapons", new List<string> { "Melee", "Ranged", "Magic" } },
                { "Items", new List<string> { "Consumables", "Equipment", "Collectibles" } },
                { "Gameplay", new List<string> { "Triggers", "Spawners", "Checkpoints", "Interactables" } }
            };
        }
    }
}