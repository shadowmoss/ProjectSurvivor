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
			EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
			{
				EnemyCountText.text = "敌人数量:" + enemyCount;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.CurrentSeconds.RegisterWithInitValue(currentSeconds =>
			{
				if(Time.frameCount % 30 == 0)
				{
				int currentSecondsInt = Mathf.FloorToInt(currentSeconds);
				int seconds = currentSecondsInt % 60;
				int minutes = currentSecondsInt / 60;
				TimeText.text = "时间:" + $"{minutes}:{seconds}";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			// please add init code here
			Global.Exp.RegisterWithInitValue(exp =>
			{
				ExpText.text = "Exp:"+exp;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "Level:"+level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Level.Register(lv =>
			{
				Time.timeScale = 0;
				BtnUpgrade.Show();
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			Global.Exp.RegisterWithInitValue(exp =>
			{
				if (exp >= 5)
				{
					Global.Exp.Value -= 5;
					Global.Level.Value++;
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			BtnUpgrade.Hide();

			BtnUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1.0f;
				Global.SimpleAbilityDamage.Value *= 1.5f;
				BtnUpgrade.Hide();
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
