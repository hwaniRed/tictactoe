using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class main : MonoBehaviour, IPointerClickHandler
{
    List<List<int>> pieceList = null;
    Turn turn;

    enum Turn
    {
        User
        , Cpu
    }

    // Start is called before the first frame update
    void Start()
    {
        turn = Turn.User;

        arrayInitiate();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Button onBtnClick(Button button)
    {
        Debug.Log(button.name);
        if(turn == Turn.User)
        {
            
        }else
        {
            return null;
        }

        return button;
    }

    bool isOkCheck(int xIdx, int yIdx)
    {
        bool isOk = false;

        int val = pieceList[xIdx][yIdx];

        if(val == -1)
        {
            isOk = true;
        }

        return isOk;
    }


    List<List<int>> arrayInitiate()
    {
        int len = 3;

        pieceList = new List<List<int>>();
        for ( int i = 0; i < 3; i++)
        {
            List<int> tmpList = new List<int>();
            for ( int j = 0; j < 3; j++)
            {
                tmpList.Add(-1);
            }

            pieceList.Add(tmpList);
        }

        return pieceList;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        string objName = eventData.pointerCurrentRaycast.gameObject.name;

        int xIdx = -1;
        int yIdx = -1;
        if (objName.Contains('_'))
        {
            xIdx = Int32.Parse(objName.Split('_')[1]);
            yIdx = Int32.Parse(objName.Split('_')[2]);
            Debug.Log($"xIdx : {xIdx}/ yIdx : {yIdx}");
        }

        if (turn == Turn.User)
        {
            if(isOkCheck(xIdx, yIdx))
            {
                pieceList[xIdx][yIdx] = (int)Turn.User;
                //말 체크

                turn = Turn.Cpu;
            }
        }
        else
        {
            return;
        }

    }
}
