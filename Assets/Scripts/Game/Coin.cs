using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Coin : ViewController
	{
		void Start()
		{
			// Code Here
		}

        private void OnTriggerEnter2D(Collider2D collision)
        {
            "�����ռ����".LogInfo();
            if (collision.GetComponent<CollectableArea>())
            {
                Global.Coin.Value++;
                this.DestroyGameObjGracefully();
            }
        }
    }
}
