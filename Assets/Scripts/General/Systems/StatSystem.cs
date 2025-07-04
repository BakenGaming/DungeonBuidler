using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatSystem
{
    private int health;
    private float moveSpeed;

    public StatSystem (PlayerStatsSO _stats)
    {
        health = _stats.health;
        moveSpeed = _stats.moveSpeed;
    }

    public StatSystem (EnemyStatsSO _stats)
    {
        health = _stats.health;
        moveSpeed = _stats.moveSpeed;

    }

    public int GetPlayerHealth (){return health;}
    public float GetMoveSpeed(){return moveSpeed;}
}
