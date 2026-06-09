/****************************************************************************
 * 2026.6 鲁童昕的MacBook Pro
 ****************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace QFramework.Example
{
	public partial class ExpUpgradePanel
	{
		[SerializeField] public UnityEngine.UI.Button UpgradeItemTemplate;
		[SerializeField] public RectTransform UpgrateRoot;

		public void Clear()
		{
			UpgradeItemTemplate = null;
			UpgrateRoot = null;
		}

		public override string ComponentName
		{
			get { return "ExpUpgradePanel";}
		}
	}
}
