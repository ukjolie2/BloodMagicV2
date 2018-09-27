﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSkill : SkillClass {

    private PlayerController player;
    public void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        Power = 5;
        InvokeRepeating("DestroySelf", 0.1f, 1f);
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    public override void UseAbility()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Rigidbody2D body = gameObject.GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            BasicEnemyController enemyHp = other.gameObject.GetComponent<BasicEnemyController>();
            enemyHp.hp -= Power;
        }
    }
}