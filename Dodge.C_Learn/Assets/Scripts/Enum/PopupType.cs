/// <summary>
/// 팝업 타입
/// </summary>
public enum PopupType
{
    MapEditorPopup = 0,     //맵에디터 기본 팝업
    FileBrowserPopup,       //파일브라우저 팝업

    TitlePopup = 10,        //타이틀 기본 팝업
    SelectPopup,            //캐릭터 선택 팝업

    InGamePopup = 100,      //인게임 기본 팝업
    PausePopup,             //퍼즈 팝업
    GameOverPopup,          //게임오버 팝업
}
