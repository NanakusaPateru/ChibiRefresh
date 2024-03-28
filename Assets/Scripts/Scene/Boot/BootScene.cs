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

            // �N���`�F�b�N
            LoadAsync(sceneData, CancellationTokenManager.Instance.GetToken(TokenType.GameEnd)).Forget();

			// Manager�N���X�̐���

		}

        public override async UniTask LoadAsync(ISceneData sceneData, CancellationToken token)
        {
            // UserData�̓ǂݍ���
            UserDataManager.Instance.Load();
            Debug.Log(ScreenManager.Instance.ScreenHeight);
            Debug.Log(PopupManager.Instance.transform);
            Debug.Log(InputManager.Instance);

            // �e��Manager�̏�����
            await InputManager.Instance.LoadController();

            // TitleScene�ւ̑J��
            var titleSceneData = new Title.TitleSceneData();
            Manager.SceneManager.Instance.Load(SceneType.Title, titleSceneData, LoadSceneMode.Single);
            await UniTask.Yield();
        }
    }
}