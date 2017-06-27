using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// item in product list
/// </summary>
public class ProductItem : MonoBehaviour, IPointerClickHandler
{
	public System.Action<GameObject, ItemData> OnSelectAction;

	private ItemData _data;

	[SerializeField]
	private Image _icon;
	[SerializeField]
	private Text _name;
	[SerializeField]
	private Text _price;

	public void SetData(ItemData data)
	{
		_data = data;
		Refresh();
	}

	void Refresh()
	{
//		throw new System.NotImplementedException();
		_icon.sprite = qfSpriteLoader.Instance.LoadSprite(_data.IconKey);
		_name.text = _data.Name;
		_price.text = _data.Price.ToString();
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		Debug.Log(gameObject.name + " clicked");
		if (OnSelectAction != null)
			OnSelectAction(gameObject, _data);
	}
}
