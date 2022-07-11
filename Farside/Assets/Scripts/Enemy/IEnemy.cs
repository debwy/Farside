using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    void TakeDamage(int damage); //allows enemy to take normal damage
    void TakeRangedDamage(int damage); //allows enemy to take modified ranged damage (or the lack of it)
    void Death(); //sets hp to 0, then dies (used for enemy & traps interactions)
    void Die(); //ability to die (usually paired with despawn method)
    bool IsDead(); //returns whether enemy is dead (for other scripts, etc. patrol)
    void NotifyAggro(bool input);
    void Heal(int healing);
}
