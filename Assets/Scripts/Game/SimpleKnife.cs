using UnityEngine;
using QFramework;
using System.Linq;
using UnityEditor.UI;
using System;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class SimpleKnife : ViewController
	{
		void Start()
		{
			// Code Here
		}
		private float mCurrentSeconds = 0;
        void Update()
        {
            mCurrentSeconds += Time.deltaTime;
			if (Player.Default)
			{
				if(mCurrentSeconds >= 1.0f)
				{
					mCurrentSeconds = 0;
					var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
					var enemy = enemies.OrderBy(enemy=>((Player.Default.transform.position-enemy.transform.position)).magnitude).FirstOrDefault();
					if (enemy)
					{
						Knife.Instantiate()
							.Position(this.Position())
							.Show()
							.Self(self =>
							{
                                Rigidbody2D rigidbody = self.GetComponent<Rigidbody2D>();
								var direction = enemy.Position()-Player.Default.Position();
								rigidbody.velocity = direction * 2;
								self.OnTriggerEnter2DEvent(collider=>
								{
                                    HurtBox hurtBox = collider.GetComponent<HurtBox>();
									if (hurtBox)
									{
										if (hurtBox.Owner.CompareTag("Enemy"))
										{
                                            Enemy enemy1 = hurtBox.Owner.GetComponent<Enemy>();
											enemy1.Hurt(5);
											self.DestroyGameObjGracefully();
										}
									}
								}).UnRegisterWhenGameObjectDestroyed(self);
								ActionKit.OnUpdate.Register(() =>
								{
									if((Player.Default.Position()-enemy.Position()).magnitude > 15)
									{
										self.DestroyGameObjGracefully();
									}
								}).UnRegisterWhenGameObjectDestroyed(self);
							});
					}
					
				}
			}
        }
    }
}
