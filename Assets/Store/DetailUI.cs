using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///	ui for detail info. 
///		Press back to list page; 
///		Press add to list can add current product to compare list
///		Press showcase to view item in AR scene
/// </summary>
public class DetailUI : BackStackBase {

	[SerializeField]
	private Image _banner;
	[SerializeField]
	private Text _price;
	[SerializeField]
	private Text _name;
	[SerializeField]
	private Text _desc;

	public void ShowDetail(ItemData data)
	{
		Show();

		if (data == null)
		{
			Debug.LogError("invalid itemData");
			return;
		}

		_banner.sprite = qfSpriteLoader.Instance.LoadSprite(data.BannerKey);
		_desc.text = data.Description;
		_name.text = data.Name;
		_price.text = data.Price.ToString();

	}
}
