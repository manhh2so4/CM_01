using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] bool combatEnabled = true;
    [SerializeField]float inputTimer;
    float Timer = 0;
    public int Dame = 10;
    
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
        if(Input.GetMouseButtonDown(0)|| Input.GetKeyDown(KeyCode.Return)){
            if(combatEnabled){
                gotInput = true;
            }
        }
    }
    void checkAttack(){
        if(gotInput){
            char_Anim.stage = 4;
        }
        Timer += Time.deltaTime;
        if(Timer >= inputTimer) gotInput = false; 

    }
}
