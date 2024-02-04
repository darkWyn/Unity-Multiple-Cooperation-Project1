using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks//可以得到PUN里面的反馈
{
    public GameObject gameManager, chooseCardPanel;
    private int createTimes;//用于标记GameManager生成次数
    public static Launcher launcher;
    private void Awake()
    {
        launcher = this.gameObject.GetComponent<Launcher>();
        createTimes = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//启用Settings,连接服务器
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()//是否连接到主服务器
    {
        base.OnConnectedToMaster();
        //GameManager.playerNum++;
        PhotonNetwork.JoinOrCreateRoom("GameRoom", new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, default);//如果有房间，则加入；如果没有，则创建    RoomOptions为玩家数量上限

    }
    public override void OnJoinedRoom()//加入房间后
    {
        base.OnJoinedRoom();
        //CreateFightingArea();
        //GameObject curCanvas = Instantiate(chooseCardPanel);
        GameObject curCanvas = PhotonNetwork.Instantiate("Prefab/UI/ChoosingCard", new Vector3(0, 0, 0), Quaternion.identity, 0);
        chooseCardPanel = curCanvas.transform.Find("ChooseCardPanel").gameObject;
        
    }
    public void CreateFightingArea()
    {
        if (PhotonNetwork.CountOfPlayers == 1&&createTimes==0)
        {
            gameManager = PhotonNetwork.Instantiate("Prefab/UI/FightingArea", new Vector3(0, 0, 0), Quaternion.identity, 0);
            createTimes++;
        }
    }
}
