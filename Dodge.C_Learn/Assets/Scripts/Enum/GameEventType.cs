/// <summary>
/// 게임 이벤트 타입(구독하기 위한)
/// </summary>
public enum GameEventType
{
    LockInput = 0,                  //new Input System 잠굴 때
    StayCameraView,                 //카메라 스크롤 움직임을 멈출 때
    FollowMouse,                    //마우스 따라오게 할 때

    EnemyMoveTimerCompleted = 100,  //Enemy의 내려가는 Timer가 끝났을 때
    SetPlayerType,                  //title에서 플레이어가 설정될 때
}
