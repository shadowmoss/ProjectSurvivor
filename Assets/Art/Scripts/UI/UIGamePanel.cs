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


			// ÿ������ֵ�仯ʱ�������
			Global.Exp.RegisterWithInitValue(exp => {
				ExpText.text = "����ֵ:"+exp;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// ÿ���ȼ�ֵ�仯ʱ������ǰ�����
			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "�ȼ�:" + level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// ÿ������ֵ���ڵ���5������5�㾭��ֵ��һ��
			Global.Exp.Register(exp =>
			{
				if (exp >= 5) {
					Global.Exp.Value -= 5;
					Global.Level.Value++;
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			// ÿ���ȼ��仯ʱ,ʱ����ͣ����ʾ��Ļ���������ѡ��ť
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


			//	ʱ��ֵ�¼�
			Global.CurrentSeconds.RegisterWithInitValue(currentTime =>
			{
				if (Time.frameCount % 30 == 0) {
					var currentSecondsInt = Mathf.FloorToInt(currentTime);
					var seconds = currentSecondsInt % 60;
					var minutes = currentSecondsInt / 60;
					TimeText.text = "ʱ��" + $"{minutes:00}:{seconds:00}";
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			// ʱ��仯��ִ������
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if (Global.CurrentSeconds.Value >= 5) {
					// ��ֳ������õ�ʱ�䣬����Ϸͨ��
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
