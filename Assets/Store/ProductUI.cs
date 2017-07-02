using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ui for product browse, view prodcuts in list
///		click one to open detail page
/// </summary>

public class ProductUI : BackStackBase
{
	[SerializeField]
	private ProductItem _pdItemPrefab;

	[SerializeField]
	private RectTransform _listContentRt;

	[SerializeField]
	private Transform _slot;

	// Use this for initialization
	void Start ()
	{
		#region DebugData
/*
		int count = Random.Range(4,12);
		while(count > 0)
		{
			GameObject itemGo = Instantiate(_pdItemPrefab.gameObject, _slot, false);
			ProductItem item = itemGo.GetComponent<ProductItem>();
			item.SetData(MainUI.Instance.DataSet.GetData("ttt"));
			item.OnSelectAction = MainUI.Instance.OpenItemForDetail;
			count--;
		}
*/
		#endregion

		ItemData[] datas = MainUI.Instance.DataSet.DataStorage;
		for (int i = 0; i < datas.Length; i++)
		{
			GameObject itemGo = Instantiate(_pdItemPrefab.gameObject, _slot, false);
			ProductItem item = itemGo.GetComponent<ProductItem>();
			item.SetData(datas[i]);
			item.OnSelectAction = MainUI.Instance.OpenItemForDetail;
		}

		qfUtility.AutoAdjustForScrollView(_listContentRt, _slot.GetComponent<VerticalLayoutGroup>().spacing);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
