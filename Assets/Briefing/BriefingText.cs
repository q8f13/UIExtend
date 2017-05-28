using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// briefing text
/// </summary>
public class BriefingText : MonoBehaviour
{
	public Text Container;			// 用于显示的文字容器
	public Text Ghost;				// 用于计算的容器，不显示
	public Button BtnNext;				// next

	public bool AutoPlay = true;		// 是否自动播放第一段

	[Range(1, 60), Tooltip("打字机效果每个字的帧数间隔")]
	public int FramePerChar = 2;

	private int _lineCount = 3;		// 当前文本框的可见行数

	private StringBuilder _sb = new StringBuilder(150);
	private string _textAll;

	private TextGenerator _tg;

	private bool _isPaused = true;
	private int _flag = 0;		// 所有文字中的index
	private int _fxFlag = 0;	// 当前段文字中的index

	// Use this for initialization
	void Start () {
		RectTransform rt = GetComponent<RectTransform>();

		_textAll = Container.text;
		// 计算可见行数
		float ascentPerLine = Container.lineSpacing*Container.font.ascent;
		_lineCount = Mathf.FloorToInt((rt.sizeDelta.y - ascentPerLine)/(ascentPerLine + Container.fontSize/2.0f));

		BtnNext.onClick.AddListener(() => { NextPa(); });
		// 重置界面
		Reset(AutoPlay);
	}

	/// <summary>
	/// 重制，如果开了自动播放(Autoplay)则自动开始第一段，否则显示按钮
	/// </summary>
	/// <param name="autoPlay"></param>
	void Reset(bool autoPlay)
	{
		Container.text = null;
		BtnNext.gameObject.SetActive(!autoPlay);
		if(autoPlay)
			NextPa();
	}

	void NextPa()
	{
		if (!_isPaused)
		{
			Debug.LogWarning("text fx is still playing");
			return;
		}

		_sb.Remove(0, _sb.Length);

		if (_flag >= _textAll.Length - 1)
		{
			Debug.LogWarning("reach the end");
			return;
		}
		
		// 先让ghost文本框先尝试装一下下段文本量，用来计算
		Ghost.text = _textAll.Substring(_flag,Mathf.Min(150, _textAll.Length - _flag));
		_tg = Ghost.cachedTextGenerator;

		// 开始打字机效果
		_isPaused = false;
		BtnNext.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		// text pop up
		if (!_isPaused && Time.frameCount % FramePerChar == 0)
		{
			bool lineEnd = false;
			// 打字机效果追踪。如果发现有换行则提前结束
			if (_fxFlag < Ghost.text.Length)
			{
				_sb.Append(Ghost.text[_fxFlag]);

				Container.text = _sb.ToString();

				_tg = Ghost.cachedTextGenerator;

				lineEnd = IsLineEnding(Ghost.text[_fxFlag]) && _tg.lineCount > _lineCount && _fxFlag > _tg.lines[_lineCount - 1].startCharIdx;

				_fxFlag++;
			}
				
			if (lineEnd || _fxFlag >= _tg.characterCountVisible)
			{
				_flag += _fxFlag;
				_isPaused = true;
				_fxFlag = 0;
				if (_flag < _textAll.Length - 1)
					BtnNext.gameObject.SetActive(true);
			}
		}
	}

	bool IsLineEnding(char c)
	{
		return c == '\n';
	}
}
