using System;
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
                String lineIdx = "HOR0" + (i+1);     
                judgeWinner(lineIdx);
                break;
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
                String lineIdx = "VER0" + (j+1);     
                judgeWinner(lineIdx);
                break;
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
                judgeWinner(Constants.CROSS_01);
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
                judgeWinner(Constants.CROSS_02);
            }
        }

        return rtrn;
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

        Debug.Log($"winId : {winId}");

        Enum curTurn = main.getTurn();
        Enum opstTurn = main.getOppositTurn(curTurn);

        int curTurnVal = Convert.ToInt32(curTurn);
        int opstTurnVal = Convert.ToInt32(opstTurn);
        Debug.Log($"curTurnVal : {curTurnVal}");
        Debug.Log($"opstTurnVal : {opstTurnVal}");

        String popupMsg = "";

        if(winId == curTurnVal){
            Debug.Log("Player가 승리하였습니다.");
            popupMsg = "PLAYER가 승리하였습니다.";

        }else if(winId == opstTurnVal ){
            Debug.Log("Cpu가 승리하였습니다.");
            popupMsg = "CPU가 승리하였습니다.";
        }

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

    IEnumerator popupOpen(String popupMsg){
        
        Debug.Log("1초뒤 파업 오픈");
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
}
