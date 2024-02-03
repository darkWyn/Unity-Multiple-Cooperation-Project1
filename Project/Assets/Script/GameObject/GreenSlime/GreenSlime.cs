using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : NormalObject
{
    private float Speed;

    float timer;

    [SerializeField]List<GameObject> enemies = new List<GameObject>();
    protected override void Start()
    {
        base.Start();
        Speed = speed;
        timer = 0;
    }
    protected override void Update()
    {
        base.Update();
        if (GameManager.instance.�غϿ�ʼ)
        {
            animator.SetBool("IsMoving", true);
        }
        else
        {
            animator.SetBool("IsMoving", false);
            Stop();
        }
        timer += Time.deltaTime;
        if (enemies.Count > 0)
        {
            if (timer > interval)
            {
                Damage(enemies[0], damage, 0);
                timer = 0;
            }
            
        }
    }
    public void Stop()
    {
        speed = 0;
    }
    public void MoveUp()
    {
        speed = Speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<NormalObject>() && collision.GetComponent<NormalObject>().��Ӫ != ��Ӫ)
        {
            enemies.Add(collision.gameObject);
            Debug.Log(collision.gameObject.name+"���");
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NormalObject>() && collision.GetComponent<NormalObject>().��Ӫ != ��Ӫ)
        {
            enemies.Remove(collision.gameObject);
            Debug.Log(collision.gameObject.name + "�Ƴ�");
        }

    }
}
