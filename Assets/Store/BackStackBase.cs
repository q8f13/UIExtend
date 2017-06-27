using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BackStackBase : MonoBehaviour
{
	protected Stack<BackStackBase> _stack;
	public bool IsInited { get { return _stack != null; } }

	public virtual void Initialize(Stack<BackStackBase> stack)
	{
		_stack = stack;
	}

	public virtual void Show()
	{
		if (!IsInited)
		{
			Debug.LogError("please call Initialize first");
			return;
		}

		gameObject.SetActive(true);
		if(!_stack.Contains(this))
			_stack.Push(this);
	}

	public virtual void Hide()
	{
		gameObject.SetActive(false);
	}
}
