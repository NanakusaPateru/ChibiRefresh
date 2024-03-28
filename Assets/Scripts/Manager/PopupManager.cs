using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class PopupManager : SingletonObjectManagerBase<PopupManager>
    {
		public static string STANDARD_POPUP = "Assets/Addressable/Prefabs/Popup.prefab";

		public override void Initialize()
		{
			base.Initialize();

			TestPopup().Forget();
		}

		private async UniTask TestPopup()
		{
			var prefab = await AddressableManager.Instance.LoadGameObject(STANDARD_POPUP);
			var test = Instantiate(prefab, transform);
		}
	}
}