/// <summary>
/// input system 잠글 때 쓰는 팝업 상속 class
/// </summary>
public class BaseLockPopup : BasePopup
{
    /// <summary>
    /// 생성 함수
    /// </summary>
    protected override void Init()
    {
        base.Init();
        Managers.Event.Dispatch(GameEventType.LockInput, false);
    }

    /// <summary>
    /// 닫기 함수
    /// </summary>
    public override void Close()
    {
        Managers.Event.Dispatch(GameEventType.LockInput, true);
        base.Close();
    }
}
