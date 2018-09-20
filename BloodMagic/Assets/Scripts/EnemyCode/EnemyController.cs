using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Status
    public bool slide = false;

    public Transform target;
    public float moveSpeed = 3f;
    public int hp = 10;
    //enemy will head this way
    Vector3 direction;
    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        if (Vector3.Distance(transform.position, target.position) > 1f)
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

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Touch");
        if (other.CompareTag("Blood"))
        {
            slide = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Blood"))
        {
            slide = false;
        }
    }
}
