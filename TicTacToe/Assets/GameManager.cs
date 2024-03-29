﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    List<List<int>> pieceList = null;

    List<String> emptySpaceList = null;
    main main = null;

    List<Enum> turnAry = null;

    int turnCount;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("GameManager Start!!");
        // ControlManager 오브젝트 안에 ControlManager 스크립트를 가져온다.
        main = GameObject.Find("Canvas").GetComponent<main>();

        turnAry = new List<Enum>();
        turnCount = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<String, object> checkMatch(List<List<int>> _pieceList)
    {
        Dictionary<String, object> returnMap = new Dictionary<String, object>();

        bool rtrn = false;
        String checkedLine = "";

        pieceList = _pieceList;

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
                Debug.Log($"HOR_{i+1} : 배열이 빈값을 포함하고 있습니다.");

               if(i < 2) continue;
               else break;
            }

            //가로줄 같은 값 잇는지 체크
            if (isSameValue(first, second, third))
            {
                Debug.Log($"HOR_{i+1} : 배열이 같은 값으로 이루어져 있습니다.");
                checkedLine = "HOR0" + (i+1);   
                rtrn = true;  
                returnMap.Add("isCheck", rtrn);
                returnMap.Add("checkedLine", checkedLine);
                
                return returnMap;
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
                Debug.Log($"VER_{j+1} : 배열이 빈값을 포함하고 있습니다.");

                if (j < 2) continue;
                else break;
            }

            //가로줄 같은 값 잇는지 체크
            if (isSameValue(first, second, third))
            {
                Debug.Log($"VER_{j+1} : 배열이 같은 값으로 이루어져 있습니다.");
                rtrn = true;     
                checkedLine = "VER0" + (j+1);     
                returnMap.Add("isCheck", rtrn);
                returnMap.Add("checkedLine", checkedLine);
                
                return returnMap;
            }
        }


        first = pieceList[0][0];
        second = pieceList[1][1];
        third = pieceList[2][2];

        //좌우상하 대각선 빈값 체크
        if (!containEmpty(first, second, third))
        {
            Debug.Log("CROSS01 : 배열이 값이 있습니다.");
            //좌우상하 대각선 같은값 체크
            if (isSameValue(first, second, third))
            {
                Debug.Log("CROSS01 : 배열이 같은 값으로 이루어져 있습니다.");
                rtrn = true;     
                checkedLine = Constants.CROSS_01;     
                returnMap.Add("isCheck", rtrn);
                returnMap.Add("checkedLine", checkedLine);
                
                return returnMap;
            }
        }

        first = pieceList[0][2];
        second = pieceList[1][1];
        third = pieceList[2][0];

        //우좌상하 대각선 빈값 체크
        if (!containEmpty(first, second, third))
        {
            Debug.Log("CROSS02 : 배열이 값이 있습니다.");
            //우좌상하 대각선 같은값 체크
            if (isSameValue(first, second, third))
            {
                Debug.Log("CROSS02 : 배열이 같은 값으로 이루어져 있습니다.");
                rtrn = true;     
                checkedLine = Constants.CROSS_02;     
                returnMap.Add("isCheck", rtrn);
                returnMap.Add("checkedLine", checkedLine);
                
                return returnMap;
            }
        }

        returnMap.Add("isCheck", rtrn);
        returnMap.Add("checkedLine", "");

        return returnMap;
    }

    public void judgeWinner(String lineIdx){
        
        int winId = -1;
        int lineNum = 0;

        if(lineIdx == Constants.HOR_01 ){
            lineNum = 0;
        }else if(lineIdx == Constants.HOR_02){
            lineNum = 1;
        }else if(lineIdx == Constants.HOR_03){
            lineNum = 2;
        }else if(lineIdx == Constants.VER_01){
            lineNum = 0;
        }else if(lineIdx == Constants.VER_02){
            lineNum = 1;
        }else if(lineIdx == Constants.VER_03){
            lineNum = 2;
        }

        Debug.Log($"Constants : {Constants.HOR_02}");
        Debug.Log($"line : {lineIdx} / lineNum : {lineNum}");

        int firstVal = 0;
        int secondVal = 0;
        int thirdVal = 0;

        if(lineIdx != Constants.CROSS_01 && lineIdx != Constants.CROSS_02){
            if(lineIdx.Contains("HOR")){
                firstVal = pieceList[lineNum][0];
                secondVal = pieceList[lineNum][1];
                thirdVal = pieceList[lineNum][2];
            }else{
                firstVal = pieceList[0][lineNum];
                secondVal = pieceList[1][lineNum];
                thirdVal = pieceList[2][lineNum];
            }
        }else if( lineIdx == Constants.CROSS_01){
            firstVal = pieceList[0][0];
            secondVal = pieceList[1][1];
            thirdVal = pieceList[2][2];
        }else if ( lineIdx == Constants.CROSS_02){
            firstVal = pieceList[0][2];
            secondVal = pieceList[1][1];
            thirdVal = pieceList[2][0];
        }

        Debug.Log($"firstVal : {firstVal}");
        Debug.Log($"secondVal : {secondVal}");
        Debug.Log($"thirdVal : {thirdVal}");

        int[] numAry = new int[]{firstVal, secondVal, thirdVal};
        List<int> tmpList = null;

        for( int i = 0 ; i < 2 ; i++){
            tmpList = new List<int>();
            for(int j = 0 ; j < 3 ; j++){
                if(i != numAry[j]){
                    tmpList.Add(numAry[j]);
                }
            }
        }

        if(tmpList.Count == 3 ) {
		    winId = tmpList[0];
		    Debug.Log(" 승리!!!");
        }else{
		// 세 칸의 값이 다른 값이 들어왔다 ===> 앞의 라인 탐색 로직이 틀렸다는 뜻, 로직 재점검
        }

        /*
        int count = 0;
        for( int j = 0 ; j < 2 ; j++){
            for(int i = 0 ; i < 3 ; i++){
                if( i == 0 ){
                    count = 0;  
                } 

                if( j == numAry[i]){
                    count++;
                    Debug.Log($"count : {count}");
                }

                if(count == 3){
                    winId = j;
                }        
            }
        }
        */
        Debug.Log($"winId : {winId}");

        Enum curTurn = main.getTurn();
        Enum opstTurn = main.getOppositTurn(curTurn);

        int curTurnVal = Convert.ToInt32(curTurn);
        int opstTurnVal = Convert.ToInt32(opstTurn);
        Debug.Log($"curTurnVal : {curTurnVal}");
        Debug.Log($"opstTurnVal : {opstTurnVal}");

        String popupMsg = "";

        if(winId == (int)main.Turn.User){
            Debug.Log("Player가 승리하였습니다.");
            popupMsg = "PLAYER가 승리하였습니다.";
        }else if (winId == (int)main.Turn.Cpu){
            Debug.Log("Cpu가 승리하였습니다.");
            popupMsg = "CPU가 승리하였습니다.";
        }
        /*
        if(winId == curTurnVal){
            Debug.Log("Player가 승리하였습니다.");
            popupMsg = "PLAYER가 승리하였습니다.";

        }else if(winId == opstTurnVal ){
            Debug.Log("Cpu가 승리하였습니다.");
            popupMsg = "CPU가 승리하였습니다.";
        }
        */

        StartCoroutine(popupOpen(popupMsg));
    }

    public void cpuAction(){

        getEmptySpaceList();

        int len = emptySpaceList.Count;

        Debug.Log("emptySpaceList len : " + len);
        int idx = Range(0, len);
        Debug.Log("random idx : " + idx);

        string tmpStr = emptySpaceList[idx];
        string[] tAry = tmpStr.Split('_');
        int xIdx = Int32.Parse(tAry[0]);
        int yIdx = Int32.Parse(tAry[1]);

        pieceList[xIdx][yIdx] = (int)main.Turn.Cpu;

        Enum turn = main.getTurn();
        turn = main.Turn.User;
        main.setTurn(turn);

        string btnName = "/Canvas/ingamePanel/btn_" + tmpStr;
        GameObject obj = GameObject.Find(btnName);
        main.setButtonText(obj,(int)main.Turn.Cpu );
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

    public IEnumerator popupOpen(String popupMsg){
        
        Debug.Log("1초뒤 팝업 오픈");
        yield return new WaitForSeconds(1.5f);
        
        GameObject popup = main.popup;
        Text tvPopup = popup.transform.Find("tvPopup").GetComponent<Text>();

        tvPopup.text = popupMsg;

        popup.SetActive(true);
    }

    bool containEmpty(int first, int second, int third)
    {
        int[] tmpList = { first, second, third };

        return (-1 == Array.IndexOf(tmpList, -1)) ? false : true;
    }

    List<String> getEmptySpaceList(){

        emptySpaceList = new List<String>();

        for( int i = 0 ; i < pieceList.Count ; i++){
            for( int j = 0 ; j < pieceList[i].Count ; j++){
                int val = pieceList[i][j];

                Debug.Log("val : " + val);

                if(val == -1){
                    String tmpStr= i + "_" + j;
                    emptySpaceList.Add(tmpStr);  
                }
            }
        }

        return emptySpaceList;
    }

    public void turnChange(object[] param){
        
        Debug.Log("turnChange!!");

        List<List<int>> _pieceList = param[0] as List<List<int>>;
        Enum _enum = param[1] as Enum;

        turnAry.Add(_enum);

        Dictionary<String, object> checkedDic = checkMatch(_pieceList);

        if(_enum == (Enum)main.Turn.User){
            
        }else{

        }
    }
}
