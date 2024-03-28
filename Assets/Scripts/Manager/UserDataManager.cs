using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Define;
using System;
using System.Threading;

namespace Manager 
{
    public class UserDataManager : SingletonManagerBase<UserDataManager>
    {
        public string UserId
        {
            get;
            private set;
        } = string.Empty;

		public override void Initialize()
		{
			base.Initialize();
		}

		public void Load()
        {
            // 保存しているユーザーデータを読み込む
            var userId = "";
            
            if (!PlayerPrefs.HasKey(SaveDataDefine.USER_ID))
            {
                // バックエンドとの通信はないので、「日時＋乱数」で被らないような数値で設定する
                var time = DateTime.Now;
                var uniqueId = Guid.NewGuid();

                userId = $"{time}-{uniqueId}";
                PlayerPrefs.SetString(SaveDataDefine.USER_ID, userId);
                PlayerPrefs.Save();
			}

            UserId = PlayerPrefs.GetString(SaveDataDefine.USER_ID);            
        }
    }
}
