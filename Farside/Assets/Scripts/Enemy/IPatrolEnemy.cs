using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPatrolEnemy
{
    Rigidbody2D Body();
    void Flip();
    bool IsFacingRight();
    bool IsDead();
}
