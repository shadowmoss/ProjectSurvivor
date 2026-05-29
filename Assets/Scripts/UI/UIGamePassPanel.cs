using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
	public class UIGamePassPanelData : UIPanelData
	{
	}
	public partial class UIGamePassPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePassPanelData ?? new UIGamePassPanelData();
			Time.timeScale = 0;
			AudioKit.PlaySound("Game_pass");
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					Global.ResetData();
					this.CloseSelf();
					SceneManager.LoadScene("Game");
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnBackToStart.onClick.AddListener(() =>
			{
				Global.ResetData();
				this.CloseSelf();
				SceneManager.LoadScene("GameStart");
				
			});
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
			Time.timeScale = 1;
		}
	}
}
