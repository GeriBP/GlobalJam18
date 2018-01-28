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

		AudioManager.instance.Play ("menu_music");
    }

	public void LoadScene(string scene)
	{
		AudioManager.instance.Stop ("menu_music");
		AudioManager.instance.Play ("click_button");
		//SceneManager.LoadScene (scene);
		SceneManager.LoadSceneAsync (scene);
	}

	public void Exit()
	{
		Debug.Log ("Bye!");
		AudioManager.instance.Play ("click_button");
		Application.Quit ();
	}
}

