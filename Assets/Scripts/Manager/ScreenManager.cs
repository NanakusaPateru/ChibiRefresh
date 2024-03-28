using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class ScreenManager : SingletonManagerBase<ScreenManager>
    {
		private Vector2 screenSize = Vector2.zero;

		public float ScreenWidth => Screen.width;
		public float ScreenHeight => Screen.height;

		public float SafeAreaTop => Screen.safeArea.yMin;
		public float SafeAreaBottom => Screen.height - Screen.safeArea.yMax;

		//public Rect activeScreenRect

		public override void Initialize()
		{
			base.Initialize();
		}

		public void SetSafeArea(RectTransform safeArea)
		{
			safeArea.offsetMin = new Vector2(safeArea.offsetMin.x, SafeAreaBottom);
			safeArea.offsetMax = new Vector2(safeArea.offsetMax.x, -SafeAreaTop);
		}
	}
}
