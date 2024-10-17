using TMPro;
using UnityEngine;

public class MapEditorPopup : BasePopup
{
    [SerializeField] private TMP_InputField inputX;
    [SerializeField] private TMP_InputField inputY;
    

    public override void Init()
    {
        base.Init();

        inputX.onSubmit.AddListener(OnInputX);
        inputY.onSubmit.AddListener(OnInputY);

        MapGenerator.Instance.controller.OnMove += OnSeeText;
    }

    private void OnSeeText(SpawnPoint spawnPoint)
    {
        inputX.text = spawnPoint.transform.position.x.ToString();
        inputY.text = spawnPoint.transform.position.y.ToString();
    }

    private void OnInputX(string s)
    {
        //SpawnPoint go = MapGenerator.Instance.ChoiceSpawnPoint();
        //Vector2 pos = go.transform.position;
        //go.transform.position = new Vector2(pos.x + float.Parse(s), pos.y);
    }

    private void OnInputY(string s)
    {
        //SpawnPoint go = MapGenerator.Instance.ChoiceSpawnPoint();
        //Vector2 pos = go.transform.position;
        //go.transform.position = new Vector2(pos.x, pos.y + float.Parse(s));
    }

    public void CreatePoint()
    {
        MapGenerator.Instance.CreatePoint();
    }

    public void Save()
    {
        CreateSimpleFileBrowserPopup().LoadCanvas("d");
    }

    public void Load()
    {
        CreateSimpleFileBrowserPopup().LoadCanvas();
    }

    public SimpleFileBrowserPopup CreateSimpleFileBrowserPopup()
    {
        BasePopup popup = Managers.Popup.CreatePopup(PopupType.SimpleFileBrowserPopup, false, false);
        return popup as SimpleFileBrowserPopup;
    }
}
