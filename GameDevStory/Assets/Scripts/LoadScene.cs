using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {

	public void LoadByIndex(int index) {
		SceneManager.LoadScene(index);
	}

	public void LoadMainMenu()
	{
		//Destroy(GameManager.Instance.gameObject);
		SceneManager.LoadScene("MainMenu");
	}

	public void LoadPrototypeScene()
	{
		SceneManager.LoadScene("PrototypeScene");
	}

	public void LoadHighScoreScene()
	{
		DontDestroyOnLoad(GameManager.Instance);
		SceneManager.LoadScene("HighScoreTestScene");
	}

}
