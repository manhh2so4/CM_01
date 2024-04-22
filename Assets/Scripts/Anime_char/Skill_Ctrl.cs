using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Ctrl : MonoBehaviour
{
    public SkillInfoPaint[] skillStand;
	public SkillInfoPaint[] skillfly;
    public int skillId = 0;
	public int skillCurrent = -1;
    [SerializeField]public GameObject skill0;
    [SerializeField]public GameObject skill1;
    [SerializeField]public GameObject skill2;
	SkillInfor eff0;
	int i0;
	SkillInfor eff1;
	int i1;
	SkillInfor eff2;
	int i2;
    public Test_SpriteFX SpriteSkill;
    private void Reset() {
        LoadObjSkill();
    }
    void LoadObjSkill(){
		skill0 = transform.GetChild(0).gameObject;
		skill1 = transform.GetChild(1).gameObject;
		skill2 = transform.GetChild(2).gameObject;
		SpriteSkill = gameObject.GetComponent<Test_SpriteFX>();
	}
    public void changeSkill(){
		if(skillId == skillCurrent) return;
		SkillPaint[] a = Read_anim_skill.skillPaints;
		SkillInfoPaint[] temp1 = new SkillInfoPaint[a[skillId].skillStand.Length];
		for (int i = 0; i < a[skillId].skillStand.Length; i++)
		{
			temp1[i] = a[skillId].skillStand[i];			
		}
		skillStand = temp1;
		SkillInfoPaint[] temp2 = new SkillInfoPaint[a[skillId].skillfly.Length];
		for (int i = 0; i < a[skillId].skillfly.Length; i++)
		{
			temp2[i] = a[skillId].skillfly[i];
		}
		skillfly = temp2;
		skillCurrent = skillId;
	}
    private void Awake() {
		LoadObjSkill();
	}
    public void DrawSkill(Sprite sprite, int x, int y,GameObject _gameObject)
    {
		
        float x0 = x*4;
        float y0 = -y*4;
		
        _gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
		Vector3 move = new Vector3(x0/100,y0/100,0);
		_gameObject.transform.localPosition = move;
    } 
}
