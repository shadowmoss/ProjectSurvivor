using UnityEngine;
using QFramework;
using System.Linq;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class AbilitiesController : ViewController,IController
	{
		void Start()
		{
			// Code Here
			Global.SimpleSwordUnlocked.RegisterWithInitValue(unlock=>
			{
				if (unlock)
				{
					SimpleSword.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(this);
			Global.SimpleKnifeUnlocked.RegisterWithInitValue(unlock =>
			{
				if (unlock)
				{
					SimpleKnife.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(this);
			Global.RotateSwordUnlocked.RegisterWithInitValue(unlock =>
			{
				if (unlock)
				{
					RotateSword.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(this);
			Global.BasketBallUnlocked.RegisterWithInitValue(unlock =>
			{
				if (unlock)
				{
					BasketBallAbility.Show();
				}
			}).UnRegisterWhenGameObjectDestroyed(this);
			// Randomly pick a upgradeItem to upgrade
            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
            ExpUpgradeItem expUpgradeItem = expUpgradeSystem
			.Items
			.Where(item=>item.IsWeapon)
			.ToList()
			.GetRandomItem();
			expUpgradeItem.Upgrade();
		}
		public IArchitecture GetArchitecture()
		{
			return Global.Interface;
		}
	}
}
