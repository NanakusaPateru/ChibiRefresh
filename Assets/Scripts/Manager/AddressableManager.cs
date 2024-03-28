using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Manager
{
    public class AddressableManager : SingletonManagerBase<AddressableManager>
    {
        public async UniTask<GameObject> LoadGameObject(string path)
        {
            var prefab = await Addressables.LoadAssetAsync<GameObject>(path);
            return prefab;
        }
    }
}