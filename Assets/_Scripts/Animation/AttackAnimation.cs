using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : StateMachineBehaviour
{

	// 첫 번째와 마지막 프레임을 제외하고 각 업데이트 프레임에서 호출됩니다.
	public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
	{
		if (animatorStateInfo.normalizedTime >= 1.0f)      // normalizedTime 1초단위로 만든다.
		{
			BaseObject bo = animator.GetComponentInParent<BaseObject>();	// 애니메이터를 갖고 있는 모델의 ㅁ부모인 Player로 찾아간다

			bo.ThrowEvent("AttackEnd", eStateType.STATE_IDLE);

		}
	}

}
