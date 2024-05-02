using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] bool combatEnabled = true;
    float inputTimer;
    [SerializeField] float lastInputTime;
    
    bool gotInput,isAtacking,isFristAttack;
    Char_anim char_Anim;
    private void Awake() {
        LoadCompnents();
    }
    private void Reset() {
        LoadCompnents();
    }
    void LoadCompnents(){
		if(char_Anim == null) char_Anim = GetComponent<Char_anim>();
    }
    private void Update() {
        
        checkCombatInput();
        checkAttack();

    }
    void checkCombatInput(){
        if(Input.GetMouseButtonDown(0)){
            if(combatEnabled){
                gotInput = true;
            }
        }
    }
    void checkAttack(){
        if(gotInput){
            char_Anim.stage = 4;
            gotInput = false;
        }
    }
}
