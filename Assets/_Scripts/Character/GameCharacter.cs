using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCharacter
{
	public BaseObject TargetComponent = null;

	CharacterTemplateData TemplateData = null;

	CharacterStatusData CharacterStatus = new CharacterStatusData();

	public CharacterTemplateData CHARACTER_TEMPLATE
	{ get { return TemplateData; } }

	public CharacterStatusData CHARACTER_STATUS
	{ get { return CharacterStatus; } }

	double CurrentHP = 0;
	public double CURRENT_HP
	{ get { return CurrentHP; } }

	// 스킬 데이터

	public void IncreaseCurrentHP(double valueData)
	{
		CurrentHP += valueData;
		if (CurrentHP < 0)
			CurrentHP = 0;

		double maxHP = CharacterStatus.GetStatusData(eStatusData.MAX_HP);
		if (CurrentHP > maxHP)
			CurrentHP = maxHP;

		if (CurrentHP == 0)
			TargetComponent.OBJECT_STATE = eBaseObjectState.STATE_DIE;
	}

	public void SetTemplate(CharacterTemplateData _templateData)
	{
		TemplateData = _templateData;
		CharacterStatus.AddStatusData(ConstValue.CharacterStatusDataKey, TemplateData.STATUS);
		CurrentHP = CharacterStatus.GetStatusData(eStatusData.MAX_HP);
	}
}
