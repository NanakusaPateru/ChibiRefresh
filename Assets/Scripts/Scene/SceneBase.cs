using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace Scene
{
	public abstract class SceneBase : MonoBehaviour
	{
		public abstract UniTask LoadAsync(ISceneData sceneData, CancellationToken token);
	}
}