using Cysharp.Threading.Tasks;
using Manager;
using Scene;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UniRx;
using UnityEngine.SceneManagement;
using Select;

namespace Title
{
	public class TitleSceneData : ISceneData
	{
	}

	public class TitleScene : SceneBase
	{
		public override async UniTask LoadAsync(ISceneData sceneData, CancellationToken token)
		{
			var titleSceneData = sceneData as TitleSceneData;

			InputManager.Instance.Controller.OnStartButton
				.Subscribe(_ => 
				{
					// SelectScene�Ɉڍs
					var selectSceneData = new SelectSceneData();
					Manager.SceneManager.Instance.Load(SceneType.Select, selectSceneData, LoadSceneMode.Single);
				})
				.AddTo(this);

			await UniTask.Yield();
		}
	}
}