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
		public static EasyEvent FlashScreen = new EasyEvent();
		public static EasyEvent OpenTreasurePanel = new EasyEvent();
		protected override void OnInit(IUIData uiData = null)
		{
			mData = uiData as UIGamePanelData ?? new UIGamePanelData();
			Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(Global.MaxHP),3);
			// Enemy Count
			EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
			{
				EnemyCountText.text = "Enemy Count:" + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			// Time
			Global.CurrentSeconds.RegisterWithInitValue(currentSeconds =>
			{
				if(Time.frameCount % 30 == 0)
				{
				int currentSecondsInt = Mathf.FloorToInt(currentSeconds);
				int seconds = currentSecondsInt % 60;
				int minutes = currentSecondsInt / 60;
				TimeText.text = "time:" + $"{minutes}:{seconds}";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			// please add init code here
			// Exp
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpValue.fillAmount = exp / (float) Global.ExpToNextLevel();
				// ExpText.text = "Exp:"+"("+exp+"/"+Global.ExpToNextLevel()+")";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Exp.RegisterWithInitValue(exp =>
			{
				if (exp >= Global.ExpToNextLevel())
				{
					Global.Exp.Value -= Global.ExpToNextLevel();
					Global.Level.Value++;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			// Level
			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "Level:"+level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			ExpUpgradePanel.Hide();
			Global.Level.Register(lv =>
			{
				Time.timeScale = 0;
				// UpgrateRoot.Show();
				ExpUpgradePanel.Show();
				AudioKit.PlaySound("Level_up");
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			

			var enemyGenerator = FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if( enemyGenerator.LastWave && enemyGenerator.CurrentWave == null && EnemyGenerator.EnemyCount.Value == 0)
				{
					this.CloseSelf();
					Debug.Log("Game Pass");
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			
			Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin),0);
			Global.Coin.RegisterWithInitValue(coin =>
			{
				PlayerPrefs.SetInt(nameof(Coin),coin);
				CoinText.text = "Coin:" + coin;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			FlashScreen.Register(
				() =>
				{
					ActionKit
						.Sequence()
						.Lerp(0,0.5f,0.1f,alpha=>ScreenColor.ColorAlpha(alpha))
						.Lerp(0.5f,0,0.2f,alpha =>ScreenColor.ColorAlpha(alpha),
						()=>ScreenColor.ColorAlpha(0))
						.Start(this);
				}
			).UnRegisterWhenGameObjectDestroyed(this);
			OpenTreasurePanel.Register(() =>
			{
				Time.timeScale = 0f;
				TreasureChestPanel.Show();
			}).UnRegisterWhenGameObjectDestroyed(this);
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
