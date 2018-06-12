using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

	public string mainMenuLevel;

	public void Reset()
	{
		FindObjectOfType<PlayerControl> ().Reset ();
	}
	public void QuitToMain()
	{
	//	Application.LoadLevel (mainMenuLevel);
		SceneManager.LoadScene(0);
	}

}
