using SimpleFileBrowser;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class SimpleFileBrowserPopup : BasePopup
{
    public override void Init()
    {
        base.Init();

        FileBrowser.SetFilters(true, new FileBrowser.Filter(".Files", ".json"), new FileBrowser.Filter(".Files", ".json"));

        FileBrowser.SetDefaultFilter(".json");
        FileBrowser.AddQuickLink("Users", Application.dataPath, null) ;

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    public void LoadCanvas(string jsonText = null)
    {
        //if (jsonText == null)
        //{
        //    StartCoroutine(ShowLoadDialogCoroutine());
        //    return;
        //}

        //string initialPath = "C:\\Users\\[YourPath]";
        //string initialFilename = "SaveData_" + DateTime.Now.ToString(("MM_dd_HH_mm_ss")) + ".json";
        //FileBrowser.ShowSaveDialog(null, null, FileBrowser.PickMode.Files, false, initialPath, initialFilename, "Save As", "Save");

        //StartCoroutine(ShowSaveDialogCoroutine(jsonText));
    }

    IEnumerator ShowSaveDialogCoroutine(string text, string initialPath = null, string initialFilename = null)
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, false, initialPath, initialFilename, "Save Files and Folders", "Save");

        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];
            File.WriteAllText(path, text);
        }
    }

    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            // Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
            for (int i = 0; i < FileBrowser.Result.Length; i++)
                Debug.Log(FileBrowser.Result[i]);

            // Read the bytes of the first file via FileBrowserHelpers
            // Contrary to File.ReadAllBytes, this function works on Android 10+, as well
            byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

            // Or, copy the first file to persistentDataPath
            string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
            FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
        }
    }

    public override void Close()
    {
        base.Close();
    }
}
