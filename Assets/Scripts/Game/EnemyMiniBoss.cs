using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class EnemyMiniBoss : ViewController,IEnemy
	{
		public enum State
		{
			Following_Player,
			Warning,
			Dash,
			Wait
		}
		public float speed = 1.2f;
		public float HP = 30;
		bool mIgnoreHurt = false;
        public void Hurt(float damage)
        {
			if (mIgnoreHurt)
			{
				return;
			}
			FloatingTextController.Play(transform.position,""+damage);
			Sprite.color = Color.red;
			AudioKit.PlaySound("Hit");
			ActionKit.Delay(0.2f, () =>
			{
				HP -= damage;
				Sprite.color = Color.white;
				mIgnoreHurt = false;
			}).Start(this);
        }
		private FSM<State> enemyState = new FSM<State>();
        void Start()
		{
			EnemyGenerator.EnemyCount.Value++;
			// Code Here
			enemyState.State(State.Following_Player)
				.OnFixedUpdate(() =>
				{
					if (Player.Default)
					{
						Vector3 direction = (Player.Default.transform.position-transform.position).normalized;
						SelfRigidbody2D.velocity = direction * 1.2f;
						if ((Player.Default.Position() - transform.Position()).magnitude < 10)
						{
							enemyState.ChangeState(State.Warning);
						}
					}
					else
					{
						SelfRigidbody2D.velocity = Vector2.zero;
					}
				});
			enemyState.State(State.Warning)
				.OnEnter(() =>
				{
					SelfRigidbody2D.velocity = Vector2.zero;
				})
				.OnUpdate(() =>
				{
					// Construct a Linear Approximate function try it
					// when get colser and colser. the color change more frequently
					long frame = 3 + (60*3 - enemyState.FrameCountOfCurrentState)/ 10;
					if (enemyState.FrameCountOfCurrentState / frame % 2 == 0)
					{
						Sprite.color = Color.red;
					}
					else
					{
						Sprite.color = Color.white;
					}
					if(enemyState.FrameCountOfCurrentState >= 60 * 3)
					{
						enemyState.ChangeState(State.Dash);
					}
				}).OnExit(() =>
				{
					Sprite.color = Color.white;
				});
				float startDistance = 0;
			enemyState.State(State.Dash)
				.OnEnter(() =>
				{
					if (Player.Default)
					{
						Vector3 direction = (Player.Default.Position() - this.Position()).normalized;
						startDistance = (Player.Default.Position() - this.Position()).magnitude;
						Debug.Log("Boss Dash start Distance: " + startDistance);
						SelfRigidbody2D.velocity = direction * speed;
					}
				})
				.OnUpdate(() =>
				{
					if((Player.Default.Position() - this.Position()).magnitude >=  startDistance)
						{
							Debug.Log("Curent Boss And Player Distance hit the State Change Standard");
							enemyState.ChangeState(State.Wait);
						}
				});
			enemyState.State(State.Wait)
			.OnEnter(() =>
			{
				Debug.Log("进入到Wait状态");
				SelfRigidbody2D.velocity = Vector2.zero;
			})
			.OnUpdate(() =>
			{
				if (enemyState.FrameCountOfCurrentState == 30)
				{
					enemyState.ChangeState(State.Following_Player);
				}
			});
			enemyState.StartState(State.Following_Player);
		}
        void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
        public void Update()
        {
            enemyState.Update();
        }
        public void FixedUpdate()
        {
            enemyState.FixedUpdate();
        }

        public void SetHpScale(float hPScale)
        {
            HP *= hPScale;
        }

        public void SetSpeedScale(float speedScale)
        {
            speed *= speedScale;
        }
    }
}
