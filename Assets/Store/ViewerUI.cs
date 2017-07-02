using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewerUI : BackStackBase
{
	[SerializeField] private ThumbItem ItemPrefab;

	[SerializeField]
	private GameObject _sideMenuGo;
	[SerializeField]
	private Transform _slot;
	[SerializeField]
	private Button _sideMenuToggler;

	private ThumbItem[] _items;
//	private ItemData[] _dataArray;

	// Use this for initialization
	void Start ()
	{
		_sideMenuToggler.onClick.AddListener(() => ToggleSideMenu(!_sideMenuGo.activeInHierarchy));
	}

	void ToggleSideMenu(bool on)
	{
		_sideMenuGo.gameObject.SetActive(on);
		_sideMenuToggler.gameObject.SetActive(!on);
	}

	ThumbItem GetItem(int idx)
	{
		if(_items == null)
			_items = new ThumbItem[MainUI.Instance.DataSet.Count];

		if (idx < 0 || idx > _items.Length - 1)
		{
			Debug.LogError("invalid index: out of boundary");
			return null;
		}

		if (_items[idx] != null)
			_items[idx].gameObject.SetActive(true);
		else
			_items[idx] = CreateNewItem();

		return _items[idx];
	}

	ThumbItem CreateNewItem()
	{
		GameObject go = Instantiate(ItemPrefab.gameObject, _slot);
		return go.GetComponent<ThumbItem>();
	}

	public void Refresh(ItemData[] data)
	{
//		_dataArray = data;

		// update items
		int i;
		for (i=0; i < data.Length; i++)
		{
			GetItem(i).SetData(data[i]);
		}

		// hide unused items
		if (i < _items.Length)
		{
			for (; i < _items.Length; i++)
			{
				GetItem(i).gameObject.SetActive(false);
			}
		}

		// reset content height
		VerticalLayoutGroup vlg = _slot.GetComponent<VerticalLayoutGroup>();
		qfUtility.AutoAdjustForScrollView(_slot.GetComponent<RectTransform>()
			, vlg.spacing);

		// hide side menu
		ToggleSideMenu(true);

		// show selected item model in transparent (unlocated)
			// instantiate the prefab of itemdata
			// set prefab center in screen
			// switch shader to 'locating'
			// show tip to guide user dragging and drop it
	}
}
