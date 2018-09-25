using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeSkill : SkillClass {

    private PlayerController player;
    public int speedFraction = 2;
    private string name;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        HpCost = 50;
        HpReturn = 51;
        Power = 5;
    }

    public void setName(string pass)
    {
        name = pass;
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override void UseAbility()
    {
        if(name == "Player")
        {
            player.hp -= HpCost;
            Invoke("ReturnHealth", 0.05f);
        }
        else
        {
            transform.Rotate(0, 0, 90);
        }
        Invoke("DestroySelf", 0.1f);
    }

    public void ReturnHealth()
    {
        if(player.hp + HpReturn > 100)
        {
            Debug.Log("this happened");
            player.hp = 150;
        }
        else
        {
            player.hp += HpReturn;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && gameObject.CompareTag("Player"))
        {
            BasicEnemyController enemyHp = other.gameObject.GetComponent<BasicEnemyController>();
            enemyHp.hp -= Power;
            ReturnHealth();
        }
        else if (name == "Enemy" && other.CompareTag("Player"))
        {
            player.hp -= Power;
        }
    }
}