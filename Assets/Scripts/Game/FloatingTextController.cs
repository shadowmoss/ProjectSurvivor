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
		public static void Play(Vector2 position,string text)
		{
			Debug.Log(position);
			Debug.Log("Voke FloatingText");
			mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
			.Position(position.x,position.y)
			.Self(f =>
			{
				var positionY = position.y;
				var textTrans = f.transform.Find("Text");
				var textComp = textTrans.GetComponent<Text>();
				textComp.text = text;
				ActionKit.Sequence()
					.Lerp(0, 0.5f, 0.5f, (p) =>
					{
						 Debug.Log("执行Lerp");
						f.PositionY(positionY + p * 0.8f);
						textComp.LocalScaleX(Mathf.Clamp01(p * 0.03f));
						textComp.LocalScaleY(Mathf.Clamp01(p * 0.03f));
					})
					.Delay(0.5f)
					.Lerp(1.0f, 0, 0.3f, (p) =>
					{
						Debug.Log("执行Lerp2");
						textComp.ColorAlpha(p);
					}, () =>
					{
						Debug.Log("执行销毁Lerp");
						textTrans.DestroyGameObjGracefully();
					}).Start(textComp);
			}).Show();
		}
	}
}
