using UnityEngine;
using QFramework;
using System.Collections.Generic;
using ProjectSurvior;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class RotateSword : ViewController
	{
		private List<Collider2D> mSwords = new List<Collider2D>();
		void Start()
		{
			// Code Here
			Sword.Hide();
			Global.RotateSwordCount.Or(Global.AdditionalFlyThingCount)
			.Register(CreateSword)
			.UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.RotateSwordRange.Register((range) =>
			{
				UpdateCirclePos();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			CreateSwords();
		}
		void CreateSword()
		{
			mSwords.Add(
						Sword.InstantiateWithParent(this)
							.Self(self =>
							{
								self.OnTriggerEnter2DEvent(collider =>
								{
									Debug.Log("当前有碰撞产生");
									HurtBox hurtBox = collider.GetComponent<HurtBox>();
									if (hurtBox != null)
									{
										if (hurtBox.Owner.CompareTag("Enemy"))
										{
											var enemy =  hurtBox.Owner.GetComponent<Enemy>();
											// hurtBox.Owner.GetComponent<Enemy>().Hurt(Global.RotateSwordDamage.Value);
											DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value,enemy);
											// 定义击退效果,从剑到敌人的方向，击退敌人的距离等于，敌人到Player的距离
											if(Random.Range(0,1.0f) < 0.5f)
											{
												collider.attachedRigidbody.velocity = 
													collider.NormalizedDirection2DFrom(self) * 5 +
													collider.NormalizedDirection2DFrom(Player.Default) * 10;
											}
										}
									}
								}).UnRegisterWhenGameObjectDestroyed(this);
							})					
					.Show());
		}
		void CreateSwords()
		{
			int toAddCount = Global.RotateSwordCount.Value + Global.AdditionalFlyThingCount.Value - mSwords.Count;
			for(var i = 0;i < toAddCount; i++)
			{
				CreateSword();
			}
			UpdateCirclePos();
		}
		void UpdateCirclePos()
		{
			var radius = Global.RotateSwordRange.Value;
			var durationDegress = 360 / mSwords.Count;
			for(var i=0;i < mSwords.Count; i++)
			{
				var circleLocalPos = new Vector2(Mathf.Cos(durationDegress * i * Mathf.Deg2Rad),Mathf.Sin(durationDegress * i * Mathf.Deg2Rad)) * radius;
				
				mSwords[i].LocalPosition(circleLocalPos.x,circleLocalPos.y).LocalEulerAnglesZ(durationDegress * i -90);
			}
		}
		public void Update()
		{
			int degree = Time.frameCount;
			this.LocalEulerAnglesZ(-degree * Global.RotateSwordSpeed.Value);
		}
	}
}
