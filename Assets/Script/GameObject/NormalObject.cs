using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class NormalObject : MonoBehaviourPun
{
    [Header("��������")]
    public float maxHealth;
    public float curHealth;
    public float damage,speed;//�˺�
    public int ��Ӫ;
    public float physicalResistance;//�����ԡ�0
    public float magicalResistance;//ħ�����ԡ�1
    public float attackDistance;//��������
    public int cardNum;//��Ƭ���
    [Space]
    protected GameObject detector;
    [Header("Ŀ��")]
    public GameObject target;//Ŀ��

    [Header("�������")]
    public float interval;

    protected Animator animator;
    protected BoxCollider2D boxCollider2D;

    protected bool isAlive;

    public bool hasSet;//�Ƿ񱻷���
    // Start is called before the first frame update
    protected virtual void Start()
    {
        #region ��ʼ��
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
    #region �ƶ�
    public virtual void Move()
    {
        if (GameManager.instance.�غϿ�ʼ == true&&target!=null)
        {
            Vector3 deltaDirection = (target.transform.position - transform.position).normalized;
            transform.position += deltaDirection * speed * Time.deltaTime;
            

        }
        else if(GameManager.instance.�غϿ�ʼ == true && target == null)
        {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }

    }
    #endregion
    #region ��������
    public void HealthManage()
    {
        if (GameManager.instance.�غϿ�ʼ == true)
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
        else if (GameManager.instance.�غϿ�ʼ == false)
        {
            isAlive = false;
            curHealth = maxHealth;
            gameObject.SetActive(true);
            boxCollider2D.enabled = true;
            transform.Find("Canvas").gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;

            if (GameManager.instance.�غϿ�ʼ == false && hasSet == true)
            {
                transform.localPosition = Vector3.zero;
            }

        }
    }
    public void RoundManage()//����غϿ�ʼ�ͽ���ʱ�Ĳ���
    {
        if (GameManager.instance.�غϿ�ʼ == false&&hasSet==true)
        {
            curHealth = maxHealth;
            gameObject.SetActive(true);
            transform.localPosition = Vector3.zero;
            
        }
    }
    #endregion
    #region ��ײ����
    public virtual void ColliderManage()
    {
        if (GameManager.instance.�غϿ�ʼ == false)
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
            if (damageType == 0)//�����˺�
            {
                target.GetComponent<NormalObject>().curHealth -= (1 - target.GetComponent<NormalObject>().physicalResistance) * damage;
            }
            else if (damageType == 1)//ħ���˺�
            {
                target.GetComponent<NormalObject>().curHealth -= (1 - target.GetComponent<NormalObject>().magicalResistance) * damage;
            }
            else if (damageType == 2)//��ʵ�˺�
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
        if (��Ӫ == 1)
        {
            health1.GetComponent<Image>().fillAmount = curHealth / maxHealth;
            health1.SetActive(true);
            health2.SetActive(false);
        }else if (��Ӫ == 2)
        {
            health2.GetComponent<Image>().fillAmount = curHealth / maxHealth;
            health2.SetActive(true);
            health1.SetActive(false);
        }
    }
    #endregion
}
