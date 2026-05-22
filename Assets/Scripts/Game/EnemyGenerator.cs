using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	[Serializable]
	public class EnemyWave
	{
		public float GenerateDuration = 1;
		public GameObject EnemyPrefab;
		public int seconds = 10;
	}
	public partial class EnemyGenerator : ViewController
	{
		private float mCurrentGenerateSeconds = 0;
		public float mCurrentWaveSeconds = 0;
		public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);
		[SerializeField]
		public List<EnemyWave> enemyWaves = new List<EnemyWave>();
		public Queue<EnemyWave> mEnemyWavesQueue = new Queue<EnemyWave>();
		private EnemyWave mCurrentWave = null;
		public int WaveCount = 0;
		public bool LastWave => WaveCount == enemyWaves.Count;
		public EnemyWave CurrentWave => mCurrentWave;
		void Start()
		{
			// Code Here
			foreach(EnemyWave item in enemyWaves)
			{
				mEnemyWavesQueue.Enqueue(item);
			}
		}

        void Update()
        {
            // mCurrentGenerateSeconds += Time.deltaTime;
			if (mCurrentWave == null)
			{
				if (mEnemyWavesQueue.Count > 0)
				{
					WaveCount++;
					mCurrentGenerateSeconds = 0;
					mCurrentWaveSeconds = 0;
					mCurrentWave = mEnemyWavesQueue.Dequeue();
				}
			}
			if(mCurrentWave != null)
			{
				mCurrentGenerateSeconds += Time.deltaTime;
				mCurrentWaveSeconds += Time.deltaTime;

				if(mCurrentGenerateSeconds >= mCurrentWave.GenerateDuration)
				{
						mCurrentGenerateSeconds = 0;
						var player = Player.Default;
						if (player)
						{
							var randomAngle = UnityEngine.Random.Range(0,360f);
							var randomRadius = randomAngle * Mathf.Deg2Rad;
							// 这里Mathf返回360度角的cosx,sinx,相当于一个半径为1的弧度为x的x坐标和y坐标
							var direction = new Vector3(Mathf.Cos(randomRadius),Mathf.Sin(randomRadius));
							var generatePos = player.transform.position + direction * 10;

							mCurrentWave.EnemyPrefab.Instantiate().Position(generatePos).Show();
						}
				}
				if(mCurrentWaveSeconds >= mCurrentWave.seconds)
				{
					mCurrentWave = null;
				}	
			}
			
        }

    }
}
