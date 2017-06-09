using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIRigBlock : MonoBehaviour
{
	private Image _icon;
	private RigData _data;

	[SerializeField]
	private Texture2D _multiSprite;

	private Image _frame;

	private bool _inited = false;

	public System.Action<bool, PointerEventData> OnHoverAction;

	// Use this for initialization
	void Start ()
	{
		Init();
	}

	void Init()
	{
		if (_inited)
			return;

		_icon = qfUtility.GetComponentByPath<Image>(transform, "bg");
		_frame = qfUtility.GetComponentByPath<Image>(transform, "frame");

		try
		{
			EventTrigger et = _frame.gameObject.AddComponent<EventTrigger>();
			EventTrigger.Entry ety = new EventTrigger.Entry();
			ety.eventID = EventTriggerType.PointerEnter;
			ety.callback.AddListener((data) =>
			{
				if(OnHoverAction != null)
					OnHoverAction(true, (PointerEventData) data);
			});
			et.triggers.Add(ety);

			EventTrigger.Entry exit = new EventTrigger.Entry();
			exit.eventID = EventTriggerType.PointerExit;
			exit.callback.AddListener((data) =>
			{
				if(OnHoverAction != null)
					OnHoverAction(false, (PointerEventData) data);
			});
			et.triggers.Add(exit);
		}
		catch (Exception)
		{
			throw new NullReferenceException("this");
		}
	}

	public void SetData(RigData data)
	{
		Init();
		_data = data;
		Refresh();
	}

	void Refresh()
	{
		_icon.sprite = qfSpriteLoader.Instance.LoadSprite(_data.IconKey);
	}
}

[Serializable]
public class RigData
{
	public int ID = -1;
	public string NameKey = "NotSet";
	public string IconKey = "NotSet";

	public GearProps Props = null;

	public static RigData GetRandomStubData()
	{
		RigData d = new RigData();
		string[] rndIconKeys = 
		{
			"fb"
			, "linkedin"
			, "pin"
			, "rss"
			, "share"
			, "twitter"
			, "ytb"
		};

		d.Props = new GearProps();
		d.Props.Level = (GearProps.QualityLV) (UnityEngine.Random.Range(1, 5));
		int idx = (int) (UnityEngine.Random.value*rndIconKeys.Length);
		d.IconKey = rndIconKeys[idx];
		d.NameKey = rndIconKeys[idx];
		return d;
	}
}

[Serializable]
public class GearProps
{
	public int ID = -1;
	public float Durability = 1.0f;
	public QualityLV Level = QualityLV.None;

	public enum QualityLV
	{
		None = 0,
		Damaged,
		Normal,
		Rare,
		Epic
	}
}

