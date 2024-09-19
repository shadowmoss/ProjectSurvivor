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
        // ���ڼ�¼��ǰ��������Ƶ�ʺͲ��εĳ���ʱ��
        private float mCurrentMaintainSeconds = 0;

        // ��ǰ���е��˵ڼ�����
        public int waveCount = 0;

       
        // ��ǰ�ĵ��˲����б�
        [SerializeField]
        List<EnemyWave> enemyWaves = new List<EnemyWave>(); 
        Queue<EnemyWave> enemyWaveQueue = new Queue<EnemyWave>();

        // ��ǰ�Ƿ���е������һ��
        public bool LastWave => waveCount == enemyWaves.Count;

        // ���Ⱪ¶��ǰ����
        public EnemyWave CurrentWave => mCurrentWave;

        // ��ǰ��������
        public static BindableProperty<int> EnemyCount = new BindableProperty<int>();

        private void Start()
        {
            // �Ƚ����úõ�EnemyWave��ӵ����е���
            foreach(EnemyWave item in enemyWaves) { 
                enemyWaveQueue.Enqueue(item);
            }
        }


        private float mCurrentSeconds = 0;

        private EnemyWave mCurrentWave = null;

        private void Update()
        {
            // ��ǰ���μ�¼Ϊnull
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
                        // �����ʽ�ĺ�����Ҫ����ѧϰһ�¡�
                        var randomAngle = UnityEngine.Random.Range(0, 360f);
                        var randomRadius = randomAngle * Mathf.Rad2Deg;
                        var direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));

                        var generatePos = player.transform.position + direction * 10;

                        // ���ݲ������õĵ��˽�������
                        mCurrentWave.EnemyPrefab.Instantiate()
                            .Position(generatePos)
                            .Show();
                    }
                }
                // ������ǰ���γ���ʱ�䣬����ǰ��������Ϊnull,
                if (mCurrentMaintainSeconds >= mCurrentWave.Seconds) {
                    mCurrentWave = null;
                    mCurrentSeconds = 0;
                    mCurrentMaintainSeconds = 0;
                }
            }
        }
    }
}
