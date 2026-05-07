using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public class UIGamePanelData : UIPanelData
	{
	}
	public partial class UIGamePanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			// please add init code here
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "Exp:"+exp;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "Level:"+level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.Exp.RegisterWithInitValue(exp =>
			{
				if (exp >= 5)
				{
					Global.Exp.Value -= 5;
					Global.Level.Value++;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
		
		protected override void OnOpen(IUIData uiData = null)
		{
		}
		
		protected override void OnShow()
		{
		}
		
		protected override void OnHide()
		{
		}
		
		protected override void OnClose()
		{
		}
	}
}
