
using System.Collections.Generic;
using HStrong.ProjectileSystem;
using Unity.Mathematics;
using UnityEngine;

public class TargeterToProjectile : WeaponComponents<ProjecttileData>
{
    WeaponTargeter targeter;
    CharacterStats stats;
    protected Movement movement;


    protected override void SubscribeHandlers()
    {
        base.SubscribeHandlers();
        stats = Core.GetCoreComponent<CharacterStats>();
        targeter = GetComponent<WeaponTargeter>();
        targeter.TargeterTrigger += SpwanTargetProjectile;  
        movement = Core.GetCoreComponent<Movement>();     
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        targeter.TargeterTrigger -= SpwanTargetProjectile; 
    }
    private void SpwanTargetProjectile(List<Transform> targeters){
        if(targeters.Count <= 0 || targeters == null){
            var projectile1 = GameObject.Instantiate(data.prefabProjectile, transform.position + new Vector3(0,Core.height/2,0), transform.rotation);				
		    Projectile projectile_Scrip1 = projectile1.GetComponent<Projectile>();	
            Debug.Log("transform.right" + transform.right);
            projectile_Scrip1.SetProjectile(12f , transform.right , transform.parent.tag, stats);

        }else{
            float dir = Mathf.Sign(targeters[0].position.x - transform.position.x);
            movement.CheckIfShouldFlip((int)dir);
            var projectile = GameObject.Instantiate(data.prefabProjectile, transform.position + new Vector3(0,Core.height/2,0), transform.rotation);				
            var projectile_Scrip = projectile.GetComponent<Projectile>();	
            projectile_Scrip.SetProjectile(12f , targeters[0], transform.parent.tag, stats);
        }
    }


}