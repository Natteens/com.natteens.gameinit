using System.Collections.Generic;

namespace GameInit.Editor.AutoFolders
{
    public interface IFolderStructure
    {
        string GetRootFolder();
        Dictionary<string, List<string>> GetStructure();
    }
}