using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks//���Եõ�PUN����ķ���
{
    public GameObject gameManager, chooseCardPanel;
    private int createTimes;//���ڱ��GameManager���ɴ���
    public static Launcher launcher;
    private void Awake()
    {
        launcher = this.gameObject.GetComponent<Launcher>();
        createTimes = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();//����Settings,���ӷ�����
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnConnectedToMaster()//�Ƿ����ӵ���������
    {
        base.OnConnectedToMaster();
        //GameManager.playerNum++;
        PhotonNetwork.JoinOrCreateRoom("GameRoom", new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, default);//����з��䣬����룻���û�У��򴴽�    RoomOptionsΪ�����������

    }
    public override void OnJoinedRoom()//���뷿���
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
