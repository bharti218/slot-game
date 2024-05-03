using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slotgame
{

	public class StateMachine : MonoBehaviour
	{
		public virtual BaseState CurrentState
		{
			get { return _currentState; }
			set { Transition(value); }
		}
		protected BaseState _currentState;
		protected bool _inTransition;

		public virtual T GetState<T>() where T : BaseState
		{
			T target = GetComponent<T>();
			if (target == null)
				target = gameObject.AddComponent<T>();
			return target;
		}

		public virtual void ChangeState<T>() where T : BaseState
		{
			CurrentState = GetState<T>();
		}

		protected virtual void Transition(BaseState value)
		{
			if (_currentState == value)
			{
				Debug.Log("StateMachine Transition _inTransition" + _inTransition.ToString());
				return;
			}


			_inTransition = true;

			if (_currentState != null)
				_currentState.Exit();

			_currentState = value;

			if (_currentState != null)
				_currentState.Enter();

			_inTransition = false;
		}
	}
}