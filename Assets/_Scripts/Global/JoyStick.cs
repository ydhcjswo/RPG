using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyStick : BaseObject
{
	//------------------------------------------
	// 싱글톤 :: 조이스틱 1개 기준
	static JoyStick _instance = null;
	public static JoyStick Instance
	{
		get { return _instance; }
	}

	private void Awake()
	{
		_instance = this;
	}
	//------------------------------------------

	public Camera UI_Camera;
	public bool NormalizedPower = false;

	public bool IsKeyBoardInput = false;
	public bool IsPresed = false;

	private Vector2 _Axis;
	public Vector2 Axis
	{
		get
		{
			if (IsPresed)
				return _Axis;
			else
				return Vector2.zero;
		}
	}

	private int TouchID = -10;
	public Transform PointerTrans;

	private Vector3 CenterPosition;
	private Vector3 InnerPosition;

	private float Radius = 60.0f;
	private float InnerRadius = 10.0f;

	private void OnEnable()
	{
		UI_Camera = UICamera.mainCamera;
		if (!UI_Camera)  // UI_Camera == null
		{
			Debug.LogError("JoyStick에 UI 카메라를 찾지 못했습니다.");
			return;
		}

		CenterPosition = UI_Camera.WorldToScreenPoint(this.SelfTransform.position); // 조이스틱 센터와터치위치 비교하기

		UIWidget widget = this.SelfComponent<UIWidget>();
		Radius = widget.width * 0.5f;
		InnerRadius = PointerTrans.gameObject.GetComponent<UIWidget>().width * 0.5f;
	}

#if UNITY_ANDROID
		IsKeyBoardInput = false;
		
#endif


	void OnPress(bool Pressed)		// NGUI 가 실행시켜주는 함수들
	{
		Debug.Log("Press");
		if (IsKeyBoardInput)
			return;

		if (Pressed)// 눌려짐
		{
			IsPresed = true;
			TouchID = UICamera.currentTouchID;
			InnerPosition = UICamera.currentTouch.pos;
		}
		else //안눌려짐
		{
			IsPresed = false;
			InnerPosition = CenterPosition;
		}
		Movement();
	}

	private void OnDrag()
	{
		if (IsPresed)
		{
			InnerPosition = UICamera.currentTouch.pos;
			Movement();
		}
	}

	void Movement()
	{
		Vector2 MovePosition = InnerPosition - CenterPosition;  // 원점에서 터치한 위치까지 벡터

		if (MovePosition.magnitude < Radius * 0.1f)
		{
			MovePosition = Vector3.zero;
		}
		else if(MovePosition.magnitude>=Radius-InnerRadius/2)
		{
			MovePosition = (MovePosition.normalized * (Radius-InnerRadius/2));
		}
		PointerTrans.localPosition = MovePosition;

		if (NormalizedPower)
			MovePosition = MovePosition.normalized * Radius;

		_Axis.x = MovePosition.x / Radius;
		_Axis.y = MovePosition.y / Radius;
	}

#if UNITY_EDITOR        // 유니티 내에서만 실행됨
	private void Update()
	{
		Vector3 movePosition = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if(movePosition != Vector3.zero)
		{
			IsKeyBoardInput = true;
			IsPresed = true;
			InnerPosition = CenterPosition + movePosition * Radius;
			Movement();
		}
		else
		{
			if (IsKeyBoardInput == true)
			{
				IsPresed = false;
				IsKeyBoardInput = false;
			}
		}

	}
#endif


}
