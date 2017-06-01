using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : Actor
{
	Transform Target = null;
	MonsterRegenerator Generator;
	public override void ThrowEvent(string keyData, params object[] datas)
	{
		switch (keyData)
		{
			case ConstValue.EventKey_EnemyInit:
				{
					Generator = datas[0] as MonsterRegenerator;
				}
				break;

			default:
				{
					base.ThrowEvent(keyData, datas);
				}
				break;
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.name.Equals("Player"))
		{
			Target = other.transform;
			StartCoroutine("FollowTarget");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.name.Equals("Player"))
		{
			Target = null;
			StopCoroutine("FollowTarget");
		}
	}

	IEnumerator FollowTarget()
	{

		while (Target != null)
		{
			SelfComponent<NavMeshAgent>().Resume(); // resume 재동작
			SelfComponent<NavMeshAgent>().SetDestination(Target.position);

			yield return new WaitForSeconds(0.3f);
		}
		yield return null;
	}

}
