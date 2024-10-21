using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class MapEditorPopup : BasePopup
{
    [Header("InputField")]
    [SerializeField] private TMP_InputField inputX;             //inputField x값
    [SerializeField] private TMP_InputField inputY;             //inputField Y값

    [Header("Dropdown")]
    [SerializeField] private TMP_Dropdown dropdown;             //드롭바

    [Header("Toggle")]
    [SerializeField] private Toggle stayCameraViewToggle;       //카메라잠구기 선택박스
    [SerializeField] private Toggle fallowCameraToggle;         //spawnPoint 카메라 따라올지 선택박스

    [SerializeField] private List<ArrowButton> arrowButtonList; //화살표로 움직일 수 있는 obj List

    protected override void Init()
    {
        base.Init();

        inputX.onSubmit.AddListener(OnInputXEvent);
        inputY.onSubmit.AddListener(OnInputYEvent);

        dropdown.onValueChanged.AddListener(OnDropdownEvent);

        stayCameraViewToggle.onValueChanged.AddListener(OnStayCameraViewEvent);
        fallowCameraToggle.onValueChanged.AddListener(OnFollowCameraEvent);

        SetDropdown();
        PattenGenerator.Instance.controller.OnMove += OnMoveSeeText;
        PattenGenerator.Instance.controller.OnSpawn += SetDropdown;
    }

    #region DropDown

    /// <summary>
    /// Dropdown바꿀 때 그것으로 value바꿔주는 함수
    /// </summary>
    private void OnDropdownEvent(int index)
    {
        SpawnPoint go = PattenGenerator.Instance.spawnPoint;

        if (go == null)
            return;

        dropdown.value = index;
        go.EnemyType = (EnemyType)index;
    }

    /// <summary>
    /// Dropdown에 enemy값 하나하나 채워주는 함수
    /// </summary>
    private void SetDropdown()
    {
        dropdown.ClearOptions();

        var optionList = new List<TMP_Dropdown.OptionData>();

        foreach (var type in Enum.GetValues(typeof(EnemyType)))
        {
            optionList.Add(new TMP_Dropdown.OptionData(type.ToString()));
        }

        dropdown.AddOptions(optionList);
    }

    private void SetDropdown(EnemyType type)
    {
        dropdown.value = (int)type;
    }

    #endregion

    #region InputField

    /// <summary>
    /// 움직일 때 x y좌표 text 바꿔주는 함수
    /// </summary>
    private void OnMoveSeeText(SpawnPoint spawnPoint)
    {
        inputX.text = spawnPoint.transform.position.x.ToString();
        inputY.text = spawnPoint.transform.position.y.ToString();
    }

    private void OnInputXEvent(string s)
    {
        SpawnPoint go = PattenGenerator.Instance.spawnPoint;

        Vector2 pos = go.transform.position;
        go.transform.position = new Vector2(float.Parse(s), pos.y);
    }

    private void OnInputYEvent(string s)
    {
        SpawnPoint go = PattenGenerator.Instance.spawnPoint;

        Vector2 pos = go.transform.position;
        go.transform.position = new Vector2(pos.x, float.Parse(s));
    }

    #endregion

    private void OnStayCameraViewEvent(bool isOn)
    {
        Managers.Event.Dispatch(GameEventType.StayCameraView, isOn);
    }

    private void OnFollowCameraEvent(bool isOn)
    {
        Managers.Event.Dispatch(GameEventType.FollowCamera, isOn);
    }

    #region Data

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

    #endregion


    /// <summary>
    /// 받아온 인덱스에 짜투리창 올렸다 내렸다 해주는 함수
    /// </summary>
    public void MoveArrowButton(int idx)
    {
        if (idx < 0 || arrowButtonList.Count <= idx)
        {
            return;
        }
        arrowButtonList[idx].MoveArrowButton();
    }

    /// <summary>
    /// point생성 버튼
    /// </summary>
    public void CreatePoint()
    {
        PattenGenerator.Instance.CreatePoint();
    }
}
