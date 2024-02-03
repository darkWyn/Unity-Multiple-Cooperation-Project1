using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NormalObject : MonoBehaviourPun
{
    [Header("基础属性")]
    public float maxHealth;
    public float curHealth;
    public float damage,speed;//伤害
    public int 阵营;
    public float physicalResistance;//物理抗性→0
    public float magicalResistance;//魔法抗性→1
    public float attackDistance;//攻击距离
    public int cardNum;//卡片编号
    [Space]
    protected GameObject detector;
    [Header("目标")]
    public GameObject target;//目标

    [Header("攻击间隔")]
    public float interval;

    protected Animator animator;
    protected BoxCollider2D boxCollider2D;

    protected bool isAlive;

    public bool hasSet;//是否被放置
    // Start is called before the first frame update
    protected virtual void Start()
    {
        #region 初始化
        animator = GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        boxCollider2D.enabled = false;
        detector = transform.Find("detector").gameObject;
        curHealth = maxHealth;
        isAlive = false;
        
        #endregion
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        HealthManage();
        RoundManage();
        ColliderManage();
        UpdateUI();
    }
    #region 移动
    public virtual void Move()
    {
        if (GameManager.instance.回合开始 == true&&target!=null)
        {
            Vector3 deltaDirection = (target.transform.position - transform.position).normalized;
            transform.position += deltaDirection * speed * Time.deltaTime;
            

        }
        else if(GameManager.instance.回合开始 == true && target == null)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }

    }
    #endregion
    #region 生命周期
    public void HealthManage()
    {
        if (GameManager.instance.回合开始 == true)
        {
            if (curHealth > 0)
            {
                Move();
                gameObject.SetActive(true);
                boxCollider2D.enabled = true;
                transform.Find("Canvas").gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                isAlive = true;
            }
            if (curHealth <= 0)
            {
                isAlive = false;
                boxCollider2D.enabled = false;
                transform.Find("Canvas").gameObject.SetActive(false);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.localPosition = Vector3.zero;
            }

        }
        else if (GameManager.instance.回合开始 == false)
        {
            isAlive = false;
            curHealth = maxHealth;
            gameObject.SetActive(true);
            boxCollider2D.enabled = true;
            transform.Find("Canvas").gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

            if (GameManager.instance.回合开始 == false && hasSet == true)
            {
                transform.localPosition = Vector3.zero;
            }

        }
    }
    public void RoundManage()//处理回合开始和结束时的操作
    {
        if (GameManager.instance.回合开始 == false&&hasSet==true)
        {
            curHealth = maxHealth;
            gameObject.SetActive(true);
            transform.localPosition = Vector3.zero;
            
        }
    }
    #endregion
    #region 碰撞控制
    public virtual void ColliderManage()
    {
        if (GameManager.instance.回合开始 == false)
        {
            boxCollider2D.enabled = false;
            target = null;
        }
        else
        {
            boxCollider2D.enabled = true;
            if (isAlive == true)
            {
                detector.SetActive(true);
            }
        }

        if (isAlive == false)
        {
            detector.SetActive(false);
        }
    }

    #endregion
    public virtual void Damage(GameObject target,float damage,int damageType)
    {
        if (target.GetComponent<NormalObject>())
        {
            if (damageType == 0)//物理伤害
            {
                target.GetComponent<NormalObject>().curHealth -= (1 - target.GetComponent<NormalObject>().physicalResistance) * damage;
            }
            else if (damageType == 1)//魔法伤害
            {
                target.GetComponent<NormalObject>().curHealth -= (1 - target.GetComponent<NormalObject>().magicalResistance) * damage;
            }
            else if (damageType == 2)//真实伤害
            {
                target.GetComponent<NormalObject>().curHealth -= damage;
            }
        }
    }
    #region UI
    public void UpdateUI()
    {
        GameObject health1, health2;
        health1 = transform.Find("Canvas").Find("HealthPanel").Find("health1").gameObject;
        health2 = transform.Find("Canvas").Find("HealthPanel").Find("health2").gameObject;
        if (阵营 == 1)
        {
            health1.GetComponent<Image>().fillAmount = curHealth / maxHealth;
            health1.SetActive(true);
            health2.SetActive(false);
        }else if (阵营 == 2)
        {
            health2.GetComponent<Image>().fillAmount = curHealth / maxHealth;
            health2.SetActive(true);
            health1.SetActive(false);
        }
    }
    #endregion
}
