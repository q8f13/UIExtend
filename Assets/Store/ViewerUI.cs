using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewerUI : BackStackBase
{
	[SerializeField]
	private GameObject THumbItemPrefab;

	[SerializeField]
	private Transform _slot;

	private List<ThumbItem> _items = new List<ThumbItem>();

	[SerializeField]
	private GameObject _sideMenuGo;

	[SerializeField]
	private Button _sideMenuToggler;

	public override void Show()
	{
		base.Show();

		// clear
		if (_items.Count > 0)
		{
			for(int i=0;i<_items.Count;i++)
				RemoveItem(_items[i]);

			_items.Clear();
		}

		// load wishlist
		// UNDONE: stub data source
		ItemData[] list = MainUI.Instance.DataSet.DataStorage;

		for (int i = 0; i < list.Length; i++)
		{
			GameObject g = Instantiate(THumbItemPrefab, _slot);
			g.GetComponent<ThumbItem>().SetData(list[i]);
		}

		// recalculate height for slot
		VerticalLayoutGroup vlg = _slot.GetComponent<VerticalLayoutGroup>();
		qfUtility.AutoAdjustForScrollView(_slot.GetComponent<RectTransform>(), vlg.spacing);
	}

	void Start()
	{
		_sideMenuToggler.onClick.AddListener(ToggleSideMenu);
	}

	void ToggleSideMenu()
	{
		_sideMenuGo.SetActive(!_sideMenuGo.activeInHierarchy);
	}

	void RemoveItem(ThumbItem item)
	{
		item.transform.parent = null;
		Destroy(item.gameObject);
	}
}
