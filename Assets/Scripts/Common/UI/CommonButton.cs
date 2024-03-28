using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Common.UI
{
    public class CommonButton : Button
    {
        private Subject<Unit> onTapSubject = new Subject<Unit>();
        public IObservable<Unit> OnTap => onTapSubject;

		protected override void Awake()
		{
			base.Awake();
			onClick.AddListener(() => onTapSubject.OnNext(Unit.Default));
		}
	}
}
