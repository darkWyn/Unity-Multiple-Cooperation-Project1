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
        playerNum.GetComponent<Text>().text = "���������:" + GameManager.playerNum.ToString();
        Debug.Log("NetWorkPlayers:" + PhotonNetwork.CountOfPlayers.ToString() + "GameManagerPlayers:" + GameManager.playerNum.ToString());
    }

    public void StartButton()
    {
        SceneManager.LoadScene("GamingArea");
    }
    #region ������
    public void ExitButton()//�˳���Ϸ
    {
        Application.Quit();
        //GameManager.playerNum--;
    }
    public void EditorExit()//Unity�ڵ��˳���ʽ
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

#endif
    }


    public void ContinueButton()//������Ϸ
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
}
