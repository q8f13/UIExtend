using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// item in AR scene UI
/// </summary>
public class ThumbItem : MonoBehaviour, IPointerDownHandler, IPointerExitHandler
{
	[SerializeField]
	private Image _icon;
	[SerializeField]
	private Text _name;

	// bool => _isDraggingAndHover
	public System.Action<ItemData> OnTouchDownAction;
	public System.Action<ItemData> OnTouchExitAction;
	private ItemData _data;

	public void SetData(ItemData data)
	{
		_icon.sprite = qfSpriteLoader.Instance.LoadSprite(data.IconKey);
		_icon.SetNativeSize();
		_name.text = data.Name;
		_data = data;
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log(gameObject.name + " touched down");
		if (OnTouchDownAction != null)
			OnTouchDownAction(_data);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log(gameObject.name + " touched down");
		if (OnTouchExitAction != null)
			OnTouchExitAction(_data);
	}
}
