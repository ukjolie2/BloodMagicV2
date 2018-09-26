using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : EnemyClass
{
    public CloseRangeAttack closeRange;
    CreateAttack attacks;
    int slowDown = 0;
    public int limit = 0;
    private void Start()
    {
        moveSpeed = 3f;
        slide = false;
        hp = 10;
        attacks = gameObject.GetComponent<CreateAttack>();

}
    void FixedUpdate()
    {
        if (hp <= 0)
        {
            attacks.SpawnBlood();
            Destroy(gameObject);
        }
        if(target != null)
        {
            transform.LookAt(target.position);
            transform.Rotate(new Vector3(0, -90, 0), Space.Self);

            if (Vector3.Distance(transform.position, target.position) > 2.5f)
            {
                if (slide)
                {
                    direction += direction * Time.deltaTime * 0.75f;
                }
                else
                {
                    direction = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
                }
                transform.Translate(direction);
            }
            else if (slide)
            {
                direction = new Vector3(moveSpeed * Time.deltaTime * 0.05f, 0, 0);
                transform.Translate(direction);
                attacks.AttackCloseRange();
            }
            else
            {
                if(slowDown >= limit)
                {
                    attacks.AttackCloseRange();
                    slowDown = 0;
                }
                slowDown++;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Blood"))
        {
            slide = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Blood"))
        {
            slide = false;
        }
    }

}
