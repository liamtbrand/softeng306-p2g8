using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectDifficulty
{
    Tutorial,Easy,Medium,Hard
}

// Object representing a project
public class Project {

	private string title;
	private string company;
	private string description;
	private int length;
	private ProjectDifficulty difficulty;
	private bool enabled = false;
	private int minWorkers;

	public Project () { }

	public Project (string title, string company, string description, int length, 
		ProjectDifficulty difficulty, int minWorkers)
	{
		this.title = title;
		this.company = company;
		this.description = description;
		this.length = length;
		this.difficulty = difficulty;
		this.minWorkers = minWorkers;
	}

	public void setTitle (string title)
	{
		this.title = title;
	}

	public void setCompany (string company)
	{
		this.company = company;
	}

	public void setDescription (string description)
	{
		this.description = description;
	}

	public void setLength (int length)
	{
		this.length = length;
	}

	public void setDifficulty (ProjectDifficulty difficulty)
	{
		this.difficulty = difficulty;
	}

	public void setEnabled (bool enabled)
	{
		this.enabled = enabled;
	}

	public void setMinWorkers (int minWorkers)
	{
		this.minWorkers = minWorkers;
	}

	public string getTitle ()
	{
		return this.title;
	}

	public string getCompany ()
	{
		return this.company;
	}

	public string getDescription ()
	{
		return this.description;
	}

	public int getLength ()
	{
		return this.length;
	}

	public ProjectDifficulty getDifficulty ()
	{
		return this.difficulty;
	}

	public bool getEnabled ()
	{
		return this.enabled;
	}

	public int getMinWorkers ()
	{
		return this.minWorkers;
	}

	// Formats strings for stats section of the project menu
	public string getStats()
	{
		if (this.difficulty == ProjectDifficulty.Tutorial)
		{
			return "Length: " + this.length + "\nDifficulty: Easy \nMinimum workers: " + this.minWorkers;
		}
		else
		{
			return "Length: " + length + "\nDifficulty: " + difficulty.ToString() + "\nMinimum workers: " + this.minWorkers;
		}
	}
}
