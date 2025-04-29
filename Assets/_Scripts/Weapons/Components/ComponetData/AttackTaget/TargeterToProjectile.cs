
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
    public override void Refest()
    {
        base.Refest();
        targeter.TargeterTrigger -= SpwanTargetProjectile; 
    }
    private void SpwanTargetProjectile( List<Transform> targeters ){
        if(targeters.Count <= 0 || targeters == null){
            var projectile1 = SpwanProjectile();				
            projectile1.SetData(12f , transform.right , transform.tag, stats);

        }else{
            float dir = Mathf.Sign(targeters[0].position.x - transform.position.x);
            movement.CheckIfShouldFlip((int)dir);
            var projectile = SpwanProjectile();					
            projectile.SetData(12f , targeters[0], transform.tag, stats);
        }
    }
    Projectile SpwanProjectile(){
        return PoolsContainer.GetObject(data.prefabProjectile , transform.position + new Vector3(0,Core.Height/2,0));
    }
}