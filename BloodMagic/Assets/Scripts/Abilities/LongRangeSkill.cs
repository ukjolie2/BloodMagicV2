using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeSkill : SkillClass {

    private PlayerController player;
    private string name;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        HpCost = 10;
        HpReturn = 10;
        Power = 5;
        InvokeRepeating("DestroySelf", 3, 1f);
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
        if (gameObject.tag == "Player")
        {
            player.hp -= HpCost;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Enemy") && name == "Player")
        {
            BasicEnemyController enemyHp = other.gameObject.GetComponent<BasicEnemyController>();
            enemyHp.hp -= Power;
            player.hp += HpReturn;
        }
        else if(name == "Enemy" && other.CompareTag("Player"))
        {
            player.hp -= Power;
        }
        if(!other.CompareTag(name))
        {
            Destroy(gameObject);
        }
    }
}