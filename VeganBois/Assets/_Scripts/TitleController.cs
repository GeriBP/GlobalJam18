using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour 
{
	void Start()
	{
		FourPlayerM.points = new int[] { 0, 0, 0, 0 };
        FourPlayerM.rounds = 0;
    }

	public void LoadScene(string scene)
	{
		SceneManager.LoadScene (scene);
	}

	public void Exit()
	{
		Debug.Log ("Bye!");
		Application.Quit ();
	}
}

