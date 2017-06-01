using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Actor
{
	Animator Anim;
	JoyStick Stick;

	DetectionArea DetectArea;
	float AttackRange = 3.0f;

	void Start()
	{
		Stick = JoyStick.Instance;
		Anim = this.GetComponentInChildren<Animator>();

		DetectArea = SelfObject.GetComponentInChildren<DetectionArea>();
		if (DetectArea == null)
		{
			Debug.Log("DetectionArea 가 없어서 생성.");
			GameObject go = new GameObject(typeof(DetectionArea).ToString(), typeof(DetectionArea));
			go.transform.SetParent(SelfTransform);
			go.transform.localPosition = Vector3.zero;
			DetectArea = go.GetComponent<DetectionArea>();
		}
		DetectArea.Init(this, this.AttackRange);
	}


	protected override void Update()
	{
		if (Stick.IsPresed)
		{
			// #Transform Move
			//Vector2 Axis = JoyStick.Instance.Axis;
			//Vector3 pos = new Vector3(Axis.x, 0, Axis.y);
			//SelfTransform.position += pos * Time.deltaTime * 10;

			Vector2 Axis = Stick.Axis;
			Vector3 MovePosition = transform.position;
			MovePosition += new Vector3(Axis.x, 0, Axis.y);
			this.SelfComponent<NavMeshAgent>().SetDestination(MovePosition);
			ChangeState(eStateType.STATE_WALK);
			//SetAnimation(eStateType.STATE_WALK);
		}
		else
			ChangeState(eStateType.STATE_IDLE);

		if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
		{
			ChangeState(eStateType.STATE_ATTACK);
			//SetAnimation(eStateType.STATE_ATTACK);
		}
	}

	public void SetAnimation(eStateType state)
	{
		Anim.SetInteger("State", (int)state);
	}

	public override void ThrowEvent(string keyData, params object[] datas)
	{
		switch (keyData)
		{
			case "AttackEnd":
				{
					SetAnimation((eStateType)datas[0]);         // object로 받아왔기 때문에 사용할려면 필요한 자료형으로 캐스팅
				}
				break;

			default:
				{
					base.ThrowEvent(keyData, datas);
				}
				break;
		}

		base.ThrowEvent(keyData, datas);

	}

	// Test Code(수정예정)
	eStateType State = eStateType.STATE_IDLE;
	void ChangeState(eStateType type)
	{
		if (State == type)
			return;

		State = type;
		SetAnimation(State);

		switch (State)
		{
			case eStateType.STATE_IDLE:
				break;
			case eStateType.STATE_ATTACK:
				{
					// DetectionArea
					Actor actor = DetectArea.GetFirst();
					if (actor != null)
					{
						Vector3 dir = actor.SelfTransform.position - SelfTransform.position;

						dir.Normalize();

						SelfTransform.rotation = Quaternion.LookRotation(dir);
						actor.SelfTransform.rotation = Quaternion.LookRotation(-dir);

						actor.ThrowEvent(ConstValue.EventKey_Hit, SELF_CHARACTER);
					}

				}
				break;
			case eStateType.STATE_WALK:
				break;
			case eStateType.STATE_DEAD:
				break;
		}
	}

}

