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
			Global.BasketBallCount.RegisterWithInitValue(count =>
			{
				Debug.Log("当前应生成的Ball的数量"+count);
				if(count > mBalls.Count)
				{
					Debug.Log("Add new Ball");
					mBalls.Add(Ball.Instantiate()
							.SyncPosition2DFrom(this)
							.Show());
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			// Code Here
			
		}
		
	}
}
