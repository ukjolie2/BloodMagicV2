using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotAttack : SkillClass {

    public LineRenderer lineRenderer;
    public float hookTimeLimit = 1.0f;
    public float pullSpeed = 25.0f;

    private Rigidbody2D hook;
    private PlayerController playerController;

    private float timeLeft;
    private int hookCount = 0;
    private int hookLength;
    private Vector2 originalHookPos;
    private float prevDist;
    private int frameNum = 0;

    public void Start()
    {
        HpCost = 1; //must be multiplied by the hook length
        HpReturn = 1;
        Power = 1;
    }

    void Update ()
    {
		if(Input.GetMouseButtonDown(1) && hookCount == 0)
        {
            UseAbility();
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

            if (frameNum == 1)
                pullHook();
            else
                frameNum++;
        }
        else if (hook != null && timeLeft <= 0f)
        {
            //reset hook
            lineRenderer.enabled = false;
            hook = null;
            hookCount = 0;
            frameNum = 0;
            //recover hp
            playerController.hp += HpReturn * hookLength;
        }
    }

    public override void UseAbility()
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
                hook = hit.rigidbody;
                //hook.transform.position = hit.transform.position;
                originalHookPos = hook.transform.position;
                prevDist = Vector2.Distance(originalHookPos, transform.position);
                timeLeft = hookTimeLimit;
                hookCount = 1;

                //decrease hp of player
                playerController = transform.GetComponent<PlayerController>();
                if(playerController != null)
                {
                    hookLength = (int)Vector3.Distance(transform.position, hook.transform.position);
                    playerController.hp -= HpCost * hookLength;
                }
            }
        }
    }

    void pullHook()
    {
        //if moving away from enemy, pull enemy to player
        float currDist = Vector2.Distance(originalHookPos, transform.position);
        if(currDist > prevDist)
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.position, pullSpeed * Time.deltaTime);
        //if moving toward enemy, pull to enemy
        else
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, pullSpeed * Time.deltaTime);
    }
}
