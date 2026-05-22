using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	
	public partial class SimpleAbility : ViewController
	{
		private float mCurrentSeconds = 0;
		void Start()
		{
			// Code Here
		}
        void Update()
        {
            mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds >= 1.5f)
			{
				mCurrentSeconds = 0;
				// 找到当前场景中的Enemy脚本
				var enemies =  FindObjectsByType<Enemy>(FindObjectsInactive.Exclude,FindObjectsSortMode.None);

				// 计算每个找到的Enemy脚本的gameObject距离当前Player的距离
				foreach(var enemy in enemies)
				{
					var distance = (Player.Default.transform.position - enemy.transform.position).magnitude;
					
					if(distance <= 5)
					{
						enemy.Sprite.color = Color.red;
						// 这里缓存一次多一个引用的原因我不太清楚。
						var enemyRefCache = enemy;
						ActionKit.Delay(0.3f, () =>
						{
							enemyRefCache.HP-= Global.SimpleAbilityDamage.Value;
							enemyRefCache.Sprite.color = Color.white;
						}).StartGlobal();
					}
				}
			}
        }
    }
}
