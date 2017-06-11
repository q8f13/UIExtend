using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用来测试briefing换字
/// </summary>

public class TextChanger : MonoBehaviour
{
	public static string[] TEXT_SAMPLES =
	{
		"战术和火力压制\n我们把火力压制和其衍生出的战术打法作为游戏的立身之本。尝试压制敌人并摆脱敌人的压制是一件充满挑战和乐趣的事情。面对训练有素的日军，光有勇气还不够，你要足智多谋。\n小队指挥\n你可以只身出战，也可以带领最多由10人组成的小队参战。你可以管理并增强你的士兵，并为他们赋予一些独特的性格。\n东方文化\n中国是二战期间最早投入到世界反法西斯战争中的国家之一，但是这段历史却并不被经常提及。我们希望通过这个游戏，为你呈现一个充满东方文化韵味的二战游戏体验。",
		"《银河护卫队》（Guardians of the Galaxy）是漫威影业出品的一部科幻动作电影，取材自漫威漫画，是漫威电影宇宙的第十部电影。由迪斯尼出品、漫威影业制作，詹姆斯·古恩执导。克里斯·帕拉特、范·迪塞尔、布莱德利·库珀、佐伊·索尔达娜、戴夫·巴蒂斯塔、李·佩斯等主演[1-2]  。影片于2014年8月1日在北美公映。",
		"雪乐山公司（Sierra Entertainment）是一家专门制作和出版电子游戏的公司，成立于1979年，其前身是在线系统公司（On-Line Systems），创立者为肯·威廉姆斯（Ken Williams）和罗伯塔·威廉姆斯（Roberta Williams），在2004年被维旺迪环球娱乐公司重组，2007年在维旺迪游戏收购Activision后与Activision合并成立动视暴雪公司。"
	};

	public Button ChangeBtn;
	public BriefingText Briefing;

	// Use this for initialization
	void Start ()
	{
		ChangeBtn.onClick.AddListener(() => { Briefing.Refill(TEXT_SAMPLES[Random.Range(0, 3)]); });
	}

}
