using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour 
{
	public void Play1v1()
	{
		SceneManager.LoadScene ("Janster");
	}

	public void Play2v2()
	{
		SceneManager.LoadScene ("Janster");
	}

	public void Exit()
	{
		Debug.Log ("Bye!");
		Application.Quit ();
	}
}

