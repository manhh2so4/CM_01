using UnityEngine;
using DG.Tweening;
using HStrong.ProjectileSystem;

public class E_AttackState : EnemyAbilityState
{
	Vector3 posCurrent,targetPosition ;
	Projectile projectile_Scrip;
	int[] attackAnim ;
	const float timeAttack = 0.15f;
    public E_AttackState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

	public override void Enter() {
		base.Enter();
		setAttack();
		movement.SetVelocityZero();
		posCurrent = enemy.transform.position;
		targetPosition.Set( posCurrent.x - movement.facingDirection*0.5f ,posCurrent.y, posCurrent.z);
        enemy.transform.DOMove(targetPosition, timeAttack).SetEase(enemy.ease)

            .OnComplete((TweenCallback)(()=>{

				Vector2 posStart = new Vector2(enemy.transform.position.x, enemy.transform.position.y + core.Height/2);
                Vector2 dirAttack = (Vector2)enemy.playerCheck.position + Vector2.up*core.Height/2 - posStart;
				
                projectile_Scrip = PoolsContainer.GetObject(enemyData.projectile, posStart);
                projectile_Scrip.SetData(15 , dirAttack.normalized, enemy.gameObject.tag, stats);

                enemy.transform.DOMove(posCurrent, timeAttack).SetEase(enemy.easeEnd)
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
            case 2:
            case 3:
                if( TimeRate(timeAttack) ) return;

				enemy.Paint( attackAnim[FrameCurrent]);
				FrameCurrent = (FrameCurrent + 1)%2; 

                break;
			case 5:
            case 4:
                if( TimeRate(timeAttack) ) return;
               
				enemy.Paint( attackAnim[FrameCurrent] );
				FrameCurrent = ( FrameCurrent + 1)%2;
				
                break;
            default:
                enemy.Paint(0);
				break;

        }        
	}
	void setAttack(){
		if( enemyData.Sprites.Length >= 4){
			attackAnim = new int[] {0,3};
		}else{
			attackAnim = new int[] {0,1};
		}

	}
}