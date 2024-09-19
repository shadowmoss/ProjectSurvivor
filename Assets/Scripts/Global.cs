using ProjectSurvivor;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;

public class Global
{
    /// <summary>
    /// ����ֵ
    /// </summary>
    public static BindableProperty<int> Exp = new BindableProperty<int>(0);
    /// <summary>
    /// ���
    /// </summary>
    public static BindableProperty<int> Coin = new BindableProperty<int>(0);

    /// <summary>
    /// �ȼ�
    /// </summary>
    public static BindableProperty<int> Level = new BindableProperty<int>(1);

    /// <summary>
    /// �������������Ĺ�������ֵ
    /// </summary>
    public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);

    // ��ǰ��Ϸʱ��
    public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

    // ��ǰSimpleAbility����Ƶ��
    public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);


    // ����������
    public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.4f);
    // ��ҵ������
    public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.1f);


    // ��ֵ��ʼ��
    [RuntimeInitializeOnLoadMethod]
    public static void AutoInit() {
        // ���
        Global.Coin.Value = PlayerPrefs.GetInt("coin", 0);

        

        // ����ֵ������ʳ�ʼ��
        Global.ExpPercent.Value = PlayerPrefs.GetFloat("ExpPercent", 0.3f);
        Global.CoinPercent.Value = PlayerPrefs.GetFloat("CoinPercent", 0.1f);

        // �����ֵ�仯ʱ�洢
        Global.Coin.Register(coin =>
        {
            PlayerPrefs.SetInt("coin", coin);
        });
        // ������ֵ�������ʱ�洢
        Global.ExpPercent.Register(ExpPercent =>
        {
            PlayerPrefs.SetFloat("ExpPercent", ExpPercent);
        });
        // ����ҵ�����ʱ仯ʱ�洢
        Global.CoinPercent.Register(CoinPercent =>
        {
            PlayerPrefs.SetFloat("CoinPercent", CoinPercent);
        });
    }
    public static void ResetData() {
        Exp.Value = 0;
        Level.Value = 1;
        CurrentSeconds.Value = 0;
        SimpleAbilityDamage.Value = 0;
        EnemyGenerator.EnemyCount.Value = 0;
        SimpleAbilityDuration.Value = 1.5f;
    }

    public static int ExpToNextLevel() {
        return Global.Level.Value * 5 ;
    }

    public static void GeneratePowerUp(GameObject gameobject) {

        
        float expPercent = Random.Range(0,1f);

        // ����ֵ�����ж�
        if (expPercent < Global.ExpPercent.Value)
        {
            PowerUpManager.Default.Exp.Instantiate()
                .Position(gameobject.Position())
                .Show();
        }
        
        // �������
        float coinPercent = Random.Range(0, 1f);
        if (coinPercent < Global.CoinPercent.Value) {
            PowerUpManager.Default.Coin.Instantiate().Position(gameobject.Position()).Show();
        }
    }
}
