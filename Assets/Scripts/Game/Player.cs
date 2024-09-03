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
			var horizontal =  Input.GetAxis("Horizontal");	// 1	������ˮƽ���Ǵ�ֱ���򣬼�⵽������ֵ����1����ͬʱ����ʱ���Ϊ����2��������
			var vertical =  Input.GetAxis("Vertical");		// 1

			// �ٶ��Ǹ��Ŷ�,������λ��֮��Ϊ1,������Ϊ��ȡ��������ָʾ�ķ�������

			var direction = new Vector2(horizontal,vertical).normalized;

			SelfRigidbody2D.velocity = direction * MovementSpeed; 
        }
    }
}
