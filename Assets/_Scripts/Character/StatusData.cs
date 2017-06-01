﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StatusData
{
	Dictionary<eStatusData, double> DicData = new Dictionary<eStatusData, double>();

	public void InitData()
	{
		DicData.Clear();
	}
	public void Copy(StatusData data)
	{
		foreach(KeyValuePair<eStatusData,double> pair in data.DicData)
		{
			IncreaseData(pair.Key, pair.Value);
		}
	}
	public void IncreaseData(eStatusData statusData, double valueData)
	{
		double preValue = 0.0;
		DicData.TryGetValue(statusData, out preValue);
		DicData[statusData] = preValue + valueData;		// 키가 없을시 add 작업까지 진행
	}

	public void DecreaseData(eStatusData statusData,double valueData)
	{
		double preValue = 0.0;
		DicData.TryGetValue(statusData, out preValue);
		DicData[statusData] = preValue - valueData;
	}

	public void SetData(eStatusData statusData,double valueData)
	{
		DicData[statusData] = valueData;
	}

	public void RemoveData(eStatusData statusData)
	{
		if (DicData.ContainsKey(statusData) == true)
			DicData.Remove(statusData);
	}
	public double GetStatusData(eStatusData statusData)
	{
		double preValue = 0.0;
		DicData.TryGetValue(statusData, out preValue);
		return preValue;
	}
}
