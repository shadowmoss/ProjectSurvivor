using UnityEngine;
using QFramework;
using System.Linq;
using UnityEditor.UI;
using System;
using QAssetBundle;
using ProjectSurvior;

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
				if(mCurrentSeconds >= Global.SimpleKnifeDuration.Value)
				{
					mCurrentSeconds = 0;
					var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude,FindObjectsSortMode.None)
									   .OrderBy(enemy=>Player.Default.Distance2D(enemy))
									   .Take(Global.SimpleKnifeCount.Value+Global.AdditionalFlyThingCount.Value);
					var i =0;								   
					foreach (var enemy in enemies)
					{
						if(i < 4)
						{
							ActionKit.DelayFrame(11 * i, () =>
							{
								AudioKit.PlaySound(Sfx.KNIFE);
							}).StartGlobal();
							i++;
						}
						
						if (enemy)
						{
							Knife.Instantiate()
								.Position(this.Position())
								.Show()
								.Self(self =>
								{
									var selfCache = self;
									
									Rigidbody2D rigidbody = self.GetComponent<Rigidbody2D>();
									var direction = enemy.NormalizedDirection2DFrom(Player.Default);
									self.transform.up = direction;
									rigidbody.velocity = direction * 5;
									var attackCount = 0;
									selfCache.OnTriggerEnter2DEvent(collider=>
									{
										HurtBox hurtBox = collider.GetComponent<HurtBox>();
										if (hurtBox)
										{
											if (hurtBox.Owner.CompareTag("Enemy"))
											{
												Enemy enemy1 = hurtBox.Owner.GetComponent<Enemy>();
												// enemy1.Hurt(Global.SimpleKnifeDamage.Value);
												DamageSystem.CalculateDamage(Global.SimpleKnifeDamage.Value,enemy1);
												attackCount++;
												if(attackCount >= Global.SimpleKnifeAttackCount.Value)
												{
													selfCache.DestroyGameObjGracefully();
												}
											}
										}
									}).UnRegisterWhenGameObjectDestroyed(selfCache);
									ActionKit.OnUpdate.Register(() =>
									{
										if(Player.Default.Distance2D(selfCache) > 15)
										{
											selfCache.DestroyGameObjGracefully();
										}
									}).UnRegisterWhenGameObjectDestroyed(selfCache);
								});
						}
					
					}
				}
			}
        }
    }
}
