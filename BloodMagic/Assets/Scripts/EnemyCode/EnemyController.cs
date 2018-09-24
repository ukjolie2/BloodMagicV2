using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : EnemyClass
{

    private void Start()
    {
        moveSpeed = 3f;
        slide = false;
        hp = 10;
        collider = GetComponent<Collider2D>();

}
    void FixedUpdate()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        if (Vector3.Distance(transform.position, target.position) > 2.5f)
        {
            if(slide)
            {
                direction += direction * Time.deltaTime * 0.75f;
            }
            else
            {
                direction = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            }
            transform.Translate(direction);
        }
        else if(slide)
        {
            Debug.Log("Slipping");
            direction = new Vector3(moveSpeed * Time.deltaTime * 0.05f, 0, 0);
            transform.Translate(direction);
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Touch");
        if (collision.transform.CompareTag("Blood"))
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
