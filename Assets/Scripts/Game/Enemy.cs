using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class Enemy : ViewController
	{
		public float HP = 3;
		public float moveSpeed = 2.0f;
		void Start()
		{
			// Code Here
			EnemyGenerator.EnemyCount.Value++;
		}
        void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
        void Update()
        {
			// 每个Enemy向着Player移动的逻辑
			if (Player.Default)
			{
				var direction = (Player.Default.transform.position - this.transform.position).normalized;
				this.transform.Translate(direction *moveSpeed* Time.deltaTime);
			}

			if(HP <= 0)
			{
				this.DestroyGameObjGracefully();
				Global.Exp.Value++;
			}
        }
    }
}
