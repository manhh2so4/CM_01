using UnityEngine;
using DG.Tweening;

public class E_AttackState : EnemyAbilityState
{
	Vector3 posCurrent,targetPosition ;
	protected GameObject projectile;
	protected Projectile projectile_Scrip;
	Vector3 dirAttack = new Vector3();

    public E_AttackState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }
    public override void DoChecks() {
		base.DoChecks();
		
	}

	public override void Enter() {
		base.Enter();
		Movement.SetVelocityZero();
		posCurrent = enemy.transform.position;
		targetPosition.Set( posCurrent.x - Movement.facingDirection*0.5f,posCurrent.y,posCurrent.z); 
		enemy.transform.DOMove( targetPosition, 0.1f/enemyData.speedAtk).SetEase(enemy.ease)


            .OnComplete(()=>{
				dirAttack = enemy.playerCheck.position - enemy.transform.position;

				projectile = GameObject.Instantiate(enemy.projectile, enemy.transform.position, enemy.transform.rotation);				
				projectile_Scrip = projectile.GetComponent<Projectile>();

				
        		projectile_Scrip.FireProjectile(15 , dirAttack.normalized , 10 , enemy.gameObject.tag );

                enemy.transform.DOMove(posCurrent, 0.2f/enemyData.speedAtk).SetEase(enemy.easeEnd)
                .OnComplete(()=>{;
                isAbilityDone = true;

                });
            });

	}

	public override void Exit() {
		base.Exit();
		
	}
	public override void LogicUpdate() {

		base.LogicUpdate();
		Movement.SetVelocityZero();
		switch(enemyData.type){			
			case 0:
				enemy.Paint(0);
				break;
            case 1:
                if(TimeRate(0.2f/enemyData.speedMove)) return;
                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;  

                break;
            case 2:
                break;
            case 4:
                if(TimeRate(0.2f/enemyData.speedMove)) return;

                enemy.Paint(FrameCurrent);
                FrameCurrent = ( FrameCurrent + 1)%2;

                break;
            default:
                enemy.Paint(0);
				break;

        }        
	}

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
}