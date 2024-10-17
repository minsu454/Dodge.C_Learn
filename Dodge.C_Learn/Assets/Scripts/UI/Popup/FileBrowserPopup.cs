using SimpleFileBrowser;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class FileBrowserPopup : BaseLockPopup
{
    public override void Init()
    {
        base.Init();

        FileBrowser.SetFilters(true, new FileBrowser.Filter(".Files", ".json"), new FileBrowser.Filter(".Files", ".json"));
        FileBrowser.SetDefaultFilter(".json");

        FileBrowser.AddQuickLink("Users", Application.dataPath, null);
    }

    private void OnDisable()
    {
        Close();
    }
}