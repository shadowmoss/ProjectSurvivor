using System.Collections;
using System.Collections.Generic;
using ProjectSurvior;
using QFramework;
using QFramework.Example;
using Unity.VisualScripting;
using UnityEngine;
namespace QFramework.Example
{
    public class Global : Architecture<Global>
    {
        public static BindableProperty<int> HP = new BindableProperty<int>(3);
        public static BindableProperty<int> MaxHP = new BindableProperty<int>(3);
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(Config.InitSimpleSwordDamage);
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(Config.InitSimpleSwordDuration);
        public static BindableProperty<int> SimpleSwordCount = new BindableProperty<int>(Config.InitSimpleSwordCount);
        public static BindableProperty<float> SimpleSwordRange = new BindableProperty<float>(Config.InitSimpleSwordRange);

        public static BindableProperty<float> SimpleKnifeDamage = new BindableProperty<float>(Config.InitSimpleKnifeDamage);
        public static BindableProperty<float> SimpleKnifeDuration = new BindableProperty<float>(Config.InitSimpleKnifeDuration);
        public static BindableProperty<int> SimpleKnifeCount = new BindableProperty<int>(Config.InitSimpleKnifeCount);
        public static BindableProperty<int> SimpleKnifeAttackCount = new BindableProperty<int>(1);

        public static BindableProperty<float> RotateSwordDamage = new(Config.InitRotateSwordDamage);
        public static BindableProperty<int> RotateSwordCount = new(Config.InitRotateSwordCount);
        public static BindableProperty<float> RotateSwordSpeed = new(Config.InitRotateSwordSpeed);
        public static BindableProperty<float> RotateSwordRange = new(Config.InitRotateSwordRange);

        public static BindableProperty<float> BasketBallDamage = new(Config.InitBasketBallDamage);
        public static BindableProperty<float> BasketBallSpeed = new(Config.InitBasketBallSpeed);
        public static BindableProperty<float> BasketBallCount = new(Config.InitBasketBallCount);

        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.3f);
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);


        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            ResKit.Init();
            UIKit.Root.SetResolution(1920,1080,1);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP),3);
            HP.Value = MaxHP.Value;

            Global.Coin.Value = PlayerPrefs.GetInt("coin",0);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent),0.4f);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent),0.1f);
            Global.Coin.Register(coin =>
            {
            PlayerPrefs.SetInt(nameof(coin),coin); 
            });
            Global.ExpPercent.Register(expPercent =>
            {
            PlayerPrefs.SetFloat(nameof(ExpPercent),expPercent); 
            });
            Global.CoinPercent.Register(coinPercent =>
            {
            PlayerPrefs.SetFloat(nameof(CoinPercent),coinPercent); 
            });
            Global.MaxHP.Register(maxHP =>
            {
            PlayerPrefs.SetInt(nameof(MaxHP),maxHP); 
            });
        }

        public static void ResetData()
        {
            HP.Value = MaxHP.Value;
            // Coin.Value = 0;
            Exp.Value = 0;
            Level.Value = 1;
            CurrentSeconds.Value = 0;
            SimpleAbilityDamage.Value = Config.InitSimpleSwordDamage;
            SimpleAbilityDuration.Value = Config.InitSimpleSwordDuration;
            SimpleSwordRange.Value = Config.InitSimpleSwordRange;
            SimpleSwordCount.Value = Config.InitSimpleSwordCount;

            SimpleKnifeDamage.Value = Config.InitSimpleKnifeDamage;
            SimpleKnifeCount.Value = Config.InitSimpleKnifeCount;
            SimpleKnifeDuration.Value = Config.InitSimpleKnifeDuration;
            SimpleKnifeAttackCount.Value = Config.InitSimpleKnifeAttackCount;

            RotateSwordDamage.Value = Config.InitRotateSwordDamage;
            RotateSwordCount.Value = Config.InitRotateSwordCount;
            RotateSwordSpeed.Value = Config.InitRotateSwordSpeed;
            RotateSwordRange.Value = Config.InitRotateSwordRange;
            
            // EnemyGenerator.EnemyCount.Value = 0;
            Interface.GetSystem<ExpUpgradeSystem>().ResetData();
        }
        public static int ExpToNextLevel()
        {
            return Level.Value * 5;
        }
        public static void GeneratePowerUp(GameObject gameObject)
        {
            float expPercent = Random.Range(0, 1f);

            // Debug.Log($"ExpPercent:{expPercent} Global.ExpPercent.Value:{Global.ExpPercent.Value}}}");
            if (expPercent < Global.ExpPercent.Value)
            {
                PowerUpManger.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                    return;
            }

            float coinPercent = Random.Range(0,1f);
            // Debug.Log($"CoinPercent:{coinPercent} Global.CoinPercent.Value:{Global.CoinPercent.Value}");
            if(coinPercent < Global.CoinPercent.Value)
            {
                PowerUpManger.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                    return;
            }
            float hpPercent = Random.Range(0,1f);
            if(hpPercent < 0.3f)
            {
                PowerUpManger.Default.HP.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                    return;
            }
            float bombPercent = Random.Range(0,1f);
            if(bombPercent < 0.3f)
            {
                PowerUpManger.Default.Bomb.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                    return;
            }
            float getAllPercent = Random.Range(0,1f);
            if(getAllPercent < 0.3f)
            {
                PowerUpManger.Default.GetAllExp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
                    return;
            }
        }

        protected override void Init()
        {
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
        }
    }

}
