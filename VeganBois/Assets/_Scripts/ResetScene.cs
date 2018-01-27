using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour {

	public string sceneName;

	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.R) || Input.GetButtonDown ("1Back"))
			SceneManager.LoadScene (sceneName);
	}
}
