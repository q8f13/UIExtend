using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDrawer : MonoBehaviour
{
	[SerializeField]
	private GridLayoutGroup _grid;

	[SerializeField]
	private UIRigBlock _currRig;

	private float _mouseExitTimer = 0.0f;
	private const float MOUSE_EXIT_HIDE_DELAY = 0.5f;
	private bool _mouseExitCounting = false;

	// Use this for initialization
	void Start () {
		CreateStubIcons(8);

		_currRig.OnHoverAction += OnHoverHandler;

		_grid.gameObject.SetActive(false);
	}

	private void OnHoverHandler(bool on, PointerEventData data)
	{
		if (on)
		{
			_grid.gameObject.SetActive(on);
			Reset();
		}
		else
		{
			_mouseExitCounting = true;
		}
	}

	void Reset()
	{
		_mouseExitCounting = false;
		_mouseExitTimer = 0.0f;
	}

	void Update()
	{
		if (_mouseExitCounting)
		{
			_mouseExitTimer += Time.deltaTime;
			if (_mouseExitTimer > MOUSE_EXIT_HIDE_DELAY)
			{
				_grid.gameObject.SetActive(false);
				Reset();
			}
		}
	}

	void CreateStubIcons(int count)
	{
		int i = count;
		while(i > 0)
		{
			GameObject go = Instantiate(_currRig.gameObject);
			go.GetComponent<RectTransform>().SetParent(_grid.transform);
			UIRigBlock rig = go.GetComponent<UIRigBlock>();
			rig.SetData(RigData.GetRandomStubData());
			rig.OnHoverAction += OnHoverHandler;
			i--;
		}
	}
}
