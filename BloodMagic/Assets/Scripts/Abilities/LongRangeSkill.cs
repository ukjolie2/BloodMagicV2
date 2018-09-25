using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeSkill : SkillClass {

    private PlayerController player;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        HpCost = 50;
        HpReturn = 51;
        Power = 5;
        InvokeRepeating("DestroySelf", 3, 1f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override void UseAbility()
    {
        player.hp -= HpCost;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyHp = other.gameObject.GetComponent<EnemyController>();
            enemyHp.hp -= Power;
            player.hp += HpReturn;
        }
        if(!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}