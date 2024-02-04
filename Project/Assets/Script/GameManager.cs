using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public static int playerNum;
    [Space]
    public bool 回合开始;
    [HideInInspector]public bool resetSign;//给棋子重置
    GameObject area1, area2, land1, land2;
    [Space]
    public int chessNum;//在场棋子数量
    [Space]
    public bool[] isReady;//玩家准备完成
    [Space]
    [Header("单个回合战斗时间上限")]
    [SerializeField] private float roundTime;
    public float readyTime;//回合间准备时间
    private float timerForRound, timerForReady;//开始后的计时器与回合间的计时器


    [Space]
    [Header("双方控制台")]
    public GameObject[] chooseCardPanel;
    private void Awake()
    {
        instance = this;
        playerNum = 0;
        playerNum = PhotonNetwork.CountOfPlayers;

    }
    // Start is called before the first frame update
    void Start()
    {
        playerNum = PhotonNetwork.CountOfPlayers;
        #region 初始化
        area1 = transform.Find("1").gameObject;
        area2 = transform.Find("2").gameObject;
        land1 = Resources.Load("Prefab/Other/LandA") as GameObject;
        land2 = Resources.Load("Prefab/Other/LandB") as GameObject;
        for (int i = 0; i < 100; i++)
        {
            GameObject curLand1 = Instantiate(land1, area1.transform);
            curLand1.name = "landA" + i.ToString();
            GameObject curLand2 = Instantiate(land2, area2.transform);
            curLand2.name = "landB" + i.ToString();

        }
        回合开始 = false;
        chessNum = 0;
        isReady[0] = false;
        isReady[1] = false;
        #endregion
        Launcher.launcher.gameManager = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        playerNum = PhotonNetwork.CountOfPlayers;
        FightManage();
    }
    #region 回合管理

    public void FightManage()
    {
        if (回合开始 == true && PhotonNetwork.CountOfPlayers == 2)
        {
            timerForRound += Time.deltaTime;
            if (timerForRound > roundTime)
            {
                回合开始 = false;
                timerForRound = 0;
                chooseCardPanel[0].GetComponent<ChooseCard>().UpdateCards();
                chooseCardPanel[1].GetComponent<ChooseCard>().UpdateCards();
            }
        }

        if (回合开始 == false && PhotonNetwork.CountOfPlayers == 2)
        {
            timerForReady += Time.deltaTime;
        }
        else
        {
            timerForReady = 0;
        }

        if (timerForReady>readyTime)
        {
            回合开始 = true;
            timerForReady = 0;
        }


    }

    public float StartTimer()
    {
        float startTimer = (roundTime - timerForRound) % 1;
        return startTimer;
    }
    public float ReadyTimer()
    {
        float readyTimer = (readyTime - timerForReady) % 1;
        return readyTimer;
    }
    //public void FightManage()
    //{
    //    if (回合开始 == false)
    //    {
    //        timerForRound = 0;
    //        if (isReady[0] == true && isReady[1] == true)
    //        {
    //            回合开始 = true;
    //        }
    //    }
    //    else if (回合开始 == true)
    //    {
    //        timerForRound += Time.deltaTime;
    //        resetSign = false;
            
    //        if (timerForRound >= roundTime)
    //        {
    //            回合开始 = false;
    //            isReady[0] = false;
    //            isReady[1] = false;
    //            chooseCardPanel[0].GetComponent<ChooseCard>().UpdateCards();
    //            chooseCardPanel[1].GetComponent<ChooseCard>().UpdateCards();
    //            resetSign = true;
    //        }
    //    }
    //}
    #endregion
}
