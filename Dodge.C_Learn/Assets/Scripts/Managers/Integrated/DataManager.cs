public class DataManager : ICreate
{
    public PlayerType PlayerType {  get; private set; }

    public void Init()
    {
        Managers.Event.Subscribe(GameEventType.SetPlayerType, SetPlayerType);
    }
    private void SetPlayerType(object args)
    {
        PlayerType = (PlayerType)args;
    }
}