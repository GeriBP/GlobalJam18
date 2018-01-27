using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour 
{

	void Start()
	{
		FourPlayerM.points = new int[] { 0, 0, 0, 0 };
	}

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

