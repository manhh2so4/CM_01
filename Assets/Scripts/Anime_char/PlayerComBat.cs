using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComBat : MonoBehaviour
{
    [SerializeField] bool comBatEnable;
    [SerializeField] float inputTimer,attackRadius,attack1Damage;
    [SerializeField] Transform attack1HitBoxPos;
    LayerMask whatIsDamageable;
    bool gotInput,isAttacking,isFirstAttack;
    float lastInputTime = Mathf.NegativeInfinity;
    private void Update() {
        CheckCombatInput();
        CheckAttack();
    }
    void CheckCombatInput(){
        if(Input.GetMouseButtonDown(0)){
            gotInput = true;
            lastInputTime = Time.time;
        }
    }
    private void CheckAttack(){
        if(gotInput){
            gotInput = false;
            isAttacking = true;
            isFirstAttack = !isFirstAttack;

        }
        if(Time.time >= (lastInputTime + inputTimer)){
            gotInput = false;
        }
    }
    void CheckAttackHitBox(){
        Collider2D[] detectedObjecs = Physics2D.OverlapCircleAll(attack1HitBoxPos.position,attackRadius,whatIsDamageable);
        foreach (Collider2D collider in detectedObjecs)
        {
            collider.transform.parent.SendMessage("Damage",attack1Damage );
        }
    }
     
    void FinishAttack1(){
        isAttacking = false;

    }
    void OnDrawGizmos() {
        Gizmos.DrawWireSphere(attack1HitBoxPos.position,attackRadius);
    }
}
