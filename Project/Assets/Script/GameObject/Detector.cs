using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject parent;
    [SerializeField]private float speed;//Ѱ���ٶ�
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
    }
    public void FindTarget()
    {
        if (GameManager.instance.�غϿ�ʼ == true)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * speed, transform.localScale.y + Time.deltaTime * speed, transform.localScale.z);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NormalObject>() == true&&GameManager.instance.�غϿ�ʼ==true)
        {
            if (collision.GetComponent<NormalObject>().��Ӫ != parent.GetComponent<NormalObject>().��Ӫ&&collision.GetComponent<NormalObject>().curHealth>0)
            {
                parent.GetComponent<NormalObject>().target = collision.gameObject;
                transform.localScale = Vector3.zero;
            }
        }
    }
}
