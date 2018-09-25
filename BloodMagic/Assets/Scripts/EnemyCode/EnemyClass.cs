using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyClass : MonoBehaviour {

    public int hp;
    public float moveSpeed;
    public float rotationSpeed;
    public Transform target;
    public bool slide = false;
    public Vector3 direction;
}
