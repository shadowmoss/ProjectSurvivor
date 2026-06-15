using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class CameraController : ViewController
	{
		private Vector2 mTargetPosition = Vector2.zero;

		private static CameraController mDefault = null;
		public static Transform LBTrans => mDefault.LB;
		public static Transform RTTrans => mDefault.RT;
		private void Awake()
		{
			mDefault = this;
		}
		private void OnDestroy()
		{
			mDefault = null;
		}

		void Start()
		{
			Application.targetFrameRate = 60;
		}
		private Vector3 mCurrentCameraPos;
		private bool mShake = false;
		private int mShakeFrame = 0;
		private float mShakeA = 0.25f;
		public static void Shake()
		{
			mDefault.mShake = true;
			mDefault.mShakeFrame = 30;
			mDefault.mShakeA = 0.25f;
		}
		void Update()
		{
			// Code Here
			if (Player.Default)
			{
				
				mTargetPosition = Player.Default.transform.position;
				mCurrentCameraPos.x = (1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.x,mTargetPosition.x);
				mCurrentCameraPos.y = (1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.y,mTargetPosition.y);
				mCurrentCameraPos.z = transform.position.z;
				if (mShake)
				{
					mShakeFrame--;
					// if(mShakeFrame % 3 == 0)
					// {
						 var shakeA = Mathf.Lerp(mShakeA,0.0f,(mShakeFrame / 30.0f));
						 transform.position = new Vector3(
						 mCurrentCameraPos.x + Random.Range(-shakeA,shakeA),
						 mCurrentCameraPos.y+Random.Range(-shakeA,shakeA),
						 mCurrentCameraPos.z);
					// }
					if(mShakeFrame <= 0)
					{
						mShake = false;
					}
				}
				else
				{
					transform.PositionX((1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.x,mTargetPosition.x));
					transform.PositionY((1.0f - Mathf.Exp(-Time.deltaTime * 20)).Lerp(transform.position.y,mTargetPosition.y));
				}
				
			}
		}
	}
}
