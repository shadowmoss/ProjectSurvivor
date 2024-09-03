using UnityEngine;
using QFramework;
using System.Runtime.CompilerServices;

namespace ProjectSurvivor
{
	public partial class Player : ViewController
	{
		public float MovementSpeed = 5;

		public static Player Default = null;
        private void Awake()
        {
            Default = this;
        }
        private void OnDestroy()
        {
            Default = null;
        }
        void Start()
		{
			"HelloWorld!".LogInfo();
			HurtBox.OnTriggerEnter2DEvent(collider2D => {
				//ResKit.Init();
				this.DestroyGameObjGracefully();
                UIKit.OpenPanel<UIGameOverPanel>();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
		}

        private void Update()
        {
			var horizontal =  Input.GetAxis("Horizontal");	// 1	无论是水平还是垂直方向，检测到的输入值都是1，在同时按下时会变为根号2的向量。
			var vertical =  Input.GetAxis("Vertical");		// 1

			// 速度是根号二,进过单位化之后为1,这里是为了取得输入所指示的方向向量

			var direction = new Vector2(horizontal,vertical).normalized;

			SelfRigidbody2D.velocity = direction * MovementSpeed; 
        }
    }
}
