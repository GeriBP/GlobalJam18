using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour 
{
	void Start()
	{
		FourPlayerM.points = new int[] { 0, 0, 0, 0 };
        FourPlayerM.rounds = 1;

		AudioManager.instance.Play ("Menu");
    }

	public void LoadScene(string scene)
	{
		AudioManager.instance.Stop ("Menu");
		//SceneManager.LoadScene (scene);
		SceneManager.LoadSceneAsync (scene);
	}

	public void Exit()
	{
		Debug.Log ("Bye!");
		Application.Quit ();
	}
}

