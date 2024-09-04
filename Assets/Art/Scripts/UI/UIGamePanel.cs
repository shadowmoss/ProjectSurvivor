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
				ExpText.text = "经验值:"+exp;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// 每当等级值变化时，更新前端组件
			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "等级:" + level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// 每当经验值大于等于5，消耗5点经验值升一级
			Global.Exp.Register(exp =>
			{
				if (exp >= 5) {
					Global.Exp.Value -= 5;
					Global.Level.Value++;
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			// 每当等级变化时,时间暂停。显示屏幕中央的能力选择按钮
			Global.Level.Register(level =>
			{

				Time.timeScale = 0;

				BtnUpgrade.Show();

			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			BtnUpgrade.Hide();

			BtnUpgrade.onClick.AddListener(() => {
				Time.timeScale = 1;
				Global.SimpleAbilityDamage.Value *= 1.5f;
				BtnUpgrade.Hide();
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

			// 时间变化的执行任务
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if (Global.CurrentSeconds.Value >= 5) {
					// 坚持超过设置的时间，则游戏通关
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
