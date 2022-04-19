using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    main main = null;
    String[] ruleConstants = new String[] { "HOR01", "HOR02", "HOR03", "VER01", "HOR02", "HOR03", "CROSS01", "CROSS02" };


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

        int first = 0;
        int second = 0;
        int third = 0;

        //가로줄 체크
        for( int i = 0; i < 3; i++)
        {
            first  = pieceList[i][0];
            second = pieceList[i][1];
            third  = pieceList[i][2];

            //가로줄 빈값 체크
            if( containEmpty(first, second, third))
            {
                Debug.Log("배열이 빈값을 포함하고 있습니다.");

               if(i < 2) continue;
               else break;
            }

            //가로줄 같은 값 잇는지 체크
            if (isSameValue(first, second, third))
            {
                Debug.Log("배열이 같은 값으로 이루어져 있습니다.");
                if (i < 2) continue;
                else break;
            }
        }

        // 세로줄 체크
        for( int j = 0; j < 3; j++)
        {
            first = pieceList[0][j];
            second = pieceList[1][j];
            third = pieceList[2][j];

            //가로줄 빈값 체크
            if (containEmpty(first, second, third))
            {
                Debug.Log("배열이 빈값을 포함하고 있습니다.");

                if (j < 2) continue;
                else break;
            }

            //가로줄 같은 값 잇는지 체크
            if (isSameValue(first, second, third))
            {
                Debug.Log("배열이 같은 값으로 이루어져 있습니다.");
                if (j < 2) continue;
                else break;
            }
        }


        first = pieceList[0][0];
        second = pieceList[1][1];
        third = pieceList[2][2];

        //좌우상하 대각선 빈값 체크
        if (containEmpty(first, second, third))
        {
            Debug.Log("배열이 빈값을 포함하고 있습니다.");
        }

        //좌우상하 대각선 같은값 체크
        if (isSameValue(first, second, third))
        {
            Debug.Log("배열이 같은 값으로 이루어져 있습니다.");
        }

        first = pieceList[0][2];
        second = pieceList[1][1];
        third = pieceList[2][0];

        //우좌상하 대각선 빈값 체크
        if (containEmpty(first, second, third))
        {
            Debug.Log("배열이 빈값을 포함하고 있습니다.");
        }

        //우좌상하 대각선 같은값 체크
        if (isSameValue(first, second, third))
        {
            Debug.Log("배열이 같은 값으로 이루어져 있습니다.");
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
