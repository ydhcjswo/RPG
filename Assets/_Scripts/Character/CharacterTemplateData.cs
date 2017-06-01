using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleJSON;

public class CharacterTemplateData
{
	string StrKey = string.Empty;

	StatusData Status = new StatusData();       // 여기서 new를 쓰기 때문에 모노비헤비어를 상속 받으면 안된다.
	List<string> ListSkill = new List<string>();

	public string KEY { get { return StrKey; } }
	public StatusData STATUS { get { return Status; } }
	public List<string> LIST_SKILL { get { return ListSkill;	} }

	public CharacterTemplateData(string _strKey,JSONNode nodeData)
	{
		StrKey = _strKey;

		for(int i =0; i<(int)eStatusData.MAX; i++)
		{
			eStatusData statusData = (eStatusData)i;
			double valueData = nodeData[statusData.ToString("F")].AsDouble;     // 이넘에 있는 스트링 값을 전부 대문자로 받겠다.혹시 모르니
			Status.IncreaseData(statusData, valueData);
		}

		JSONArray arrSkill = nodeData["SKILL"].AsArray;
		if(arrSkill != null)
		{
			for(int i = 0; i<arrSkill.Count;i++)
			{
				ListSkill.Add(arrSkill[i]);
			}
		}
	}
	 






}
