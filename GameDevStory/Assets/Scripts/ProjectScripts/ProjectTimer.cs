using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectTimer : MonoBehaviour {

	public GameObject progressPanel;
	public Slider progressBar;
	private float maxTime = 10f;
	private float timer;
	private float progress;
	public Text progressText;

	// Use this for initialization
	void OnEnable () {
		progressPanel.SetActive(true);
		timer = 10f;
		progressBar.value = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		progress = (maxTime-timer)*10;
		progressText.text = progress.ToString("f0")+"%";
		progressBar.value = progress/100;

		if (timer <= 0) {
			progressText.text = "100%";
			progressPanel.SetActive(false);
		}	
	}
}
