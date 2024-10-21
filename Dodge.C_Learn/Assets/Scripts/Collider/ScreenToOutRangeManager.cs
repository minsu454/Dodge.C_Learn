using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToOutRangeManager : MonoBehaviour
{
    [SerializeField] private Transform[] BlockTrArr;
    [SerializeField] private int farBlock;

    void Awake()
    {
        ResetBlock();
    }

    public void ResetBlock()
    {
        for (int i = 0; i < BlockTrArr.Length; i++)
        {
            DirType dir = BlockTrArr[i].GetComponent<ScreenToOutRangeCollider>().dir;
            Vector2 pos = Vector2.zero;

            switch (dir)
            {
                case DirType.Left:
                    pos = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height / 2f));
                    pos.x -= farBlock;
                    break;
                case DirType.Right:
                    pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height / 2f));
                    pos.x += farBlock;
                    break;
                case DirType.Top:
                    pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, Screen.height));
                    pos.y += farBlock;
                    break;
                case DirType.Bottom:
                    pos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width / 2f, 0));
                    pos.y -= farBlock;
                    break;
            }

            BlockTrArr[i].position = pos;
        }
    }
}
