using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //this class spawns the attacks the player uses
    CreateAttack attacks;
    HealthChange healthBar;

    //temporary spawnHolder
    public GameObject spawner;
    EnemySpawn spawnPoint;

    //stats
    public int hp = 100;

    //movement variables
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    public bool usingController = false;
    public float dashSpeed = 100.0f;
    public float dashCoolDownTime = 3; // used so that the timer length can be modified in the unity editor
    private float dashTimer = 1;

    //attacking variables
    private bool usedRightTrigger = false;

    // Use this for initialization
    void Start ()
    {
        attacks = gameObject.GetComponent<CreateAttack>();
        healthBar = gameObject.GetComponent<HealthChange>();
        spawnPoint = spawner.GetComponent<EnemySpawn>();
        dashTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        //Death
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        // Translate character
        var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        transform.position += move * moveSpeed * Time.deltaTime;

        // Dash - controller & keyboard
        dashTimer -= Time.deltaTime; // update cooldown timer
        if (Input.GetButtonDown("Dash") || Input.GetButtonDown("X Button") || Input.GetButtonDown("Left Bumper"))
        {
            // make sure enough time has passed between dashes
            if (dashTimer < 0)
            {
                transform.position += move * dashSpeed * Time.deltaTime;
                dashTimer = dashCoolDownTime;
            }
        }

        //Attack
        if(usingController)
        {
            if (Input.GetAxis("Right Trigger") == 0)
            {
                usedRightTrigger = false;
            }
            if (Input.GetAxis("Right Trigger") == 1)
            {
                if (!usedRightTrigger)
                {
                    attacks.AttackCloseRange();
                    usedRightTrigger = true;
                }
            }
        }
        else if (Input.GetButtonDown("Fire1"))
        {
            attacks.AttackCloseRange();
        }


        // Rotation
        Vector2 direction;
        float angle = 0f;
        Quaternion rotation;

        if (usingController)
        {
            // Rotate character using right joystick
            direction = new Vector2(Input.GetAxis("Right Joystick Horizontal"), Input.GetAxis("Right Joystick Vertical"));
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
    }

    private void LateUpdate()
    {
        //Spawn all the enemies 
        // spawnPoint.spawn();
    }
}
