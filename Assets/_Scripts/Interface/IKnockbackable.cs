using UnityEngine;

public interface IKnockBackable 
{
    void KnockBack(Vector2 angle = new Vector2(), float strength = 0, int direction = 0);
}
