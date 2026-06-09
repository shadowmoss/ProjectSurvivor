using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class CircleAbility : ViewController
	{
		void Start()
		{
			// Code Here
			Circle.OnTriggerEnter2DEvent(collider =>
			{
                HurtBox hurtBox = collider.GetComponent<HurtBox>();
				if (hurtBox != null)
				{
					if (hurtBox.Owner.CompareTag("Enemy"))
					{
						hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
					}
				}
			});
		}
		int radius = 3;
		public void Update()
		{
			int degree = Time.frameCount;
			Vector2 circleLocalPos = new Vector2(Mathf.Cos(degree * Mathf.Deg2Rad),Mathf.Sin(degree * Mathf.Deg2Rad)) * radius;
			Circle.LocalPosition(circleLocalPos.x,circleLocalPos.y);
		}
	}
}
