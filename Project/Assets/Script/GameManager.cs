using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public static int playerNum;
    [Space]
    public bool �غϿ�ʼ;
    [HideInInspector]public bool resetSign;//����������
    GameObject area1, area2, land1, land2;
    [Space]
    public int chessNum;//�ڳ���������
    [Space]
    public bool[] isReady;//���׼�����
    [Space]
    [Header("�����غ�ս��ʱ������")]
    [SerializeField] private float roundTime;
    public float readyTime;//�غϼ�׼��ʱ��
    private float timerForRound, timerForReady;//��ʼ��ļ�ʱ����غϼ�ļ�ʱ��


    [Space]
    [Header("˫������̨")]
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
        #region ��ʼ��
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
        �غϿ�ʼ = false;
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
    #region �غϹ���

    public void FightManage()
    {
        if (�غϿ�ʼ == true && PhotonNetwork.CountOfPlayers == 2)
        {
            timerForRound += Time.deltaTime;
            if (timerForRound > roundTime)
            {
                �غϿ�ʼ = false;
                timerForRound = 0;
                chooseCardPanel[0].GetComponent<ChooseCard>().UpdateCards();
                chooseCardPanel[1].GetComponent<ChooseCard>().UpdateCards();
            }
        }

        if (�غϿ�ʼ == false && PhotonNetwork.CountOfPlayers == 2)
        {
            timerForReady += Time.deltaTime;
        }
        else
        {
            timerForReady = 0;
        }

        if (timerForReady>readyTime)
        {
            �غϿ�ʼ = true;
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
    //    if (�غϿ�ʼ == false)
    //    {
    //        timerForRound = 0;
    //        if (isReady[0] == true && isReady[1] == true)
    //        {
    //            �غϿ�ʼ = true;
    //        }
    //    }
    //    else if (�غϿ�ʼ == true)
    //    {
    //        timerForRound += Time.deltaTime;
    //        resetSign = false;
            
    //        if (timerForRound >= roundTime)
    //        {
    //            �غϿ�ʼ = false;
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
