using UnityEngine;
using QFramework;
using System.Collections.Generic;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class BasketBallAbility : ViewController
	{
		private List<Ball> mBalls = new List<Ball>();
		void Start()
		{
			Debug.Log("Execute Balls Ability");
			Global.BasketBallCount.Or(Global.AdditionalFlyThingCount).Register(
				CreateBalls
			).UnRegisterWhenGameObjectDestroyed(gameObject);
			// Code Here
			CreateBalls();
		}
		void CreateBall()
		{
			mBalls.Add(Ball.Instantiate()
							.SyncPosition2DFrom(this)
							.Show());
		}
		void CreateBalls()
		{
			int ballCount2Create = Global.BasketBallCount.Value + Global.AdditionalFlyThingCount.Value - mBalls.Count;
			for(var i = 0;i < ballCount2Create; i++)
			{
				CreateBall();
			}
		}
	}
}
