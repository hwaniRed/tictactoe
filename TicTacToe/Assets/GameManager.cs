using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    main main = null;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager Start!!");
        // ControlManager 오브젝트 안에 ControlManager 스크립트를 가져온다.
        main = GameObject.Find("Canvas").GetComponent<main>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool checkMatch(List<List<int>> _pieceList)
    {
        bool rtrn = false;

        List<List<int>> pieceList = _pieceList;

        //가로줄 체크
        for( int i = 0; i < 3; i++)
        {
            int first  = pieceList[i][0];
            int second = pieceList[i][1];
            int third  = pieceList[i][2];

            //가로줄 체
            if( containEmpty(first, second, third))
            {
                Debug.Log("배열이 빈값을 포함하고 있습니다.");

               if(i < 2) continue;
               else break;
            }

            if (isSameValue(first, second, third))
            {
                Debug.Log("배열이 같은 값으로 이루어져 있습니다.");
                if (i < 2) continue;
                else break;
            }
        }



        return rtrn;
    }

    bool isSameValue(int _first, int _second, int _third)
    {
        bool result = false;

        int val = -1;
        if (_first == _second)
        {
            val = _first;
        }
        else
        {
            return result;
        }

        if(val == _third)
        {
            result = true;
        }
        else
        {
            result = false;
        }


        return result;
    }

    bool containEmpty(int first, int second, int third)
    {
        int[] tmpList = { first, second, third };

        return (-1 == Array.IndexOf(tmpList, -1)) ? false : true;
    }
}
