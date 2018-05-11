using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	//public Sprite bg;

	public void Start()
	{
	//	bg = gameObject.GetComponentInParent<Sprite>();
	}

	public void PlayGame (){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
	public void QuitGame(){
		Debug.Log ("Quit!");
		Application.Quit ();
	}
}
