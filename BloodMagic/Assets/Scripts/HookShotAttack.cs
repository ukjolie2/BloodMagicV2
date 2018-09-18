using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotAttack : MonoBehaviour {

    public LineRenderer lineRenderer;
    public float hookTimeLimit = 1.0f;
    public float pullSpeed = 25.0f;

    private Rigidbody2D hook;

    private float timeLeft;
    private int hookCount = 0;

	void Update ()
    {
		if(Input.GetMouseButtonDown(1) && hookCount == 0)
        {
            shootHook();
        }
	}

    void LateUpdate()
    {
        if (hook != null && timeLeft > 0f)
        {
            //lets player hook on for hook time limit
            timeLeft -= Time.deltaTime;

            //draw hook line
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hook.transform.position);

            pullHook();
        }
        else
        {
            //reset hook
            lineRenderer.enabled = false;
            hook = null;
            hookCount = 0;
        }
    }

    void shootHook()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = transform.position;
        Vector2 direction = mousePosition - position;

        //gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        //cast ray in direction of mouse
        RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);
       // gameObject.GetComponent<PolygonCollider2D>().enabled = true;

        //if ray hits enemy collider, set up hook
        if(hit.collider != null)
        {
            if (hit.rigidbody.gameObject.tag == "Enemy")
            {
                Debug.Log(hit.collider.name);
                hook = hit.rigidbody;
                hook.transform.position = hit.transform.position;
                timeLeft = hookTimeLimit;
                hookCount = 1;
            }
        }
    }

    void pullHook()
    {

        //if moving away from enemy, pull enemy to player
        hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.position, pullSpeed * Time.deltaTime);
        //if moving toward enemy, pull to enemy
        //transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, pullSpeed * Time.deltaTime);
    }
}
