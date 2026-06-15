using UnityEngine;
using QFramework;
using System;
using System.Collections.Generic;
using ProjectSurvior;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	
	public partial class EnemyGenerator : ViewController
	{
		[SerializeField]
		public LevelConfig config;
		private float mCurrentGenerateSeconds = 0;
		public float mCurrentWaveSeconds = 0;
		public static BindableProperty<int> EnemyCount = new BindableProperty<int>(0);
		public Queue<EnemyWave> mEnemyWavesQueue = new Queue<EnemyWave>();
		private EnemyWave mCurrentWave = null;
		public int WaveCount = 0;
		private int mTotalCount = 0;
		public bool LastWave => WaveCount == mTotalCount;
		public EnemyWave CurrentWave => mCurrentWave;
		void Start()
		{
			// Code Here
			foreach (var group in config.EnemyWaveGroups)
			{
				foreach(var wave in group.Waves)
				{
					mEnemyWavesQueue.Enqueue(wave);
					mTotalCount++;
				}
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

							var xOry = RandomUtility.Choose(-1,1);
							var pos = Vector2.zero;
							if(xOry == -1)
							{
								pos.x = RandomUtility.Choose(CameraController.LBTrans.position.x,
								CameraController.RTTrans.position.x);
								pos.y = UnityEngine.Random.Range(CameraController.LBTrans.position.y,
								CameraController.RTTrans.position.y);
							}
							else
							{
								pos.x = UnityEngine.Random.Range(CameraController.LBTrans.position.x,CameraController.RTTrans.position.x);
								pos.y = RandomUtility.Choose(CameraController.LBTrans.position.y,CameraController.RTTrans.position.y);
							}

							// var randomAngle = UnityEngine.Random.Range(0,360f);
							// var randomRadius = randomAngle * Mathf.Deg2Rad;
							// // 这里Mathf返回360度角的cosx,sinx,相当于一个半径为1的弧度为x的x坐标和y坐标
							// var direction = new Vector3(Mathf.Cos(randomRadius),Mathf.Sin(randomRadius));
							// var generatePos = player.transform.position + direction * 10;

							mCurrentWave.EnemyPrefab
								.Instantiate()
								.Position(pos)
								.Self(self =>
								{
									var enemy = self.GetComponent<IEnemy>();
									enemy.SetSpeedScale(mCurrentWave.speedScale);

									enemy.SetHpScale(mCurrentWave.HPScale);
								})
								.Show();
						}
				}
				if(mCurrentWaveSeconds >= mCurrentWave.seconds)
				{
					mCurrentWave = null;
				}	
			}
			
        }
		void OnDestroy()
		{
			// WaveCount = 0;
		}

    }
}
