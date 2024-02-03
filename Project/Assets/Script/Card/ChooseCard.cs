using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class ChooseCard : MonoBehaviourPunCallbacks
{
    public int sunNum;
    private Text sunNumText;
    public int 阵营;
    [SerializeField]private GameObject[] cards;//所有卡片
    private int cloneTimes;
    // Start is called before the first frame update
    void Start()
    {
        阵营 = GameManager.playerNum;
        sunNum = 5;
        sunNumText = transform.Find("ChooseCardPanel").transform.Find("SunNum").GetComponent<Text>();
        cloneTimes = 0;
        GameManager.instance.chooseCardPanel[阵营 - 1] = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
        if (GameManager.instance.回合开始 == false&&cloneTimes==0)
        {
            Invoke("UpdateCards",0);
            cloneTimes++;
        }
        if (GameManager.instance.回合开始 == true)
        {
            cloneTimes = 0;
        }
        if (photonView.IsMine == false)
        {
            gameObject.SetActive(false);
        }
    }
    public void UpdateUI()
    {
        sunNumText.text = sunNum.ToString();
        if (photonView.IsMine == true)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (GameManager.instance.回合开始 == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }

        #region 准备区
        GameObject isReady = transform.Find("ReadyButton").Find("IsReady").gameObject;
        GameObject notReady = transform.Find("ReadyButton").Find("NotReady").gameObject;

        if (GameManager.instance.isReady[阵营 - 1] == true)
        {
            isReady.SetActive(true);
            notReady.SetActive(false);
        }else if(GameManager.instance.isReady[阵营 - 1] == false)
        {
            isReady.SetActive(false);
            notReady.SetActive(true);
        }
        #endregion
    }

    public void UpdateCards()
    {
        for (int i = 0; i < 4; i++)
        {
            int randomNum = Random.Range(0, cards.Length);
            GameObject curCard = Instantiate(cards[randomNum], transform.Find("ChooseCardPanel").Find("Bg").Find("Cards"));
            curCard.name = "Card" + i.ToString();
        }
    }
    public void ReadyBtn()
    {
        if (GameManager.instance.isReady[阵营 - 1] == false&&photonView.IsMine==true)
        {
            GameManager.instance.isReady[阵营 - 1] = true;
        }
        else if (GameManager.instance.isReady[阵营 - 1] == true && photonView.IsMine == true)
        {
            GameManager.instance.isReady[阵营 - 1] = false;
        }
    }
}
