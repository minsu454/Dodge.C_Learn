using SimpleFileBrowser;
using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class MapEditorPopup : BasePopup
{
    [SerializeField] private TMP_InputField inputX;
    [SerializeField] private TMP_InputField inputY;

    [SerializeField] private GameObject DataUI;
    [SerializeField] private GameObject PointDataUI;
    [SerializeField] private GameObject CreateUI;

    private bool isOnDataUI = true;
    private bool isOnPointDataUI = true;
    private bool isCreateUI = true;

    protected override void Init()
    {
        base.Init();

        inputX.onSubmit.AddListener(OnInputX);
        inputY.onSubmit.AddListener(OnInputY);

        PattenGenerator.Instance.controller.OnMove += OnSeeText;
    }

    private void OnSeeText(SpawnPoint spawnPoint)
    {
        inputX.text = spawnPoint.transform.position.x.ToString();
        inputY.text = spawnPoint.transform.position.y.ToString();
    }

    private void OnInputX(string s)
    {
        SpawnPoint go = PattenGenerator.Instance.ChoiceSpawnPoint();
        Vector2 pos = go.transform.position;
        go.transform.position = new Vector2(float.Parse(s), pos.y);
    }

    private void OnInputY(string s)
    {
        SpawnPoint go = PattenGenerator.Instance.ChoiceSpawnPoint();
        Vector2 pos = go.transform.position;
        go.transform.position = new Vector2(pos.x, float.Parse(s));
    }

    /// <summary>
    /// point생성 버튼
    /// </summary>
    public void CreatePoint()
    {
        PattenGenerator.Instance.CreatePoint();
    }

    /// <summary>
    /// 저장 버튼
    /// </summary>
    public void Save()
    {
        Managers.Popup.CreatePopup(PopupType.FileBrowserPopup, false, false);

        string initialFilename = "SaveData_" + DateTime.Now.ToString(("MM_dd_HH_mm_ss")) + ".json";

        StartCoroutine(ShowSaveDialogCoroutine(initialFilename));
    }

    /// <summary>
    /// 로드 버튼
    /// </summary>
    public void Load()
    {
        Managers.Popup.CreatePopup(PopupType.FileBrowserPopup, false, false);

        StartCoroutine(ShowLoadDialogCoroutine());
    }

    public void DownDataArrow()
    {
        if (isOnDataUI)
        {
            DataUI.transform.position = new Vector2(DataUI.transform.position.x, DataUI.transform.position.y - 240);
        }
        else
        {
            DataUI.transform.position = new Vector2(DataUI.transform.position.x, DataUI.transform.position.y + 240);
        }

        isOnDataUI = !isOnDataUI;
    }

    public void DownPointDataArrow()
    {
        if (isOnPointDataUI)
        {
            PointDataUI.transform.position = new Vector2(PointDataUI.transform.position.x, PointDataUI.transform.position.y - 300);
        }
        else
        {
            PointDataUI.transform.position = new Vector2(PointDataUI.transform.position.x, PointDataUI.transform.position.y + 300);
        }

        isOnPointDataUI = !isOnPointDataUI;
    }

    public void DownCreateArrow()
    {
        if (isCreateUI)
        {
            CreateUI.transform.position = new Vector2(CreateUI.transform.position.x, CreateUI.transform.position.y - 120);
        }
        else
        {
            CreateUI.transform.position = new Vector2(CreateUI.transform.position.x, CreateUI.transform.position.y + 120);
        }

        isCreateUI = !isCreateUI;
    }

    /// <summary>
    /// save버튼이 누르면 파일로 만들어주는 코루틴
    /// </summary>
    IEnumerator ShowSaveDialogCoroutine(string initialFilename)
    {
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.FilesAndFolders, false, null, initialFilename, "Save Files and Folders", "Save");

        if (FileBrowser.Success)
        {
            string path = FileBrowser.Result[0];

            string name = Path.GetFileNameWithoutExtension(path);           //파일명만 따오는 함수
            string json = PattenGenerator.Instance.DataToJson(name);

            System.IO.File.WriteAllText(path, json);
        }
    }

    /// <summary>
    /// load버튼이 누르면 파일 로드해주는 코루틴
    /// </summary>
    IEnumerator ShowLoadDialogCoroutine()
    {
        yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");

        if (FileBrowser.Success)
        {
            PattenGenerator.Instance.LoadData(FileBrowser.Result[0]);
        }
    }
}
