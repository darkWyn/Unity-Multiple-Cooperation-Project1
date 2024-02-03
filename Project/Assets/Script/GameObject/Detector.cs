using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public GameObject parent;
    [SerializeField]private float speed;//寻敌速度
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
        if (GameManager.instance.回合开始 == true)
        {
            transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * speed, transform.localScale.y + Time.deltaTime * speed, transform.localScale.z);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NormalObject>() == true&&GameManager.instance.回合开始==true)
        {
            if (collision.GetComponent<NormalObject>().阵营 != parent.GetComponent<NormalObject>().阵营&&collision.GetComponent<NormalObject>().curHealth>0)
            {
                parent.GetComponent<NormalObject>().target = collision.gameObject;
                transform.localScale = Vector3.zero;
            }
        }
    }
}
