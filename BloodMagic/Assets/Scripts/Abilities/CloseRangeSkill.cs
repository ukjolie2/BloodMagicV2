using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeSkill : SkillClass {

    private PlayerController player;
    public int speedFraction = 2;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        HpCost = 50;
        HpReturn = 51;
        Power = 5;
        InvokeRepeating("DestroySelf", 0.1f, 1f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override void UseAbility()
    {
        player.hp -= HpCost;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemyHp = other.gameObject.GetComponent<EnemyController>();
            enemyHp.hp -= Power;
            player.hp += HpReturn;
        }
    }
}