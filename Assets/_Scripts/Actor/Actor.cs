using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : BaseObject
{
	// TeamType
	[SerializeField]
	eTeamType TeamType;
public eTeamType TEAM_TYPE
	{
		get
		{
			return TeamType;
		}
	}
		[SerializeField]
	string TemplateKey = string.Empty;

	GameCharacter SelfCharacter = null;
	public GameCharacter SELF_CHARACTER
	{ get { return SelfCharacter; } }

	// TemplateKey -> Status
	// GameCharacter

	// AI
	BaseAI ai = null;
	public BaseAI AI { get { return ai; } }

	BaseObject TargetObject = null;
	// Attack Target

	// Board -> HP Bar

	private void Awake()
	{
		GameObject aiObject = new GameObject();
		aiObject.name = "NormalAI";
		ai = aiObject.AddComponent<NormalAI>();
		aiObject.transform.SetParent(SelfTransform);

		// 없으면 동작 안함
		ai.Target = this;



		GameCharacter gameCharacter = CharacterManager.Instance.AddCharacter(TemplateKey);
		gameCharacter.TargetComponent = this;
		SelfCharacter = gameCharacter;

		ActorManager.Instance.AddActor(this);

		//Debug.Log(gameCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK)); 
	}
	public double GetStatusData(eStatusData statusData)
	{
		return SelfCharacter.CHARACTER_STATUS.GetStatusData(statusData);
	}

	public override object GetData(string keyData, params object[] datas)
	{
		if (keyData == ConstValue.ActorData_Team)
			return TeamType;
		else if (keyData == ConstValue.ActorData_Character)
			return SelfCharacter;
		else if (keyData == ConstValue.ActorData_GetTarget)
			return TargetObject ;

		// Base 부모 클래스 -> BaseObject
		return base.GetData(keyData, datas);
	}

	public override void ThrowEvent(string keyData, params object[] datas)
	{
		if(keyData == ConstValue.EventKey_Hit)
		{
			if (OBJECT_STATE == eBaseObjectState.STATE_DIE)
				return;

			// 공격 주체의 케릭터
			GameCharacter casterCharacter = datas[0] as GameCharacter;

			double attackDamage = casterCharacter.CHARACTER_STATUS.GetStatusData(eStatusData.ATTACK);


			// 피격
			SelfCharacter.IncreaseCurrentHP(-attackDamage);
		}

		base.ThrowEvent(keyData, datas);
	}

	protected virtual void Update()
	{
		AI.UpdateAI();
		if(AI.END)
		{
			Destroy(SelfObject);
		}
	}

	private void OnDestroy()
	{
		if(ActorManager.Instance!= null)
		{
			ActorManager.Instance.RemoveActor(this);
		}

	}

	
}

