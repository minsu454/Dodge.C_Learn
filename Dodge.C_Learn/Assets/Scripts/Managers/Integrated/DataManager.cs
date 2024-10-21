public sealed class DataManager : ICreate
{
    public PlayerType PlayerType {  get; private set; }     //플레이어 타입

    public void Init()
    {
        Managers.Event.Subscribe(GameEventType.SetPlayerType, SetPlayerType);
    }

    /// <summary>
    /// 플레이어 타입 설정해주는 함수
    /// </summary>
    private void SetPlayerType(object args)
    {
        PlayerType = (PlayerType)args;
    }
}