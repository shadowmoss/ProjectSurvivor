using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class Bomb : ViewController
	{
		void Start()
		{
			// Code Here
		}
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (!other.GetComponent<CollectableArea>())
			{
				return;
			}
			foreach(var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
			{
				 var enemy = enemyObj.GetComponent<Enemy>();
				 if(enemy && enemy.gameObject.activeSelf)
				{
					enemy.Hurt(enemy.HP);
				}
			}
			AudioKit.PlaySound("bomb");
			CameraController.Shake();
			UIGamePanel.FlashScreen.Trigger();
			this.DestroyGameObjGracefully();
		}
	}
}
