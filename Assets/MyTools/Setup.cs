using System.IO;
using UnityEngine;
using UnityEditor;

public static class Setup
{
    [MenuItem("Tools/Setup/Create Default Folders")]
    public static void CreateDefaultFolders()
    {
        Folders.CreateDefault("_Project", "Animation", "Art", "Materials", "Music", "Prefabs", "ScriptableObjects", "Scripts", "Settings" );
        AssetDatabase.Refresh();
    }

    static class Folders
    {
        public static void CreateDefault(string root, params string[] folders)
        {
            var fullPath = Path.Combine(Application.dataPath, root);

            foreach (var folder in folders)
            {
                var path = Path.Combine(fullPath, folder);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
        }
    }
}