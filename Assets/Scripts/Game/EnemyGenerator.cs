using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;
using System.Collections;

namespace ProjectSurvivor
{
    [Serializable]
    public class EnemyWave {
        public float DurationSeconds;
        public GameObject EnemyPrefab;
        public float Seconds;
    }
	public partial class EnemyGenerator : ViewController
	{
        // 用于记录当前波次生成频率和波次的持续时间
        private float mCurrentMaintainSeconds = 0;

        // 当前进行到了第几波了
        public int waveCount = 0;

       
        // 当前的敌人波次列表
        [SerializeField]
        List<EnemyWave> enemyWaves = new List<EnemyWave>(); 
        Queue<EnemyWave> enemyWaveQueue = new Queue<EnemyWave>();

        // 当前是否进行到了最后一波
        public bool LastWave => waveCount == enemyWaves.Count;

        // 向外暴露当前波次
        public EnemyWave CurrentWave => mCurrentWave;

        // 当前敌人数量
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>();

        private void Start()
        {
            // 先将配置好的EnemyWave添加到队列当中
            foreach(EnemyWave item in enemyWaves) { 
                enemyWaveQueue.Enqueue(item);
            }
        }


        private float mCurrentSeconds = 0;

        private EnemyWave mCurrentWave = null;

        private void Update()
        {
            // 当前波次记录为null
            if (mCurrentWave == null && enemyWaveQueue.Count>0) {
                mCurrentWave = enemyWaveQueue.Dequeue();
                waveCount++;
                mCurrentSeconds = 0;
                mCurrentMaintainSeconds = 0;
            }

            if(mCurrentWave != null)
            {
                mCurrentSeconds += Time.deltaTime;
                mCurrentMaintainSeconds += Time.deltaTime;

                if (mCurrentSeconds > mCurrentWave.DurationSeconds)
                {
                    mCurrentSeconds = 0;
                    var player = Player.Default;
                    if (player)
                    {
                        // 这个公式的含义需要搜索学习一下。
                        var randomAngle = UnityEngine.Random.Range(0, 360f);
                        var randomRadius = randomAngle * Mathf.Rad2Deg;
                        var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));

                        var generatePos = player.transform.position + direction * 10;

                        // 根据波次配置的敌人进行生成
                        mCurrentWave.EnemyPrefab.Instantiate()
                            .Position(generatePos)
                            .Show();
                    }
                }
                // 超过当前波次持续时间，将当前波次设置为null,
                if (mCurrentMaintainSeconds >= mCurrentWave.Seconds) {
                    mCurrentWave = null;
                    mCurrentSeconds = 0;
                    mCurrentMaintainSeconds = 0;
                }
            }
        }
    }
}
