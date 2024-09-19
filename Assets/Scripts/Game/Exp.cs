using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class Exp : ViewController
	{
		void Start()
		{
			// Code Here
		}
        private void OnTriggerEnter2D(Collider2D collision)
        {
			if (collision.GetComponent<CollectableArea>()) {
                Global.Exp.Value++;
                this.DestroyGameObjGracefully();
            }
        }
    }
}
