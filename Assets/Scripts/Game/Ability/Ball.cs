using UnityEngine;
using QFramework;
using QAssetBundle;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class Ball : ViewController
	{
		void Start()
		{
			SelfRigidbody2D.velocity = new Vector2(Random.Range(1.0f,1.0f),Random.Range(-1.0f,1.0f) * 
			Random.Range(Global.BasketBallSpeed.Value - 5,Global.BasketBallSpeed.Value + 5));
			HurtBox.OnTriggerEnter2DEvent(collider2D =>
			{
				var hurtBox =  collider2D.GetComponent<HurtBox>();
				if (hurtBox && hurtBox.Owner.CompareTag("Enemy"))
				{
					Enemy enemy = hurtBox.Owner.GetComponent<Enemy>();
					enemy.Hurt(Global.BasketBallDamage.Value);
					
					if(Random.Range(0,1f) < 0.5f && collider2D && collider2D.attachedRigidbody && Player.Default)
					{
						collider2D.attachedRigidbody.velocity = 
												(collider2D.NormalizedDirection2DFrom(this) * 5) + 
												collider2D.NormalizedDirection2DFrom(Player.Default) * 10;
					}
				}
			});
		}
        void OnCollisionEnter2D(Collision2D collision)
        {
            var normal = collision.GetContact(0).normal;
			if(normal.x > normal.y)
			{
				SelfRigidbody2D.velocity = new Vector2(SelfRigidbody2D.velocity.x,
				Mathf.Sign(SelfRigidbody2D.velocity.y) * 
				Random.Range(0.5f,1.5f) * 
				Random.Range(Global.BasketBallSpeed.Value - 2,Global.BasketBallSpeed.Value + 2));
				SelfRigidbody2D.angularVelocity = Random.Range(-360,360);
			}
			else
			{
				var rb = SelfRigidbody2D;
				rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * 
				Random.Range(0.5f,1.5f) * 
				Random.Range(Global.BasketBallSpeed.Value-2,Global.BasketBallSpeed.Value+2),
				rb.velocity.y);
				rb.angularVelocity = Random.Range(-360f,360f);
			}
			AudioKit.PlaySound(Sfx.BALL);
        }
    }
}
