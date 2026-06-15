using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class Enemy : ViewController,IEnemy
	{
		public float HP = 3;
		public float moveSpeed = 2.0f;
		public Color DissolveColor = Color.yellow;
		void Start()
		{
			// Code Here
			EnemyGenerator.EnemyCount.Value++;
		}
        void OnDestroy()
        {
			// 这里出现Bug了，SampleScene被重新加载时，之前的Enmey对象被销毁，但是我们的Global数据重置方法再这些Enmey对象销毁之前。
            EnemyGenerator.EnemyCount.Value--;
        }
		void FixedUpdate()
		{
			// 每个Enemy向着Player移动的逻辑
			if (Player.Default)
			{
				var direction = (Player.Default.transform.position - this.transform.position).normalized;
				SelfRigidbody2D.velocity = direction * moveSpeed;
				// this.transform.Translate(direction *moveSpeed* Time.deltaTime);
			}
			else
			{
				SelfRigidbody2D.velocity = Vector2.zero;
			}
		}
        void Update()
        {
			

			if(HP <= 0)
			{
				this.DestroyGameObjGracefully();
				
				// Global.Exp.Value++;
				FxController.Play(Sprite,DissolveColor);
				// 敌人掉落经验值功能
				Global.GeneratePowerUp(gameObject);
			}
        }
		bool mIgnoreHurt = false;

		public void Hurt(float damage)
		{
			if(mIgnoreHurt)
			{
				return;
			}
			FloatingTextController.Play(transform.position,""+damage);
			Sprite.color = Color.red;
			AudioKit.PlaySound("Hit");
			ActionKit.Delay(0.2f, () =>
			{
				HP-= damage;
				Sprite.color = Color.white;
				mIgnoreHurt = false;
			}).Start(this);
		}

        public void SetHpScale(float hPScale)
        {
            HP *= hPScale;
        }

        public void SetSpeedScale(float speedScale)
        {
			moveSpeed *= speedScale;
        }
    }
}
