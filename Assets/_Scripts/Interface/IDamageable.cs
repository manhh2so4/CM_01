using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    CharacterStats GetTarget(BaseEffect prefabHit);
}
