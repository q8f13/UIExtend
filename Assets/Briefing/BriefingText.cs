using System.Text;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BriefingText : MonoBehaviour
{
	public Text Container;
	public Text Ghost;
	public Button BtnNext;

	[Range(1, 60)]
	public int FramePerChar = 2;

	public int LineCount = 3;

	private StringBuilder _sb = new StringBuilder(150);
	private string _textAll;
	private string _textCurrent;

	private TextGenerator _tg;

	private bool _isPaused = true;
	private int _flag = 0;
	private int _fxFlag = 0;

	// Use this for initialization
	void Start () {
		RectTransform rt = GetComponent<RectTransform>();

		_textAll = Container.text;
//		LineCount = Mathf.FloorToInt(rt.sizeDelta.y/(Container.lineSpacing* (Container.font.ascent + Container.font.fontSize / 2.0f)));
		LineCount = Mathf.FloorToInt(rt.sizeDelta.y/(Container.lineSpacing* (Container.font.ascent * 2)));
//		LineCount = Mathf.FloorToInt(Container.minHeight/(Container.font.ascent + Container.font.fontSize/2.0f));
		Container.text = null;

		BtnNext.gameObject.SetActive(false);
		BtnNext.onClick.AddListener(() => { NextPa(); });

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
		
		Ghost.text = _textAll.Substring(_flag,Mathf.Min(150, _textAll.Length - _flag));
		_tg = Ghost.cachedTextGenerator;

		_isPaused = false;

		BtnNext.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

		// text pop up
		if (!_isPaused && Time.frameCount % FramePerChar == 0)
		{
			bool lineEnd = false;
			if (_fxFlag < Ghost.text.Length)
			{
				_sb.Append(Ghost.text[_fxFlag]);

				Container.text = _sb.ToString();

				_tg = Ghost.cachedTextGenerator;

				lineEnd = IsLineEnding(Ghost.text[_fxFlag]) && _tg.lineCount > LineCount && _fxFlag > _tg.lines[LineCount - 1].startCharIdx;

				_fxFlag++;
				if (lineEnd)
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
