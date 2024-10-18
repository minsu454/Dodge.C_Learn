using UnityEditor;

public class CreateSONenu
{
    [MenuItem("Tools/CreateSO/DeleteAll")]
    public static void DeleteAllPattenSO()
    {
        EditorUtility.DisplayProgressBar("Delete All", "File Delete..", 0.0f);

        CreatePattenSOService.DeletePattenSO();

        EditorUtility.DisplayProgressBar("Delete All", "Done..", 1.0f);

        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }

    [MenuItem("Tools/CreateSO/CreatePattenSO")]
    public static void CreatePattenSO()
    {
        EditorUtility.DisplayProgressBar("Create PattenSO", "Generate Create PattenSO..", 0.0f);

        CreatePattenSOService.DeletePattenSO();
        CreatePattenSOService.CreatePattenSO();

        EditorUtility.DisplayProgressBar("Create PattenSO", "Done..", 1.0f);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.ClearProgressBar();
    }
}
