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
            // �ۑ����Ă��郆�[�U�[�f�[�^��ǂݍ���
            var userId = "";
            
            if (!PlayerPrefs.HasKey(SaveDataDefine.USER_ID))
            {
                // �o�b�N�G���h�Ƃ̒ʐM�͂Ȃ��̂ŁA�u�����{�����v�Ŕ��Ȃ��悤�Ȑ��l�Őݒ肷��
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
