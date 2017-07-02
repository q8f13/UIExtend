using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ThumbItem : MonoBehaviour, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
{
	[SerializeField]
	private Image _icon;
	[SerializeField]
	private Text _name;

	public System.Action<ItemData> OnTouchDownAction;

	private ItemData _data;

	public void SetData(ItemData data)
	{
		_data = data;
		_icon.sprite = qfSpriteLoader.Instance.LoadSprite(data.IconKey);
		_name.text = data.Name;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
//	  throw new System.NotImplementedException();
		Debug.Log("pointer donw");
		if (OnTouchDownAction != null)
			OnTouchDownAction(_data);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
//		throw new System.NotImplementedException();
		Debug.Log("pointer exit");
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		Debug.Log("pointer up");
	}

	void OnRelease()
	{
		
	}
}
