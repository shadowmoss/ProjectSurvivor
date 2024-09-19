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
				ExpText.text = "����ֵ:"+"("+exp+"/"+Global.ExpToNextLevel()+")";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// ÿ���ȼ�ֵ�仯ʱ������ǰ�����
			Global.Level.RegisterWithInitValue(level =>
			{
				LevelText.text = "�ȼ�:" + level;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);



			// ����ֵ�㹻���������¼���
			Global.Exp.Register(exp =>
			{
				if (exp >= Global.ExpToNextLevel()) {
					Global.Exp.Value -= Global.ExpToNextLevel();
					Global.Level.Value++;
				}

			}).UnRegisterWhenGameObjectDestroyed(gameObject);


			// ÿ���ȼ��仯ʱ,ʱ����ͣ����ʾ��Ļ���������ѡ��ť
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

            EnemyGenerator enemyGenerator =  FindObjectOfType<EnemyGenerator>();

			// ʱ��仯��ִ������
			ActionKit.OnUpdate.Register(() =>
			{
				Global.CurrentSeconds.Value += Time.deltaTime;
				if (enemyGenerator.LastWave && EnemyGenerator.EnemyCount.Value==0) {
					// ��ֳ������õ�ʱ�䣬����Ϸͨ��
					UIKit.OpenPanel<UIGamePassPanel>();
				}
			}).UnRegisterWhenGameObjectDestroyed(gameObject);

			// ��������
			EnemyGenerator.EnemyCount.RegisterWithInitValue(currentEnemyCount =>
			{
				EnemyCountText.text = "����" + $"{currentEnemyCount}";
			}).UnRegisterWhenGameObjectDestroyed(gameObject);


            // ���
            //Global.Coin.Value = PlayerPrefs.GetInt("coin",0);
			
			Global.Coin.RegisterWithInitValue(coin => {
				//PlayerPrefs.SetInt("coin",coin);
				CoinText.text = "���:" + coin;
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
