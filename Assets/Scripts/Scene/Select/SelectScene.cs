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
            // UIの表示
            // はじめから、つづきから、もどるの3つ
            // 保存データがある場合のみつづきからを表示
            // データがある場合にはじめからを押した場合は消すか確認する

            await UniTask.Yield();
		}
	}
}