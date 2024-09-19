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
            "触发收集金币".LogInfo();
            if (collision.GetComponent<CollectableArea>())
            {
                Global.Coin.Value++;
                this.DestroyGameObjGracefully();
            }
        }
    }
}
