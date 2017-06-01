using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAI : BaseAI
{
	protected override IEnumerator Idle()
	{
		// 근거리 적 탐색
		BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);


		//// Test Code ---------------------------------------------------------
		//float targetDistance = 0;
		//BaseObject targetObject_2 = ActorManager.Instance.GetSearchEnemy(Target, out targetDistance);
		//// Test Code ---------------------------------------------------------


		if (targetObject != null)
		{
			// 스킬로 대체 예정
			float attackRange = 3f;
			float distance = Vector3.Distance(
				targetObject.SelfTransform.position, SelfTransform.position);

			if (distance < attackRange)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
				AddNextAI(eStateType.STATE_WALK);

		}
		yield return StartCoroutine(base.Idle());	// 이줄에 멈춰서 코루틴이 끝나길 기다린다.
	}
	protected override IEnumerator Move()
	{
		BaseObject targetObject = ActorManager.Instance.GetSearchEnemy(Target);

		if(targetObject != null)
		{
			// 스킬로 대체 예정
			float attackRange = 0;

			float distance = Vector3.Distance(targetObject.SelfTransform.position, SelfTransform.position);

			if(distance<attackRange)
			{
				Stop();
				AddNextAI(eStateType.STATE_ATTACK, targetObject);
			}
			else
			{
				SetMove(targetObject.SelfTransform.position);
			}
		}
		yield return StartCoroutine(base.Move());
	}
	protected override IEnumerator Attack()
	{
		yield return StartCoroutine(base.Attack());
	}
	protected override IEnumerator Die()
	{
		yield return StartCoroutine(base.Die());
	}
	

}
