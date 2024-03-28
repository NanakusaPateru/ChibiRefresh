using Common.UI;
using Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Controller
{
    public enum InputType
    {
        Tap,
        Keyboard
    }

    public class InputController : MonoBehaviour
    {
        [SerializeField]
        private GameObject uiGroupObject;
        [SerializeField]
        private CommonButton leftButton;
        [SerializeField]
        private CommonButton rightButton;
        [SerializeField]
        private CommonButton upButton;
        [SerializeField]
        private CommonButton downButton;
        [SerializeField]
        private CommonButton aButton;
        [SerializeField]
        private CommonButton bButton;
		[SerializeField]
		private CommonButton startButton;
		[SerializeField]
        private CommonButton selectButton;

        private Subject<Unit> onLeftButtonSubject = new Subject<Unit>();
        public IObservable<Unit> OnLeftButton => onLeftButtonSubject;
		private Subject<Unit> onRightButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnRightButton => onRightButtonSubject;
		private Subject<Unit> onUpButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnUpButton => onUpButtonSubject;
		private Subject<Unit> onDownButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnDownButton => onDownButtonSubject;
		private Subject<Unit> onAButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnAButton => onAButtonSubject;
		private Subject<Unit> onBButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnBButton => onBButtonSubject;
		private Subject<Unit> onStartButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnStartButton => onStartButtonSubject;
		private Subject<Unit> onSelectButtonSubject = new Subject<Unit>();
		public IObservable<Unit> OnSelectButton => onSelectButtonSubject;

		public void Initialize(InputType inputType)
        {
            switch (inputType)
            {
                case InputType.Tap:
                    SetTapButton();
                    break;
                case InputType.Keyboard:
                    SetKeyboardButton();
                    break;
                default:
                    break;
            }
        }

        private void SetTapButton()
        {
            uiGroupObject.SetActive(true);
			var buttonAndSubjectPairs = new KeyValuePair<CommonButton, Subject<Unit>>[] 
            {
                new KeyValuePair<CommonButton, Subject<Unit>>(leftButton, onLeftButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(rightButton, onRightButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(upButton, onUpButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(downButton, onDownButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(aButton, onAButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(bButton, onBButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(startButton, onStartButtonSubject),
                new KeyValuePair<CommonButton, Subject<Unit>>(selectButton, onSelectButtonSubject)                  
            };

            foreach (var buttonAndSubject in buttonAndSubjectPairs)
            {
                buttonAndSubject.Key.OnTap
                    .Subscribe(_ =>
                    {
                        buttonAndSubject.Value.OnNext(Unit.Default);
                    }).AddTo(this);
            }
        }

        private void SetKeyboardButton()
        {
            uiGroupObject.SetActive(false);
			var buttonAndSubjectPairs = new KeyValuePair<KeyCode, Subject<Unit>>[]
            {
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.LeftArrow, onLeftButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.RightArrow, onRightButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.UpArrow, onUpButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.DownArrow, onDownButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.Z, onAButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.X, onBButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.Escape, onStartButtonSubject),
				new KeyValuePair<KeyCode, Subject<Unit>>(KeyCode.LeftShift, onSelectButtonSubject)
            };

            foreach (var buttonAndSubject in buttonAndSubjectPairs)
            {
				Observable.EveryUpdate()
                    .Select(_ => Input.GetKeyDown(buttonAndSubject.Key))
	                .Where(isDown => isDown)
	                .Subscribe(_ =>
	                {
                        buttonAndSubject.Value.OnNext(Unit.Default);
	                })
	                .AddTo(this);
			}
		}
    }
}