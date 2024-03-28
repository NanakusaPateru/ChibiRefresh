using Common.Controller;
using Cysharp.Threading.Tasks;
using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class InputManager : SingletonObjectManagerBase<InputManager>
    {
		public static string CONTROLLER_PATH = "Assets/Addressable/Prefabs/InputController.prefab";

		private InputController controller = null;
		public InputController Controller => controller;

		public override void Initialize()
		{
			base.Initialize();
		}

		public async UniTask LoadController()
		{
			var prefab = await AddressableManager.Instance.LoadGameObject(CONTROLLER_PATH);
			var controllerObect = Instantiate(prefab, transform);
			controller = controllerObect.GetComponent<InputController>();

#if UNITY_ANDROID || UNITY_IOS
			controller.Initialize(InputType.Tap);
#else
			controller.Initialize(InputType.Keyboard);
#endif
		}
	}
}