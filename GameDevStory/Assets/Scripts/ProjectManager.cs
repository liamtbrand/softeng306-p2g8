using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ProjectStatus
{
    NONE, COMPLETE, IN_PROGRESS
}

public class ProjectManager : MonoBehaviour {

	private ProjectStatus status = ProjectStatus.NONE;
	private ProjectTimer timerScript;
	private GameManager gameScript;

	public Button StartButton;

	public void StartProject ()
	{
		status = ProjectStatus.IN_PROGRESS;
		//timerScript = GetComponent <ProjectTimer>();

	}

}