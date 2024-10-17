using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public static class CreatePattenSOService
{
    public static string LoadPath
    {
        get { return Path.GetFullPath(Application.dataPath + "../../../SavePatten"); }
    }

    public static string SavePath
    {
        get { return Path.Combine(Application.dataPath, "Resources/Stage/Patten"); }
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

            PattenSO pattenSO = new PattenSO();
            pattenSO.name = patten.name;
            pattenSO.patten = patten;

            string path = $"Assets/Resources/Stage/Patten/{patten.name}.asset";
            AssetDatabase.CreateAsset(pattenSO, path);
        }
    }
}