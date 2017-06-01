using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue
{
	// JSON 관련
	public const string CharacterTemplatePath = "JSON/CHARACTER_TEMPLATE";
	public const string CharacterTemplateKey = "CHARACTER_TEMPLATE";

	// StatusData Key 관련
	public const string CharacterStatusDataKey = "CHARACTER_TEMPLATE";

	// GetData Key 관련
	public const string ActorData_Team = "TEAM_TYPE";
	public const string ActorData_SetTarget = "SET_TYPE";
	public const string ActorData_GetTarget = "GET_TYPE";
	public const string ActorData_AttackRange = "ATTACK_TYPE";
	public const string ActorData_Character = "CHARACTER";
	public const string ActorData_Hit = "HIT";

	// ThrowEvent Key 관련
	public const string EventKey_EnemyInit = "E_INIT";
	public const string EventKey_Hit = "E_HIT";
}

