using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that defines what an enemy ability has to implement
/// in order to be used in the game
/// </summary>
public abstract class EnemySkillClass : MonoBehaviour
{
    public abstract void UseAbility();

    public GameObject target;

    //How much damage the attack does to target
    public int Power;

}
