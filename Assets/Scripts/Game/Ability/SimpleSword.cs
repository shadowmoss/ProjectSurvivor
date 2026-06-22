using UnityEngine;
using QFramework;
using System.Linq;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	
	public partial class SimpleSword : ViewController
	{
		private float mCurrentSeconds = 0;
		void Start()
		{
			// Code Here
		}
        void Update()
        {
            mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= Global.SimpleAbilityDuration.Value)
			{
				mCurrentSeconds = 0;
				// 找到当前场景中的Enemy脚本
				var enemies =  FindObjectsByType<Enemy>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);
				var enemyMiniBosses	= FindObjectsByType<EnemyMiniBoss>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);

				// 计算每个找到的Enemy脚本的gameObject距离当前Player的距离
				foreach(var enemy in enemies
				.OrderBy(e=>e.Direction2DFrom(Player.Default).magnitude)
				.Where(e=>e.Direction2DFrom(Player.Default).magnitude < Global.SimpleSwordRange.Value)
				.Take(Global.SimpleSwordCount.Value))
				{
					var distance = Player.Default.Direction2DFrom(enemy).magnitude;
					// (Player.Default.transform.position - enemy.transform.position).magnitude;
					
					if(distance <= 5)
					{
						// enemy.Hurt(Global.SimpleAbilityDamage.Value);
						Sword.Instantiate()
							.Position(enemy.Position()+Vector3.left * 0.25f)
							.Show()
							.Self(self =>
							{
								var selfCache = self;
								selfCache.OnTriggerEnter2DEvent(Collider2D =>
								{
									var hurtBox = Collider2D.GetComponent<HurtBox>();
									if (hurtBox)
									{
										if (hurtBox.Owner.CompareTag("Enemy"))
										{
											enemy.Hurt(Global.SimpleAbilityDamage.Value);
										}
									}
								}).UnRegisterWhenGameObjectDestroyed(selfCache);

								// 劈砍动画 说是可以通过Unity Animator完成这个动画
								ActionKit
									.Sequence()
									.Callback(() =>
									{
										selfCache.enabled = false;
									})
									.Parallel(p =>
									{
										p.Lerp(0, 10, 0.2f, (z) =>
										{
											// self.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
											selfCache.LocalEulerAnglesZ(z);
										});
										p.Append(
											ActionKit.Sequence()
											.Lerp(0,1.25f,0.1f,scale=>selfCache.LocalScale(scale))
											.Lerp(1.25f,1,0.1f,scale=>selfCache.LocalScale(scale))
										);
									})
									.Callback(() =>
									{
										selfCache.enabled = true;
									})
									.Parallel(p =>
									{
										p.Lerp(10, -180, 0.2f, z =>
										{
											// selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
											selfCache.LocalEulerAnglesZ(z);
										});
										p.Append(ActionKit.Sequence()
											.Lerp(1,1.25f,0.1f,scale=>selfCache.LocalScale(scale))
											.Lerp(1.25f,1f,0.1f,scale=>selfCache.LocalScale(scale))
										);
									})
									.Callback(()=>{selfCache.enabled = false;})
									.Lerp(-180, 0, 0.3f, z =>
									{
										selfCache.LocalEulerAnglesZ(z)
												.LocalScale(z.Abs() / 180);
										// selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
										// selfCache.LocalScale(z.Abs()/180);
									})
									.Start(this, () =>
									{
										selfCache.DestroyGameObjGracefully();
									});

							});
					}
				}
				foreach(var boss in enemyMiniBosses)
				{
					var distance = (Player.Default.transform.position - boss.transform.position).magnitude;
					
					if(distance <= 5)
					{
						boss.Hurt(Global.SimpleAbilityDamage.Value);
					}
				}
			}
        }
    }
}
