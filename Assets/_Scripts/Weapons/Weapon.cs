using System;
using UnityEngine;
using UnityEngine.UIElements;
public class Weapon : MonoBehaviour {
    [field: SerializeField] public SkillData_SO Data { get; private set; }
    public event Action OnEnter;
    public event Action OnMidd;
    public event Action OnExit;
    public event Action OnStarMove;
    public event Action OnStopMove;
    //------------------------------------
    #region ID_Skill
    [Header("ID_Skill")]
    [SerializeField] public WeaponSprite wpSprite;
	public int idSkill = 0;
    public int cf;
    public int LengthSkill = 1;
	int curIdSkill = -1;
	int i0,dx0,dy0,eff0Lenth;
	int i1,dx1,dy1,eff1Lenth;
	int i2,dx2,dy2,eff2Lenth;
	public float speedAttack1;
    #endregion
	[SerializeField] public Skill[] skills;
    [SerializeField] SkillInfo[] currentSkill;

    //------------------------------------
    public Core core{ get; private set;}
    public void AttackWeapon(int FrameCurrent){

        if(curIdSkill != idSkill){
				currentSkill = skills[idSkill].skillStand;
                LengthSkill = currentSkill.Length;
				curIdSkill = idSkill;					
		}

        if(FrameCurrent >= LengthSkill){
				wpSprite.SetSkillOff();
				Exit();
				return;
		}
        if(FrameCurrent == LengthSkill/2){
				Mid();
		}
        cf = currentSkill[FrameCurrent].status;
        if(FrameCurrent == 2) OnStarMove?.Invoke();
        else OnStopMove?.Invoke();
        
        // ----------------- effS0Id -------------
        if(currentSkill[FrameCurrent].effS0Id != 0){
            wpSprite.LoadEff0(currentSkill[FrameCurrent].effS0Id);
            eff0Lenth = wpSprite.GetLenghtEff0();
            i0 = (dx0 = (dy0 = 0));
        }

        if(i0 <= eff0Lenth) {
            if (dx0 == 0) dx0 = currentSkill[FrameCurrent].e0dx;
            if (dy0 == 0) dy0 = currentSkill[FrameCurrent].e0dy;									
            wpSprite.PaintEff0(i0,dx0,dy0);
            i0++;
        }else eff0Lenth =-1;

        // ----------------- effS1Id -------------
        if(currentSkill[FrameCurrent].effS1Id != 0){
            wpSprite.LoadEff1(currentSkill[FrameCurrent].effS1Id);
            eff1Lenth = wpSprite.GetLenghtEff1();
            i1 = (dx1 = (dy1 = 0));
        }
        if(i1 <= eff1Lenth) {
            if (dx1 == 0) dx1 = currentSkill[FrameCurrent].e1dx;
            if (dy1 == 0) dy1 = currentSkill[FrameCurrent].e1dy;									
            wpSprite.PaintEff1(i1,dx1,dy1);
            i1++;
        }else eff1Lenth =-1;

        // ----------------- effS2Id -------------
        if(currentSkill[FrameCurrent].effS2Id != 0){
            wpSprite.LoadEff2(currentSkill[FrameCurrent].effS2Id);
            eff2Lenth = wpSprite.GetLenghtEff2();
            i2 = (dx2 = (dy2 = 0));
        }

        if(i2 <= eff2Lenth) {
            if (dx2 == 0) dx2 = currentSkill[FrameCurrent].e2dx;
            if (dy2 == 0) dy2 = currentSkill[FrameCurrent].e2dy;									
            wpSprite.PaintEff2(i2,dx2,dy2);
            i2++;
        }else eff2Lenth =-1;
    }
    public void SetCore(Core core)
    {
        this.core = core;
    }
    public void SetData(SkillData_SO data){
        this.Data = data;
    }

    public void Enter(){
        //print($"{transform.name} enter");
        OnEnter?.Invoke();
    }

    private void Exit()
    {       
        OnStopMove?.Invoke();   
        OnExit?.Invoke();
    }
    private void Mid()
    {       
        OnMidd?.Invoke();
    }

    private void Awake() {
        wpSprite = GetComponent<WeaponSprite>();
    }
}