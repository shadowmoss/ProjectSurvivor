using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
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


			// 每当经验值变化时更新组件
			Global.Exp.RegisterWithInitValue(exp => {
				ExpText.text = "经验值:"+"("+exp+"/"+Global.ExpToNextLevel()+")";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// 每当等级值变化时，更新前端组件
			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "等级:" + level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// 经验值足够触发升级事件。
			Global.Exp.Register(exp =>
			{
				if (exp >= Global.ExpToNextLevel()) {
					Global.Exp.Value -= Global.ExpToNextLevel();
					Global.Level.Value++;
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			// 每当等级变化时,时间暂停。显示屏幕中央的能力选择按钮
			Global.Level.Register(level =>
			{

				Time.timeScale = 0;

				UpgradeRoot.Show();

			}).UnRegisterWhenGameObjectDestroyed(gameObject);

            UpgradeRoot.Hide();

			BtnUpgrade.onClick.AddListener(() => {
				Time.timeScale = 1;
				Global.SimpleAbilityDamage.Value *= 1.5f;
                UpgradeRoot.Hide();
			});

			SimpleDurationUpgrade.onClick.AddListener(() =>
			{
				Time.timeScale = 1;
				Global.SimpleAbilityDuration.Value *= 0.8f;
				UpgradeRoot.Hide();
			});


			//	时间值事件
			Global.CurrentSeconds.RegisterWithInitValue(currentTime =>
			{
				if (Time.frameCount % 30 == 0) {
					var currentSecondsInt = Mathf.FloorToInt(currentTime);
					var seconds = currentSecondsInt % 60;
					var minutes = currentSecondsInt / 60;
					TimeText.text = "时间" + $"{minutes:00}:{seconds:00}";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

            EnemyGenerator enemyGenerator =  FindObjectOfType<EnemyGenerator>();

			// 时间变化的执行任务
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if (enemyGenerator.LastWave && EnemyGenerator.EnemyCount.Value==0) {
					// 坚持超过设置的时间，则游戏通关
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			// 敌人数量
			EnemyGenerator.EnemyCount.RegisterWithInitValue(currentEnemyCount =>
			{
				EnemyCountText.text = "敌人" + $"{currentEnemyCount}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


            // 金币
            //Global.Coin.Value = PlayerPrefs.GetInt("coin",0);
			
			Global.Coin.RegisterWithInitValue(coin => {
				//PlayerPrefs.SetInt("coin",coin);
				CoinText.text = "金币:" + coin;
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
