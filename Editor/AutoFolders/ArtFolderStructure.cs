using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public class ArtFolderStructure : IFolderStructure
    {
        public string GetRootFolder() => "Art";
    
        public Dictionary<string, List<string>> GetStructure()
        {
            return new Dictionary<string, List<string>>
            {
                { "", new List<string> { "Animations", "Materials", "Models", "Textures", "Sprites", "VFX", "Fonts" } },
                { "Animations", new List<string> { "Characters", "UI", "Environment", "Cutscenes" } },
                { "Materials", new List<string> { "Characters", "Environment", "UI", "Effects", "PostProcessing" } },
                { "Models", new List<string> { "Characters", "Environment", "Props", "Weapons", "Items" } },
                { "Textures", new List<string> { "Characters", "Environment", "UI", "Icons", "Sprites", "Particles" } },
                { "Sprites", new List<string> { "Characters", "UI", "Icons", "Items", "Effects" } },
                { "VFX", new List<string> { "Particles", "Trails", "PostProcessing", "Shaders" } }
            };
        }
    }
}