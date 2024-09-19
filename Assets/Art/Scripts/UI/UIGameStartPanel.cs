using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
	public class UIGameStartPanelData : UIPanelData
	{
	}
	public partial class UIGameStartPanel : UIPanel
	{
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
			// please add init code here
			Time.timeScale = 1.0f;
			// 点击开始游戏按钮
			BtnStartGame.onClick.AddListener(() =>
			{
				Global.ResetData();
				this.CloseSelf();
				SceneManager.LoadScene("Game");
			});

			BtnCoinUpgrade.onClick.AddListener(() => {
				CoinUpgradePanel.Show();
			});
			Global.Coin.RegisterWithInitValue(coin =>
			{
				CoinText.text = "金币:" + coin;
				if (coin >= 5)
				{
					BtnCoinPercentUpgrade.Show();
					BtnExpPercentUpgrade.Show();
				}
				else {
                    BtnCoinPercentUpgrade.Hide();
                    BtnExpPercentUpgrade.Hide();
                }
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnCoinPercentUpgrade.onClick.AddListener(() =>
			{
				Global.CoinPercent.Value+=0.1f;
				Global.Coin.Value -= 5;
			});

			BtnExpPercentUpgrade.onClick.AddListener(() =>
			{

				Global.ExpPercent.Value+=0.1f;
				Global.Coin.Value -= 5;
            });

			BtnClose.onClick.AddListener(() => {
				CoinUpgradePanel.Hide();
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
		}
	}
}
