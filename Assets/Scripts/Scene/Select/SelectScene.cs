using Cysharp.Threading.Tasks;
using Scene;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Select
{
    public class SelectSceneData : ISceneData
    {
    }

    public class SelectScene : SceneBase
    {
		public override async UniTask LoadAsync(ISceneData sceneData, CancellationToken token)
		{
            var selectSceneData = sceneData as SelectSceneData;
            // UI�̕\��
            // �͂��߂���A�Â�����A���ǂ��3��
            // �ۑ��f�[�^������ꍇ�݂̂Â������\��
            // �f�[�^������ꍇ�ɂ͂��߂�����������ꍇ�͏������m�F����

            await UniTask.Yield();
		}
	}
}