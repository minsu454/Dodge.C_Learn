using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void EventListener(object args);

public sealed class EventManager
{
    //eventListener저장해주는 Dictionary
    private readonly Dictionary<GameEventType, List<EventListener>> eventListenerDic = new Dictionary<GameEventType, List<EventListener>>();

    /// <summary>
    /// 구독하는 함수
    /// </summary>
    public void Subscribe(GameEventType type, EventListener listener)
    {
        if (!eventListenerDic.TryGetValue(type, out var list))
        {
            list = new List<EventListener>();
            eventListenerDic[type] = list;
        }

        list.Add(listener);
    }

    /// <summary>
    /// 구독취소하는 함수
    /// </summary>
    public void Unsubscribe(GameEventType type, EventListener listener)
    {
        if (!eventListenerDic.TryGetValue(type, out var list))
        {
            return;
        }

        list.Remove(listener);
        if (list.Count == 0)
        {
            eventListenerDic.Remove(type);
        }
    }

    /// <summary>
    /// 이벤트 달성 시 구독한 리스트 실행해주는 함수
    /// </summary>
    public void Dispatch(GameEventType type, object arg)
    {
        if (!eventListenerDic.TryGetValue(type, out var list))
        {
            return;
        }   

        foreach (var listener in list)
        {
            try
            {
                listener.Invoke(arg);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
