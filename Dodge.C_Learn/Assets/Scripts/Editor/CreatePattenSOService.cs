using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class CreatePattenSOService
{
    public static string LoadPath
    {
        get { return Path.GetFullPath(Application.dataPath + "../../../SavePattern"); }
    }

    public static string SavePath
    {
        get { return Path.Combine(Application.dataPath, "Resources/StageSO/Pattern"); }
    }

    public static void DeletePattenSO()
    {
        foreach (string _file in Directory.GetFiles(SavePath))
        {
            File.Delete(_file);
        }
    }

    public static void CreatePattenSO()
    {

        foreach (string _file in Directory.GetFiles(LoadPath))
        {
            string json = File.ReadAllText(_file);
            Pattern patten = JsonUtility.FromJson<Pattern>(json);

            string path = $"Assets/Resources/StageSO/Pattern/{patten.name}.asset";

            if (AssetDatabase.LoadAssetAtPath<PatternSO>(path) != null)
            {
                continue;
            }

            PatternSO pattenSO = new PatternSO();
            pattenSO.name = patten.name;
            pattenSO.pattern = patten;

            AssetDatabase.CreateAsset(pattenSO, path);
        }
    }
}