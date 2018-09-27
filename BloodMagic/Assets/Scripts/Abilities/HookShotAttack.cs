using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShotAttack : SkillClass {

    public LineRenderer lineRenderer;
    public float hookTimeLimit = .5f;
    public float pullSpeed = 25.0f;
    public float maxLength = 10f;
    public float endDistance = 1.4f;
    //public GameObject hookSpriteGO; 

    private Rigidbody2D hook;
    private PlayerController playerController;

    private float timeLeft;
    private int hookCount = 0;
    private int hookLength;
    private Vector2 originalHookPos;
    private Vector2 freezePosition;
    private float prevDist;
    private int frameNum = 0;
    private bool isEnemy;
    private Vector2 hitPoint;
    private Vector3 direction;
    private GameObject hookSprite;
    private float distance;
    private bool healed = false;

    public void Start()
    {
        HpCost = 1; //must be multiplied by the hook length
        HpReturn = 1;
        Power = 1;
        playerController = transform.GetComponent<PlayerController>();
    }

    void Update ()
    {
        if (Input.GetButtonDown("Jump") && hookCount == 0)
        {
            UseAbility();
            freezePosition = transform.position;
        }
    }

    void LateUpdate()
    {
        if (hook != null && timeLeft > 0f)
        {
            //lets player hook on for hook time limit
            timeLeft -= Time.deltaTime;

            //hookSprite.transform.position = transform.position;
            //float scaleX = Mathf.Abs(transform.position.x - hitPoint.x);
            //hookSprite.transform.localScale = new Vector3(scaleX / 4, 1, 1);

            
            //draw hook line
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            if (isEnemy)
                hitPoint = hook.transform.position;

            lineRenderer.SetPosition(1, hitPoint);
            //hookSprite.transform.Rotate(hitPoint - (Vector2)hookSprite.transform.position);

            if (frameNum == 1)
                pullHook();
            else
                frameNum++;
        }
        else if (timeLeft <= 0f && healed == false)
        {
            //reset hook
            lineRenderer.enabled = false;
            hook = null;
            hookCount = 0;
            frameNum = 0;
            isEnemy = false;
            //recover hp
            playerController.hp += HpReturn * hookLength;
            healed = true;
        }
    }

    public override void UseAbility()
    {
        healed = false;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = transform.position;
        direction = mousePosition - position;

        gameObject.GetComponent<Collider2D>().enabled = false;
        //cast ray in direction of mouse
        //RaycastHit2D hit = Physics2D.Raycast(position, direction, Mathf.Infinity);

        RaycastHit2D[] hits = Physics2D.RaycastAll(position, direction, Mathf.Infinity);

        gameObject.GetComponent<Collider2D>().enabled = true;

        
        //if ray hits enemy collider, set up hook
        if(hits[0].collider != null)
        {
            int hookIndex = 0;
            while(hits[hookIndex].transform.gameObject.tag == "Pit")
            {
                hookIndex++;
            }
            if (hits[hookIndex].transform.gameObject.tag == "Enemy" || hits[hookIndex].transform.gameObject.tag == "Walls")
            {
                distance = Vector2.Distance(hits[hookIndex].point, transform.position);
                if (distance <= maxLength)
                {
                    //createHookSprite();
                    //Destroy(hookSprite, hookTimeLimit);
                    hook = hits[hookIndex].rigidbody;
                    if (hits[hookIndex].rigidbody.gameObject.tag == "Enemy")
                    {
                        isEnemy = true;
                        hitPoint = hook.transform.position;
                    }
                    else
                    {
                        hitPoint = hits[hookIndex].point;
                    }
                    originalHookPos = hook.transform.position;
                    prevDist = Vector2.Distance(originalHookPos, transform.position);
                    timeLeft = hookTimeLimit;
                    hookCount = 1;

                    //decrease hp of player
                    if (playerController != null)
                    {
                        hookLength = (int)Vector3.Distance(transform.position, hook.transform.position);
                        playerController.hp -= HpCost * hookLength;
                    }
                }
                else
                {
                    hook = null;
                    distance = maxLength;
                }
            }
        }
    }

    void pullHook()
    {
        if (isEnemy)
        {//if moving away from enemy, pull enemy to player
            float currDist = Vector2.Distance(originalHookPos, transform.position);
            if (currDist > prevDist)
            {
                transform.position = freezePosition; //stop player movement
                hook.transform.position = Vector3.MoveTowards(hook.transform.position, transform.position + (direction.normalized * endDistance), pullSpeed * 1.3f * Time.deltaTime);
            }
            //if moving toward enemy, pull player to enemy
            else
            {
                Vector3 directionEnemy = transform.position - hook.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position + (directionEnemy.normalized * endDistance), pullSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, hitPoint, pullSpeed * Time.deltaTime);
        }
    }

    void createHookSprite()
    {
        /*hookSprite = Instantiate(hookSpriteGO, gameObject.transform.position, gameObject.transform.rotation);
        hookSprite.transform.Rotate(0, 0, 90);
        hookSprite.transform.position = transform.position;
        hookSprite.transform.localScale = new Vector3(0, 1, 1);*/
    }
}
