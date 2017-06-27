using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DataSet : ScriptableObject
{
	private Dictionary<string, ItemData> _dataSetStore;

	public ItemData[] DataStorage;

	public void Init()
	{
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
	}

	public ItemData GetData(string name)
	{
		if(_dataSetStore.ContainsKey(name))
			return _dataSetStore[name];

		Debug.LogError(string.Format("cannot find itemData by name '{0}'", name));
		return null;
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
