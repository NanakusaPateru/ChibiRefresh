using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SingletonManagerBase<T> where T : SingletonManagerBase<T>, new()
	{
        private static T instance = null;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new T();
                    instance.Initialize();
                }
                return instance;
            }
        }

        public virtual void Initialize() { }
	}
}