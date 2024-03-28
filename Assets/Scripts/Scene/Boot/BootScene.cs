using Cysharp.Threading.Tasks;
using Manager;
using Scene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Boot
{
	public class BootSceneData : ISceneData
	{
	}

	public class BootScene : SceneBase
    {
		private void Awake()
        {
            var sceneData = new BootSceneData();

            // 起動チェック
            LoadAsync(sceneData, CancellationTokenManager.Instance.GetToken(TokenType.GameEnd)).Forget();

			// Managerクラスの生成

		}

        public override async UniTask LoadAsync(ISceneData sceneData, CancellationToken token)
        {
            // UserDataの読み込み
            UserDataManager.Instance.Load();
            Debug.Log(ScreenManager.Instance.ScreenHeight);
            Debug.Log(PopupManager.Instance.transform);
            Debug.Log(InputManager.Instance);

            // 各種Managerの初期化
            await InputManager.Instance.LoadController();

            // TitleSceneへの遷移
            var titleSceneData = new Title.TitleSceneData();
            Manager.SceneManager.Instance.Load(SceneType.Title, titleSceneData, LoadSceneMode.Single);
            await UniTask.Yield();
        }
    }
}