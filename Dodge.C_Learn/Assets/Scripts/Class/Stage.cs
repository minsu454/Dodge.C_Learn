using System;

[Serializable]
public class Stage
{
    public float NextStageTime;     //다음 스테이지생성까지 시간
    public float DurationTime;      //스테이지 지속시간
    public PatternSO PatternList;   //패턴
}