using UnityEngine;
using DG.Tweening;

public class E_AttackState : EnemyAbilityState
{
	Vector3 posCurrent,targetPosition ;
	protected UnityEngine.GameObject projectile;
	protected Projectile_one projectile_Scrip;
	Vector3 dirAttack = new Vector3();
	int[] attackAnim;

    public E_AttackState(Enemy enemy, FiniteStateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

	public override void Enter() {
		base.Enter();
		setAttack();
		movement.SetVelocityZero();
		posCurrent = enemy.transform.position;
		targetPosition.Set( posCurrent.x - movement.facingDirection*0.5f,posCurrent.y,posCurrent.z); 
		enemy.transform.DOMove( targetPosition, 0.1f/enemyData.speedAtk).SetEase(enemy.ease)


            .OnComplete(()=>{
				dirAttack = enemy.playerCheck.position - enemy.transform.position;

                projectile = UnityEngine.GameObject.Instantiate(enemy.projectile, enemy.transform.position, enemy.transform.rotation);				
				projectile_Scrip = projectile.GetComponent<Projectile_one>();

				
        		projectile_Scrip.SetProjectile(15 , dirAttack.normalized , 10 , enemy.gameObject.tag,stats);

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

	public override void PhysicsUpdate() {
		base.PhysicsUpdate();
	}
	void setAttack(){
		if( enemyData.textures.Length >= 4){
			attackAnim = new int[] {0,3};
		}else{
			attackAnim = new int[] {0,1};
		}

	}
}