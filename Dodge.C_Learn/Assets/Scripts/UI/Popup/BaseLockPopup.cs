public class BaseLockPopup : BasePopup
{
    protected override void Init()
    {
        base.Init();
        Managers.Event.Dispatch(GameEventType.LockInput, false);
    }

    protected override void Close()
    {
        Managers.Event.Dispatch(GameEventType.LockInput, true);
        base.Close();
    }
}
