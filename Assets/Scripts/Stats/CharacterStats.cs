using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public Skill defence;
    public Skill magic;
    public Skill ranged;

    public int maxHitpoints;
    public int currentHitpoints;

    public Stat stabAtk;
    public Stat slashAtk;
    public Stat crushAtk;
    public Stat magicAtk;
    public Stat rangeAtk;

    public Stat stabDef;
    public Stat slashDef;
    public Stat crushDef;
    public Stat magicDef;
    public Stat rangeDef;

    public Stat meleeStr;
    public Stat rangeStr;
    public Stat magicStr;

    public Stat prayerBonus;

    void Awake()
    {
        currentHitpoints = maxHitpoints;
    }

    public void TakeDamage(int damage, ProjectileType type)
    {
        currentHitpoints -= damage;
        currentHitpoints = Mathf.Clamp(currentHitpoints, 0, maxHitpoints);

        Debug.Log(transform.name + " takes " + damage + " damage");

        if(currentHitpoints <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died");
    }
}
