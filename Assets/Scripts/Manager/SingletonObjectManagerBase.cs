using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SingletonObjectManagerBase<T> : MonoBehaviour where T : SingletonObjectManagerBase<T>, new ()
    {
		private static T instance = null;
		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					var type = typeof(T);
					var gameObject = new GameObject(type.Name, type);
					instance = gameObject.GetComponent<T>();
					instance.Initialize();
					DontDestroyOnLoad(gameObject);
				}
				return instance;
			}
		}

		public virtual void Initialize() { }
	}
}
