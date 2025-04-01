using UnityEngine;
using DG.Tweening;
using HStrong.ProjectileSystem;

public class E_AttackState : EnemyAbilityState
{
	Vector3 posCurrent,targetPosition ;
	Projectile projectile_Scrip;
	int[] attackAnim ;
    public E_AttackState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

	public override void Enter() {
		base.Enter();
		setAttack();
		movement.SetVelocityZero();
		posCurrent = enemy.transform.position;
		targetPosition.Set( posCurrent.x - movement.facingDirection*0.5f ,posCurrent.y, posCurrent.z);
        enemy.transform.DOMove(targetPosition, 0.1f/ enemyData.speedAtk).SetEase(enemy.ease)


            .OnComplete((TweenCallback)(()=>{
				
                Vector3 dirAttack = (enemy.playerCheck.position + Vector3.up*core.height/2) - enemy.transform.position;
				Vector2 pos = new Vector2(enemy.transform.position.x, enemy.transform.position.y + core.height/2);
                projectile_Scrip = PoolsContainer.GetObject(enemy.prefabProjectile, pos);
                
                projectile_Scrip.SetProjectile(15 , dirAttack.normalized, enemy.gameObject.tag, stats);

                enemy.transform.DOMove(posCurrent, 0.2f/ enemyData.speedAtk).SetEase(enemy.easeEnd)
                .OnComplete(()=>{;
                    isAbilityDone = true;
                });
        }));

	}

	public override void Exit() {
		base.Exit();
		cooldowns.Start(enemy.attackState, enemyData.speedAtk);
	}
	public override void LogicUpdate() {

		base.LogicUpdate();
		if(isExitingState) return;

		movement.SetVelocityZero();
		
		switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:
                if(TimeRate(0.2f/enemyData.speedMove)) return;
                FrameCurrent = (FrameCurrent + 1)%2;  
				enemy.Paint( attackAnim[FrameCurrent]);

                break;
            case 2:
                break;
            case 4:
                if(TimeRate(0.2f/enemyData.speedMove)) return;
               
                FrameCurrent = ( FrameCurrent + 1)%2;
				enemy.Paint( attackAnim[FrameCurrent] );
				
                break;
            default:
                enemy.Paint(0);
				break;

        }        
	}
	void setAttack(){
		if( enemyData.textures.Length >= 4){
			attackAnim = new int[] {0,3};
		}else{
			attackAnim = new int[] {0,1};
		}

	}
}