using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Enemy : ViewController
	{
		public float MovementSpeed = 2.0f;

        public float HP = 3;
		void Start()
		{
			// Code Here
		}

        private void Update()
        {

			if (Player.Default) {
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // Transform组件的移动方法,让Enemy朝向Player缓慢移动
                transform.Translate(direction * Time.deltaTime * MovementSpeed);
            }

            if (HP <= 0) {
                this.DestroyGameObjGracefully();
                Global.Exp.Value++;
            }
        }
    }
}
