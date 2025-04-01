using HStrong.ProjectileSystem;
using UnityEngine;
[System.Serializable]
public class B_Rage : BossAbilityState
{
    int count = 0;
    public int spawnCount = 4;
    //float height;
    Vector2 SpawnPos;

    public B_Rage(Boss boss, FiniteStateMachine stateMachine) : base(boss, stateMachine)
    {
		
    }
	public override void Enter() {
		base.Enter();
		boss.draw_Boss.state = StateEnemy.Skill_2;
        count = 0;
		boss.draw_Boss.OnAttackDone += AttakDone;
		boss.draw_Boss.OnTakeDamage += TakeDamage;
		movement.SetVelocityZero();
        SpawnPos = new Vector2( boss.transform.position.x, boss.transform.position.y + 15);
	}
	public override void LogicUpdate(){
		base.LogicUpdate();
		if(isExitingState) return;

        if(count > 8){
            ExitHandler();
        }


	}
	public override void Exit() {
		base.Exit();
        cooldowns.Start( boss.rage, 15 );
		boss.draw_Boss.OnAttackDone -= AttakDone;
		boss.draw_Boss.OnTakeDamage -= TakeDamage;
	}



    void AttakDone(){
        for(int i = 0; i < spawnCount; i++){
            SpawnRock();
        }
        hitBoxWeapon.ClearObj();
        movement.Flip();
        count++;
    }
    void SpawnRock(){
        float randomX = Random.Range(SpawnPos.x - 10, SpawnPos.x + 10);
        float randomY = Random.Range(SpawnPos.y - 3, SpawnPos.y + 3);
        Vector2 randomPos = new Vector2(randomX, randomY);
        Projectile rock = PoolsContainer.GetObject(boss.PrefabProjectile, randomPos);
        rock.SetProjectile(boss.tag, 12);
    }

}