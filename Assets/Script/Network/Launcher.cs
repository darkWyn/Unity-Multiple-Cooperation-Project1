using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Launcher : MonoBehaviourPunCallbacks//���Եõ�PUN����ķ���
{
    public GameObject chooseCardPanel;
    private void Awake()
    {
        
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
        //GameObject curCanvas = Instantiate(chooseCardPanel);
        GameObject curCanvas = PhotonNetwork.Instantiate("Prefab/UI/ChoosingCard", new Vector3(0, 0, 0), Quaternion.identity, 0);
        chooseCardPanel = curCanvas.transform.Find("ChooseCardPanel").gameObject;
        
    }
}
