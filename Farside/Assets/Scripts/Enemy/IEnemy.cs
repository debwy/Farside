using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void TakeDamage(int damage); //allows enemy to take normal damage
    void TakeRangedDamage(int damage); //allows enemy to take ranged damage (or the lack of it)
    void Death(); //sets hp to 0, then dies
    void Die(); //ability to die (usually paired with despawn method)
    bool IsDead(); //for other scripts to be able to check whether enemy is alive

}
