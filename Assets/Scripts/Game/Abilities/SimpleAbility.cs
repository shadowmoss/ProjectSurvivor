using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class SimpleAbility : ViewController
	{
		private float mCurrentSeconds = 0;

		void Start()
		{
			// Code Here
		}
        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;
			if (mCurrentSeconds > 1.5f) {
				mCurrentSeconds = 0;
				var enemies =  FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
				foreach (var enemy in enemies) {
					var distance = (Player.Default.transform.position -enemy.transform.position).magnitude;
					if (distance <= 5) {
						enemy.Sprite.color = Color.red;
						var enemyRefCache = enemy;
						ActionKit.Delay(0.3f, () =>
						{
							enemyRefCache.HP-=Global.SimpleAbilityDamage.Value;
							enemyRefCache.Sprite.color = Color.white;
						}).StartGlobal();
					}
				}
			}
        }
    }
}
