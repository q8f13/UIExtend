using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataSet : ScriptableObject
{
	private Dictionary<string, ItemData> _dataSetStore;

	public ItemData[] DataStorage;

	[NonSerialized]
	private List<ItemData> _wishList;

	public bool Inited { get { return _dataSetStore != null && _dataSetStore.Count > 0; } }
	public int Count { get { return DataStorage.Length; } }
	public ItemData[] GetWishList { get { return _wishList.ToArray(); } }

	public void Init()
	{
		if (Inited)
			return;

		_dataSetStore = new Dictionary<string, ItemData>();
		if (DataStorage != null && DataStorage.Length > 0)
		{
			ItemData d = null;
			for(int i=0;i<DataStorage.Length;i++)
			{
				d = DataStorage[i];
				_dataSetStore.Add(d.Name, d);
			}
		}

		if(_wishList != null && _wishList.Count > 0)
			_wishList.Clear();
	}

	public ItemData GetData(string name)
	{
		Init();

		if(_dataSetStore.ContainsKey(name))
			return _dataSetStore[name];

		Debug.LogError(string.Format("cannot find itemData by name '{0}'", name));
		return null;
	}

	public void AddToWishlist(ItemData data)
	{
		if(_wishList == null)
			_wishList = new List<ItemData>();

		if (_wishList.Contains(data))
			return;

		_wishList.Add(data);
	}

	public bool AlreadyInWishlist(ItemData data)
	{
		if (_wishList == null || _wishList.Count == 0)
			return false;

		return _wishList.Contains(data);
	}
}

[Serializable]
public class ItemData
{
	public string Id = "NotSet";
	public string Name = "NoName";
	public string Description = "NoName";
	public int Price = 0;
	public string IconKey = "NotSet";
	public string BannerKey = "NotSet";
	public GameObject ModelRef = null;
}
