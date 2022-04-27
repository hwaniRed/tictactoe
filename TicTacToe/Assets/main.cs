using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class main : MonoBehaviour, IPointerClickHandler
{
    public GameManager gm;
    public GameObject popup;
    private List<List<int>> pieceList = null;
    private Turn turn;

    public Button btnMenu;
    public Button btnRestart;
    public Button btnClose;
    public Button btnBack;

    public enum Turn
    {
        User
        , Cpu
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("Canvas").AddComponent<GameManager>() as GameManager;
        //popup = GameObject.Find("popup").GetComponent<Panel>();

        btnMenu = GameObject.Find("btnMenu").GetComponent<Button>();
        btnRestart = GameObject.Find("btnRestart").GetComponent<Button>();
        btnClose = GameObject.Find("btnClose").GetComponent<Button>();
        btnMenu.onClick.AddListener(delegate{ this.onPopupBtnClick(this.btnMenu);});
        btnRestart.onClick.AddListener(delegate{ this.onPopupBtnClick(this.btnRestart);});
        btnClose.onClick.AddListener(delegate{ this.onPopupBtnClick(this.btnClose);});


        turn = Turn.User;
 
        arrayInitiate();

        if(popup != null){
            popup.SetActive(false);
        }
        //popup.setActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool isOkCheck(int xIdx, int yIdx)
    {
        bool isOk = false;
        if( xIdx == -1 || yIdx == -1){
            Debug.Log("xIdx : " + xIdx +"/ yIdx : " + yIdx);
            Debug.Log("Point Not Clicked!!!");
            return isOk;
        }

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
        for ( int i = 0; i < len; i++)
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
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        string objName = eventData.pointerCurrentRaycast.gameObject.name;

        int xIdx = -1;
        int yIdx = -1;

        if(objName == "btnBack"){
            popup.SetActive(true);
            Text popupTitle = GameObject.Find("tvPopup").GetComponent<Text>();
            popupTitle.text = "게임을 종료하고 메뉴로 돌아가시겠습니까?";
        }

        if (objName.Contains('_'))
        {
            xIdx = Int32.Parse(objName.Split('_')[1]);
            yIdx = Int32.Parse(objName.Split('_')[2]);
            //Debug.Log($"xIdx : {xIdx}/ yIdx : {yIdx}");
        }

        if (turn == Turn.User)
        {
            if(isOkCheck(xIdx, yIdx))
            {
                pieceList[xIdx][yIdx] = (int)Turn.User;
                //말 체크
                //pieceList = getDummyList();
                gm.checkMatch(pieceList);

                setButtonText(obj, (int)turn);
                turn = Turn.Cpu;

                //Cpu calcurate
                gm.cpuAction();
            }
        }
        else
        {
            Debug.Log("CPU 턴입니다.");

            return;
        }

    }

    public void setButtonText(GameObject button, int turn)
    {
        String btnStr = "";

        if (turn == (int)Turn.User)
        {
            btnStr = "O";
        }
        else
        {
            btnStr = "X";
        }

        Text btnTxt = button.GetComponentInChildren<Text>();

        btnTxt.enabled = true;
        btnTxt.text = btnStr;
        btnTxt.fontSize = 160;
    }



    public List<List<int>> getPieceList()
    {
        return this.pieceList;
    }

    public Enum getTurn(){
        return this.turn;
    }

    public void setTurn(Enum turn){
        this.turn = (Turn)turn;
    }

    public Enum getOppositTurn(Enum _turn){
        Turn returnVal;

        if((Turn)_turn == Turn.User){
            returnVal = Turn.Cpu;
        }else{
            returnVal = Turn.User;
        }
        return returnVal;
    }
    //dummy data 만들기
    public List<List<int>> getDummyList(){

            List<List<int>> dummyList = new List<List<int>>();

            List<int> line1 = new List<int>();
            List<int> line2 = new List<int>();
            List<int> line3 = new List<int>();

            line1.Add(1);
            line1.Add(0);
            line1.Add(0);

            line2.Add(1);
            line2.Add(0);
            line2.Add(1);

            line3.Add(0);
            line3.Add(0);
            line3.Add(0);

            dummyList.Add(line1);
            dummyList.Add(line2);
            dummyList.Add(line3);

            Debug.Log("dummyList size : " + dummyList.Count);

        return dummyList;

    }

    public void onPopupBtnClick(Button obj){
        Debug.Log("Popup button Click!!!");
        Debug.Log("object : " + obj.name);

        if(obj.name == "btnRestart"){
            SceneManager.LoadScene("IngameScene");
        }else if(obj.name == "btnMenu"){

        }else if(obj.name == "btnClose"){
            popup.SetActive(false);
        }
    }
}
