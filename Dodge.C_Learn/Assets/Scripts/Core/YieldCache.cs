using System.Collections.Generic;
using UnityEngine;

namespace Common.Yield
{
    public static class YieldCache
    {
        private static readonly Dictionary<float, WaitForSeconds> waitForSecondDic = new Dictionary<float, WaitForSeconds>();

        /// <summary>
        /// new WaitForSeconds를 dictionary에 가져오는 함수(dictionary에 값이 없을 시엔 add해줌)
        /// </summary>
        public static WaitForSeconds WaitForSeconds(float delayTime)
        {
            if (waitForSecondDic.TryGetValue(delayTime, out var waitForSeconds))
            {
                return waitForSeconds;
            }

            WaitForSeconds newSec = new WaitForSeconds(delayTime);
            waitForSecondDic.Add(delayTime, newSec);
            return newSec;
        }
    }
}