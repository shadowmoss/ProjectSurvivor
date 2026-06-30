using UnityEngine;
using QFramework;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class Player : ViewController
	{
		public float moveSpeed =5.0f;
		public static Player Default = null;
        void Awake()
        {
            Default = this;
        }
        void OnDestroy()
        {
            Default = null;
        }
        void Start()
		{

			// Code Here

			// OnTriggerEnter2DEvent这个是EventKit当中的collision2d。
			HurtBox.OnTriggerEnter2DEvent(collision2d =>
			{
				HitBox hitBox = collision2d.GetComponent<HitBox>();
				if (hitBox)
				{
					if(hitBox.Owner.CompareTag("Enemy")){
						Global.HP.Value--;
						if(Global.HP.Value <= 0)
						{
							this.DestroyGameObjGracefully();
							AudioKit.PlaySound("Die");

							// Player has been Destroyed so We Open UIGameOverPanel
							// ResKit.Init();
							UIKit.OpenPanel<UIGameOverPanel>();
						}
						else
						{
							AudioKit.PlaySound("Hurt");
						}
					}
				}
				
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
			Global.HP.RegisterWithInitValue((HP) =>
			{
				HPValue.fillAmount = Global.HP.Value / (float) Global.MaxHP.Value;
			}).UnRegisterWhenGameObjectDestroyed(this);
			Global.MaxHP.RegisterWithInitValue((HP) =>
			{
				HPValue.fillAmount = Global.HP.Value / (float) Global.MaxHP.Value;
			}).UnRegisterWhenGameObjectDestroyed(gameObject);
		}
        void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
			var vertical = Input.GetAxisRaw("Vertical");
			var targetVelocity = new Vector2(horizontal,vertical).normalized * (moveSpeed * Global.MovementSpeedRate.Value);
			SelfRigidbody2D.velocity =  Vector2.Lerp(SelfRigidbody2D.velocity,targetVelocity,1.0f-Mathf.Exp(-Time.deltaTime * 5));
        }
    }
}
