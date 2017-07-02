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
public class DetailUI : BackStackBase
{
	private const string IN_LIST_TXT = "In Wishlist";
	private const string ADD_TO_LIST = "Add to list";

	[SerializeField]
	private Image _banner;
	[SerializeField]
	private Text _price;
	[SerializeField]
	private Text _name;
	[SerializeField]
	private Text _desc;

	[SerializeField]
	private Button _addBtn;
	[SerializeField]
	private Button _arBtn;

	private ItemData _currData;

	void Start()
	{
		_addBtn.onClick.AddListener(() =>
		{
			MainUI.Instance.AddItemToWishlist(_currData);
			UpdateAddToBtn();
		});

		_arBtn.onClick.AddListener(() => MainUI.Instance.ShowDemonstration(_currData));
	}

	public void ShowDetail(ItemData data)
	{
		Show();

		_currData = data;

		if (data == null)
		{
			Debug.LogError("invalid itemData");
			return;
		}

		_banner.sprite = qfSpriteLoader.Instance.LoadSprite(data.BannerKey);
		_desc.text = data.Description;
		_name.text = data.Name;
		_price.text = data.Price.ToString();

		UpdateAddToBtn();
	}

	// change add wishlist btn style due to status
	void UpdateAddToBtn()
	{
		bool alreadyInList = MainUI.Instance.DataSet.AlreadyInWishlist(_currData);
		_addBtn.interactable = !alreadyInList;
		_addBtn.transform.GetChild(0).GetComponent<Text>().text = alreadyInList ? IN_LIST_TXT : ADD_TO_LIST;
	}
}
