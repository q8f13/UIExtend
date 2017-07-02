using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
	private static MainUI _instance = null;
	public static MainUI Instance { get { return _instance; } } 
	
	[SerializeField]
	private ProductUI _pdUI;

	[SerializeField]
	private DetailUI _dtUI;

	[SerializeField]
	private ViewerUI _vUI;

	[SerializeField]
	private Button _back;

	private bool _inited = false;

	private Stack<BackStackBase> _backStack = new Stack<BackStackBase>(5);

	[SerializeField]
	private DataSet _ds;
	public DataSet DataSet { get { return _ds; } }

	void Awake()
	{
		_instance = this;
	}

	// Use this for initialization
	void Start ()
	{
		Init();

	}

	private void Init()
	{
		if (_inited)
			return;

		_dtUI.Initialize(_backStack);
		_pdUI.Initialize(_backStack);
		_vUI.Initialize(_backStack);

		_ds.Init();

		_back.onClick.AddListener(OnBackPressed);

		// open list ui
		_dtUI.Hide();
		_vUI.Hide();
		_pdUI.Show();
		UpdateBackBtn();

		_inited = true;
	}

	/// <summary>
	/// open detail page for item
	/// </summary>
	/// <param name="go"></param>
	/// <param name="data"></param>
	public void OpenItemForDetail(GameObject go, ItemData data)
	{
		_backStack.Peek().Hide();
		_dtUI.ShowDetail(data);

		UpdateBackBtn();
	}

	public void AddItemToWishlist(ItemData data)
	{
		_ds.AddToWishlist(data);
	}

	public void ShowDemonstration(ItemData data)
	{
		_backStack.Peek().Hide();
		_vUI.Show();
		_vUI.Refresh(_ds.GetWishList);
	}

	private void OnBackPressed()
	{
		if (_backStack.Count < 1)
		{
			throw new Exception("back btn should not be clicked");
		}

		_backStack.Pop().Hide();
		BackStackBase next = _backStack.Peek();
		if(next!= null)
			next.Show();

		UpdateBackBtn();
	}

	void UpdateBackBtn()
	{
		_back.gameObject.SetActive(_backStack.Count > 1);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
