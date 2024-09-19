using UnityEngine;
using QFramework;
using System;
using static UnityEngine.EventSystems.EventTrigger;

namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		public float MovementSpeed = 2.0f;

        public float HP = 3;
		void Start()
		{
            EnemyGenerator.EnemyCount.Value++;
		}
        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }

        private void Update()
        {

			if (Player.Default) {
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // Transform组件的移动方法,让Enemy朝向Player缓慢移动
                transform.Translate(direction * Time.deltaTime * MovementSpeed);
            }

            if (HP <= 0) {

                Global.GeneratePowerUp(this.gameObject);
                this.DestroyGameObjGracefully();
            }
        }

        private bool mIgnoreHurt = false;
        public void Hurt(float value)
        {
            if(mIgnoreHurt)
            {
                return;
            }

            Sprite.color = Color.red;
            ActionKit.Delay(0.2f, () =>
            {
                this.HP -= Global.SimpleAbilityDamage.Value;
                this.Sprite.color = Color.white;
                mIgnoreHurt = false;
            }).Start(this);
        }
    }
}
