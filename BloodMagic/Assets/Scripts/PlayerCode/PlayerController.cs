﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //temp
    bool cutscene = false;
    public Camera camera;
    public GameObject target;

    public int tester;

    //this class spawns the attacks the player uses
    CreateAttack attacks;
    HealthChange healthBar;

    //temporary spawnHolder
    //public GameObject spawner;
    EnemySpawn spawnPoint;

    //Player status
    public bool slide = false;
    private bool mobility = true;

    //stats
    public int hp = 100;

    //movement
    Vector3 move;
    public float moveSpeed = 1f;
    public float rotationSpeed = 5.0f;
    public bool usingController = false;

    //Attack Animations
    //melee
    private Animator meleeAnimator;
    private float meleeTime = .25f;
    private float meleeTimeCounter;
    private SpriteRenderer meleeSpriteRenderer;

	// Use this for initialization
	void Start ()
    {
        attacks = gameObject.GetComponent<CreateAttack>();
        healthBar = gameObject.GetComponent<HealthChange>();
        //spawnPoint = spawner.GetComponent<EnemySpawn>();
        move = new Vector3();

        //melee attack
        GameObject meleeGameObj = GameObject.Find("Swipe-Melee-Attack-Sprite");
        meleeAnimator = meleeGameObj.GetComponent<Animator>();
        meleeSpriteRenderer = meleeGameObj.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        //Death
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        Vector2 direction;
        float angle = 0f;
        Quaternion rotation;

        if (!cutscene)
        {
            //Attack
            if (Input.GetButtonDown("Fire1"))
            {
                attacks.AttackLongRange();
                mobility = false;
            }
            else if (Input.GetButtonDown("Fire2"))
            {
                meleeTimeCounter = meleeTime;
                meleeSpriteRenderer.enabled = true;
                meleeAnimator.SetBool("Swipe-Melee-Attacking", true);
                attacks.AttackCloseRange();
                mobility = false;
            }
            else if (Input.GetButtonDown("Fire3"))
            {
                attacks.AttackBookRange();
                mobility = false;
            }
            else
            {
                mobility = true;
            }

            if (usingController)
            {
                // Rotate character using right joystick
                direction = new Vector2(Input.GetAxis("RightJoystickHorizontal"), Input.GetAxis("RightJoystickVertical"));
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                if (angle == 0) { rotation = Quaternion.AngleAxis(0, Vector3.forward); }
                else { rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward); }
            }
            else
            {
                // Rotate character towards mouse
                direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
                angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

            if (mobility)
            {
                float xDir = Input.GetAxis("Horizontal");
                float yDir = Input.GetAxis("Vertical");
                if (xDir < 0)
                {
                    if (slide)
                    {
                        if (move.x > 0)
                        {
                            move.x += -moveSpeed * 0.002f;
                        }
                        else
                        {
                            move.x += -moveSpeed * 0.11f * tester;
                        }
                    }
                    else
                    {
                        move.x = -moveSpeed;
                    }
                }
                else if (xDir > 0)
                {
                    if (slide)
                    {
                        if (move.x < 0)
                        {
                            move.x += moveSpeed * 0.002f;
                        }
                        else
                        {
                            move.x += moveSpeed * 0.11f * tester;
                        }
                    }
                    else
                    {
                        move.x = moveSpeed;
                    }
                }
                else
                {
                    if (slide)
                    {
                        move.x -= move.x * 0.05f;
                    }
                    else
                    {
                        move.x = 0;
                    }
                }


                if (yDir < 0)
                {
                    if (slide)
                    {
                        if (move.y > 0)
                        {
                            move.y += -moveSpeed * 0.002f * tester;
                        }
                        else
                        {
                            move.y += -moveSpeed * 0.11f * tester;
                        }
                    }
                    else
                    {
                        move.y = -moveSpeed;
                    }
                }
                else if (yDir > 0)
                {
                    if (slide)
                    {
                        if (move.y < 0)
                        {
                            move.y += moveSpeed * 0.002f * tester;
                        }
                        else
                        {
                            move.y += moveSpeed * 0.11f * tester;
                        }
                    }
                    else
                    {
                        move.y = moveSpeed;
                    }
                }
                else
                {
                    if (slide)
                    {
                        move.y -= move.y * 0.05f;
                    }
                    else
                    {
                        move.y = 0;
                    }
                }
                // Translate character
                //move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
                transform.position += move * Time.deltaTime;
            }
            handleMeleeAttack();
        }
        else
        {
            PlayCutscene1();
        }
    }

    private void LateUpdate()
    {
        //Spawn all the enemies 
        //spawnPoint.spawn();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Blood"))
        {
            slide = true;
        }
        else if(other.CompareTag("cutscene1"))
        {
            cutscene = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Blood"))
        {
            slide = false;
        }
    }

    void handleMeleeAttack()
    {
        if(meleeTimeCounter > 0)
        {
            meleeTimeCounter -= Time.deltaTime;
        }
        else if(meleeTimeCounter <= 0)
        {
            meleeAnimator.SetBool("Swipe-Melee-Attacking", false);
            meleeSpriteRenderer.enabled = false;
        }
    }

    void PlayCutscene1()
    {
        CameraController c1 = camera.GetComponent<CameraController>();
        c1.setTarget();
    }
}
