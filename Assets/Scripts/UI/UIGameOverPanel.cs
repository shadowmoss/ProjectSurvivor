using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
	public class UIGameOverPanelData : UIPanelData
	{
	}
	public partial class UIGameOverPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameOverPanelData ?? new UIGameOverPanelData();
			Time.timeScale = 0;
			// please add init code here
			ActionKit.OnUpdate.Register(() =>
			{
				if (Input.GetKeyDown(KeyCode.Space))
				{
					
					this.CloseSelf();
					SceneManager.LoadScene("Game");
					Global.ResetData();
					
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
