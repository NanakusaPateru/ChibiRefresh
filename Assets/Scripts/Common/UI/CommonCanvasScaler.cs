using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class CommonCanvasScaler : CanvasScaler
    {
		protected override void Awake()
		{
			base.Awake();
			uiScaleMode = ScaleMode.ScaleWithScreenSize;
			referenceResolution = new Vector2(1080, 1920);
			screenMatchMode = ScreenMatchMode.MatchWidthOrHeight;
			matchWidthOrHeight = 1.0f;
		}
	}
}