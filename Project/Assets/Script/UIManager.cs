using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UIManager : MonoBehaviourPunCallbacks
{
    public static UIManager instance;
    private GameObject settings;
    private GameObject playerNum;

    private void Awake()
    {
        instance = this;
        settings = transform.Find("Settings").gameObject;
        GameManager.playerNum = 0;
        playerNum = transform.Find("PlayerNum").gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        settings.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        playerNum.GetComponent<Text>().text = "在线玩家数:" + GameManager.playerNum.ToString();
        Debug.Log("NetWorkPlayers:" + PhotonNetwork.CountOfPlayers.ToString() + "GameManagerPlayers:" + GameManager.playerNum.ToString());
        UpdateTime();
    }

    public void StartButton()
    {
        SceneManager.LoadScene("GamingArea");
    }
    #region 设置栏
    public void ExitButton()//退出游戏
    {
        Application.Quit();
        //GameManager.playerNum--;
    }
    public void EditorExit()//Unity内的退出方式
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }


    public void ContinueButton()//继续游戏
    {
        Time.timeScale = 1;
        settings.SetActive(false);
    }
    public void ActiveSettings()
    {
        Time.timeScale = 0;
        settings.SetActive(true);
    }
    #endregion
    #region 时间更新
    public void UpdateTime()
    {
        Text startTimer = transform.Find("Timer").Find("StartTimer").Find("timer").GetComponent<Text>();
        Text readyTimer = transform.Find("Timer").Find("ReadyTimer").Find("timer").GetComponent<Text>();
        startTimer.text = "距离回合结束还有" + GameManager.instance.StartTimer().ToString() + "秒";
        readyTimer.text = "距离回合开始还有" + GameManager.instance.ReadyTimer().ToString() + "秒";
        if (GameManager.instance.回合开始 == true)
        {
            startTimer.gameObject.SetActive(true);
            readyTimer.gameObject.SetActive(false);
        }
        else if (GameManager.instance.回合开始==false)
        {
            startTimer.gameObject.SetActive(false);
            readyTimer.gameObject.SetActive(true);
        }
        else
        {
            startTimer.gameObject.SetActive(false);
            readyTimer.gameObject.SetActive(false);
        }
    }
    #endregion
}
