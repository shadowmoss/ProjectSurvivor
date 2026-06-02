using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
	public class TestUIGameStartPanelData : UIPanelData
	{
	}
	public partial class TestUIGameStartPanel : UIPanel,IController
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as TestUIGameStartPanelData ?? new TestUIGameStartPanelData();

			// Time.timeScale = 0.0f;

			BtnStartGame.onClick.AddListener(() =>
			{
				this.CloseSelf();
				// Time.timeScale = 1.0f;

				SceneManager.LoadScene("Game");
			
			});

			// please add init code here
			BtnCoinUpgrade.onClick.AddListener(() =>
			{
				CoinUpgradePanel.Show();
			});
			
			this.GetSystem<CoinUpgradeSystem>().Say();
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

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}
