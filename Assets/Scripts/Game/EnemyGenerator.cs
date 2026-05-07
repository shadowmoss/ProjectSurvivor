using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class EnemyGenerator : ViewController
	{
		private float mCurrentSeconds = 0;
		void Start()
		{
			// Code Here
		}

        void Update()
        {
            mCurrentSeconds += Time.deltaTime;
			if(mCurrentSeconds >= 1)
			{
				mCurrentSeconds = 0;

				var player = Player.Default;
				if (player)
				{
					var randomAngle = Random.Range(0,360f);
					var randomRadius = randomAngle * Mathf.Deg2Rad;
					// 这里Mathf返回360度角的cosx,sinx,相当于一个半径为1的弧度为x的x坐标和y坐标
					var direction = new Vector3(Mathf.Cos(randomRadius),Mathf.Sin(randomRadius));
					var generatePos = player.transform.position + direction * 10;

					Enemy.Instantiate().Position(generatePos).Show();
				}
			}
        }

    }
}
