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
        HpCost = 10;
        HpReturn = 9;
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
            Invoke("ReturnHealth", 0.6f);
        }
        else
        {
            transform.Rotate(0, 0, 90);
        }
        Invoke("DestroySelf", 0.1f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && name == "Player")
        {
            BasicEnemyController enemyHp = other.gameObject.GetComponent<BasicEnemyController>();
            enemyHp.hp -= Power;
        }
        else if (name == "Enemy" && other.CompareTag("Player"))
        {
            player.hp -= Power;
        }
    }

    private void ReturnHealth()
    {
        Debug.Log("hello");
        if (player.hp + HpReturn > 150)
        {
            player.hp = 150;
        }
        else
        {
            player.hp += HpReturn;
        }
    }
}