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
			// HP
			Global.HP.RegisterWithInitValue(hp =>
			{
				HPText.text = "HP:" + hp + "/" + Global.MaxHP.Value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			// MaxHP
			Global.MaxHP.RegisterWithInitValue(hp =>
			{
				HPText.text = "HP:" +Global.HP.Value+ "/" + Global.MaxHP.Value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

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
				ExpText.text = "Exp:"+"("+exp+"/"+Global.ExpToNextLevel()+")";
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

			Global.Level.Register(lv =>
			{
				Time.timeScale = 0;
				UpgrateRoot.Show();
				AudioKit.PlaySound("Level_up");
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			UpgrateRoot.Hide();

			BtnSimpleDurationUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				Global.SimpleAbilityDuration.Value *= 0.8f;
				UpgrateRoot.Hide();
			});

			BtnUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				Global.SimpleAbilityDamage.Value *= 1.5f;
				UpgrateRoot.Hide();
			});

			var enemyGenerator = FindObjectOfType<EnemyGenerator>();
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if( enemyGenerator.LastWave && enemyGenerator.CurrentWave == null && EnemyGenerator.EnemyCount.Value == 0)
				{
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
