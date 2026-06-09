using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class SimpleAxe : ViewController
	{
		void Start()
		{
			// Code Here
		}
		private float mCurrentSeconds = 0;
		void Update()
		{
			mCurrentSeconds += Time.deltaTime;
			if(mCurrentSeconds >= 1.0f)
			{
				Axe.Instantiate()
				.Position(this.Position())
				.Show()
				.Self(self =>
				{
					var rigidBody = self.GetComponent<Rigidbody2D>();
					var randomX = RandomUtility.Choose(-8,-5,-3,3,5,8);
					var randomY = RandomUtility.Choose(5,10);
					rigidBody.velocity = new Vector2(randomX,randomY);
					self.OnTriggerEnter2DEvent(collider =>
					{
                        HurtBox hurtBox = collider.GetComponent<HurtBox>();
						if (hurtBox != null)
						{
							if (hurtBox.Owner.CompareTag("Enemy"))
							{
                                Enemy enemy = hurtBox.Owner.GetComponent<Enemy>();
								enemy.Hurt(2);
							}
						}
					}).UnRegisterWhenGameObjectDestroyed(self);
					ActionKit.OnUpdate.Register(() =>
					{
						if (Player.Default)
						{
								if ((Player.Default.transform.position.y - self.gameObject.transform.position.y) > 15)
								{
									self.DestroyGameObjGracefully();
								}
						}
					}).UnRegisterWhenGameObjectDestroyed(self);
				});
				mCurrentSeconds = 0;
			}
		}
	}
}
