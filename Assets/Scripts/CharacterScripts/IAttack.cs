using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    Character Character{get; set;}
    float AttackSpeed {get; set;}
    void AttackAsEnemy();
    void AttackAsPlayer();
}
