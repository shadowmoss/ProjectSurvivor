using UnityEngine;
using QFramework;
using UnityEngine.UI;

// 1.请在菜单 编辑器扩展/Namespace Settings 里设置命名空间
// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改
namespace QFramework.Example
{
	public partial class FloatingTextController : ViewController
	{
		private static FloatingTextController mDefault;
		private void Awake()
		{
			mDefault = this;
		}
		private void OnDestroy()
		{
			mDefault = null;
		}
		void Start()
		{
			FloatingText.Hide();
		}
		public static void Play(Vector2 position,string text,bool critical = false)
		{
			mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
			.Position(position.x,position.y)
			.Self(f =>
			{
				var positionY = position.y;
				var textTrans = f.transform.Find("Text");
				var textComp = textTrans.GetComponent<Text>();
				textComp.text = text;

				if (critical)
				{
					textComp.color = Color.red;
				}

				ActionKit.Sequence()
					.Lerp(0, 0.5f, 0.5f, (p) =>
					{
						f.PositionY(positionY + p * 0.8f);
						textComp.LocalScaleX(Mathf.Clamp01(p * 0.03f));
						textComp.LocalScaleY(Mathf.Clamp01(p * 0.03f));
					})
					.Delay(0.5f)
					.Lerp(1.0f, 0, 0.3f, (p) =>
					{
						textComp.ColorAlpha(p);
					}, () =>
					{
						textTrans.DestroyGameObjGracefully();
					}).Start(textComp);
			}).Show();
		}
	}
}
