using Cysharp.Threading.Tasks;
using Scene;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manager
{
	public enum SceneType
	{
		Boot,
		Title,
		Select,
		Game,
		Card,
		Map,
		Scenario,
		Status
	}

    public class SceneManager : SingletonManagerBase<SceneManager>
    {
		private const string BOOT_SCENE_NAME = "BootScene";
		private const string TITLE_SCENE_NAME = "TitleScene";
		private const string SELECT_SCENE_NAME = "SelectScene";
		private const string GAME_SCENE_NAME = "GameScene";
		private const string CARD_SCENE_NAME = "CardScene";
		private const string MAP_SCENE_NAME = "MapScene";
		private const string SCENARIO_SCENE_NAME = "ScenarioScene";
		private const string STATUS_SCENE_NAME = "StatusScene";

		private string GetSceneName(SceneType sceneType)
		{
			switch (sceneType)
			{
				case SceneType.Boot:
					return BOOT_SCENE_NAME;
				case SceneType.Title:
					return TITLE_SCENE_NAME;
				case SceneType.Select:
					return SELECT_SCENE_NAME;
				case SceneType.Game:
					return GAME_SCENE_NAME;
				case SceneType.Card:
					return CARD_SCENE_NAME;	
				case SceneType.Map:
					return MAP_SCENE_NAME;
				case SceneType.Scenario:
					return SCENARIO_SCENE_NAME;
				case SceneType.Status:
					return STATUS_SCENE_NAME;
				default:
					return string.Empty;
			}
		}

		public override void Initialize()
		{
			base.Initialize();
		}

		public void Load(SceneType sceneType, ISceneData sceneData, LoadSceneMode sceneMode)
		{
			LoadAsync(sceneType, sceneData, sceneMode, CancellationTokenManager.Instance.GetToken(TokenType.GameEnd)).Forget();
		}

		public async UniTask LoadAsync(SceneType sceneType, ISceneData sceneData, LoadSceneMode sceneMode, CancellationToken token)
		{
			// SceneChangeでシーン内で走っている処理をすべて終了
			if (sceneMode == LoadSceneMode.Single)
			{
				CancellationTokenManager.Instance.DisposeToken(TokenType.SceneChange);
			}

			var sceneName = GetSceneName(sceneType);
			await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, sceneMode);

			token.ThrowIfCancellationRequested();

			var sceneIndex = -1;

			// Scene検索
			for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCount; i++)
			{
				if (UnityEngine.SceneManagement.SceneManager.GetSceneAt(i).name == sceneName)
				{
					sceneIndex = i;
					break;
				}
			}

			// 見つからなければ終了
			if (sceneIndex == -1)
			{
				return;
			}

			var sceneObjects = UnityEngine.SceneManagement.SceneManager.GetSceneAt(sceneIndex).GetRootGameObjects();

			SceneBase sceneBase = null;

			// Sceneの一番上にComponentがある設計なので、Rootから取得
			foreach (var sceneObject in sceneObjects)
			{
				if (sceneObject.TryGetComponent<SceneBase>(out sceneBase))
				{
					// Scene読み込み開始
					await sceneBase.LoadAsync(sceneData, token);
					break;
				}
			}

			// 一応チェック
			if (sceneBase == null)
			{
				Debug.LogError("Scene Load Error : component not find");
			}
		}

		public void UnLoad(SceneType sceneType)
		{
			UnLoadAsync(sceneType, CancellationTokenManager.Instance.GetToken(TokenType.GameEnd)).Forget();
		}

		public async UniTask UnLoadAsync(SceneType sceneType, CancellationToken token)
		{
			var sceneName = GetSceneName(sceneType);
			await UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);

			token.ThrowIfCancellationRequested();
		}
	}
}