using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace QFramework.Example
{
	public class TestUIGameStartPanelData : UIPanelData
	{
	}
	public partial class TestUIGameStartPanel : UIPanel
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
			Global.Coin.RegisterWithInitValue((coin) =>
			{
				CoinText.text = $"Coin: {coin}";
				if(coin >= 5)
				{
					BtnCoinPercentUpgrade.Show();
					BtnExpPercentUpgrade.Show();
				}
				else
				{
					BtnCoinPercentUpgrade.Hide();
					BtnExpPercentUpgrade.Hide();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			BtnCoinPercentUpgrade.onClick.AddListener(() =>
			{
				Global.CoinPercent.Value += 0.1f;
				Global.Coin.Value -= 5;
			});
			BtnExpPercentUpgrade.onClick.AddListener(() =>
			{
				Global.ExpPercent.Value += 0.1f;
				Global.Coin.Value -= 5;
			});
			BtnClose.onClick.AddListener(() =>
			{
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
