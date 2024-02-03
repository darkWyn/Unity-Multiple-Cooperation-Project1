using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Card : MonoBehaviourPun, IPointerClickHandler
{
    public GameObject plantPrefab;//棋子的预制件
    [Header("对应卡片编号")]
    public int cardNum;
    [Space]
    private GameObject curPlant;//克隆出来的实际棋子物体
    public int cost;
    private GameObject darkBg;//黑色背景

    public static int playerOrder;

    private GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.parent.parent.parent.parent.gameObject;
        Debug.Log(canvas.name);
        darkBg = transform.Find("darkBg").gameObject;
        playerOrder = canvas.GetComponent<ChooseCard>().阵营;
        cardNum = plantPrefab.GetComponent<NormalObject>().cardNum;
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.GetComponent<ChooseCard>().sunNum < cost)
        {
            darkBg.SetActive(true);
        }
        else
        {
            darkBg.SetActive(false);
        }
    }
    #region 拖拽部分脚本

    public void OnBeginDrag(BaseEventData data)//开始拖拽
    {

        if (darkBg.activeSelf == true)//如果压黑背景显示，则无法拖拽
        {
            return;
        }
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == false)
            return;
        if (GameManager.instance.isReady[playerOrder - 1] == true)
        {
            return;
        }
        PointerEventData pointerEventData = data as PointerEventData;
        //curPlant = Instantiate(plantPrefab);
        curPlant = PhotonNetwork.Instantiate("Prefab/GameObjects/" +plantPrefab.name.ToString(), gameObject.transform.position, Quaternion.identity, 0);
        curPlant.transform.position = TraslateScreenToWorld(pointerEventData.position);
        curPlant.GetComponent<NormalObject>().hasSet = false;
    }
    public void OnDrag(BaseEventData data)//正在拖拽
    {
        if (curPlant == null)
        {
            return;
        }
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == false)
            return;

        PointerEventData pointerEventData = data as PointerEventData;
        curPlant.transform.position = TraslateScreenToWorld(pointerEventData.position);

    }
    public void OnEndDrag(BaseEventData data)//结束拖拽
    {
        if (curPlant == null)
        {
            return;
        }
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == false)
            return;

        PointerEventData pointerEventData = data as PointerEventData;
        Collider2D[] coll = Physics2D.OverlapPointAll(TraslateScreenToWorld(pointerEventData.position));
        foreach (Collider2D c in coll)
        {
            if (c.tag == "Land"+playerOrder.ToString())
            {
                if (c.transform.childCount == 0)
                {
                    curPlant.GetComponent<NormalObject>().阵营 = playerOrder;
                    curPlant.transform.position = c.transform.position;
                    curPlant.transform.parent = c.transform;
                    curPlant.transform.localPosition = Vector3.zero;
                    curPlant.GetComponent<NormalObject>().hasSet = true;
                    //curPlant.GetComponent<Plant>().SetPlantStart();
                    canvas.GetComponent<ChooseCard>().sunNum -= cost;
                    GameManager.instance.chessNum++;
                    curPlant = null;

                    Destroy(gameObject);
                    //Destroy(gameObject);
                }
                break;
            }

        }
        if (curPlant != null)
        {
            GameObject.Destroy(curPlant);
            curPlant = null;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    #endregion
    #region 屏幕坐标系转换为世界坐标系
    public static Vector3 TraslateScreenToWorld(Vector3 position)
    {
        Vector3 cameraTranslatePos = Camera.main.ScreenToWorldPoint(position);
        return new Vector3(cameraTranslatePos.x, cameraTranslatePos.y, 0);
    }

    #endregion    


}
