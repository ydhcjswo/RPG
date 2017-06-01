using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
	void Start () 
	{
		GameObject Player = GameObject.Find("Player");
		Actor PlayerScripts = Player.GetComponent<Actor>();
		CameraManager.Instance.CameraInit(PlayerScripts);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
