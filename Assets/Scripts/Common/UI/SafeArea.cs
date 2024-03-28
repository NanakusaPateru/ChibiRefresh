using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    public class SafeArea : MonoBehaviour
    {
		[SerializeField]
		private RectTransform rectTransform;

		public void Awake()
		{
			ScreenManager.Instance.SetSafeArea(rectTransform);
		}
	}
}