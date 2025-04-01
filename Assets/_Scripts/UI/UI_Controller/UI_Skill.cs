using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SkillLoad : MonoBehaviour {
   Player player;
   Cooldown cooldowns;
   

   [Header(" Skill 1 ------------")]
   
   [SerializeField] Image skillImage1;
   [SerializeField] Image imageLoad1;
   [SerializeField] TextMeshProUGUI skillText1;
   PlayerAttackState skill1;


   [Header(" Skill 2 ------------")]
   
   [SerializeField] Image skillImage2;
   [SerializeField] Image imageLoad2;
   [SerializeField] TextMeshProUGUI skillText2;
   PlayerAttackState skill2;

   [Header(" Skill 3 ------------")]
   
   [SerializeField] Image skillImage3;
   [SerializeField] Image imageLoad3;
   [SerializeField] TextMeshProUGUI skillText3;
   PlayerAttackState skill3;
   [Header(" Dash ------------")]
   [SerializeField] Image imageLoad4;
   [SerializeField] TextMeshProUGUI skillText4;
   PlayerDashState dash;

   void Start()
   {
      player = PlayerManager.GetPlayer();
      skill1 = player.Attack_1;
      skill2 = player.Attack_2;
      skill3 = player.Attack_3;
      dash = player.dashState;
      cooldowns = player.cooldowns;
      skill1.GetSkill().OnSetIcon += SetIcon1;
      skill2.GetSkill().OnSetIcon += SetIcon2;
      skill3.GetSkill().OnSetIcon += SetIcon3;
      
   }
   
   void OnDisable() {
      skill1.GetSkill().OnSetIcon -= SetIcon1;
      skill2.GetSkill().OnSetIcon -= SetIcon2;
      skill3.GetSkill().OnSetIcon -= SetIcon3;
   }

   void SetIcon1(Sprite icon){
      if(icon == null){
         skillImage1.transform.parent.gameObject.SetActive(false);
      }else{
         skillImage1.transform.parent.gameObject.SetActive(true);
         skillImage1.sprite = icon;
      }
   }
   void SetIcon2(Sprite icon){
      if(icon == null){
         skillImage2.transform.parent.gameObject.SetActive(false);
      }else{
         skillImage2.transform.parent.gameObject.SetActive(true);
         skillImage2.sprite = icon;
      }
   }
   void SetIcon3(Sprite icon){
      if(icon == null){
         skillImage3.transform.parent.gameObject.SetActive(false);
      }else{
         skillImage3.transform.parent.gameObject.SetActive(true);
         skillImage3.sprite = icon;
      }
   }
   void Update()
   {
      LoadSkill(skill1, imageLoad1, skillText1);
      LoadSkill(skill2, imageLoad2, skillText2);
      LoadSkill(skill3, imageLoad3, skillText3);
      LoadDash(dash, imageLoad4, skillText4);
   }
   void LoadSkill( PlayerAttackState skill, Image imageLoad, TextMeshProUGUI skillText){
      if( cooldowns.IsDone(skill) ){
         if( imageLoad.gameObject.activeSelf == true ){
            imageLoad.gameObject.SetActive(false);
         }
      }else{

         if( imageLoad.gameObject.activeSelf == false ){
            imageLoad.gameObject.SetActive(true);
         }

         imageLoad.fillAmount = cooldowns.GetTime(skill) / skill.GetSkill().cooldown;
         skillText.text = cooldowns.GetTime(skill).ToString("F1");

      }
   }
   void LoadDash(PlayerDashState dash, Image imageLoad, TextMeshProUGUI skillText){
      if( cooldowns.IsDone(dash) ){
         if( imageLoad.gameObject.activeSelf == true ){
            imageLoad.gameObject.SetActive(false);
         }
      }else{

         if( imageLoad.gameObject.activeSelf == false ){
            imageLoad.gameObject.SetActive(true);
         }

         imageLoad.fillAmount = cooldowns.GetTime(dash) / dash.cooldownDash;
         skillText.text = cooldowns.GetTime(dash).ToString("F1");

      }
   }
}