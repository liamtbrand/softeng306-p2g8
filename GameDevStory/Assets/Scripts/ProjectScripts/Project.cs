﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectDifficulty
{
    Easy,Medium,Hard
}

public class Project {

	private string title;
	private string company;
	private string description;
	private int length;
	private ProjectDifficulty difficulty;
	private bool enabled = false;
	private bool completed = false;

	public Project ()
	{
		
	}

	public Project (string pTitle, string cName, string pDescription, int pLength, ProjectDifficulty level)
	{
		title = pTitle;
		company = cName;
		description = pDescription;
		length = pLength;
		difficulty = level;
	}

	public void setTitle (string ptitle)
	{
		title = ptitle;
	}

	public void setCompany (string comp)
	{
		company = comp;
	}

	public void setDescription (string desc)
	{
		description = desc;
	}

	public void setLength (int len)
	{
		length = len;
	}

	public void setDifficulty (ProjectDifficulty diff)
	{
		difficulty = diff;
	}

	public void setEnabled (bool en)
	{
		enabled = en;
	}

	public void setCompleted (bool complete)
	{
		completed = complete;
	}

	public string getTitle ()
	{
		return title;
	}

	public string getCompany ()
	{
		return company;
	}

	public string getDescription ()
	{
		return description;
	}

	public int getLength ()
	{
		return length;
	}

	public ProjectDifficulty getDifficulty ()
	{
		return difficulty;
	}

	public bool getEnabled ()
	{
		return enabled;
	}

	public bool getCompleted ()
	{
		return completed;
	}

	public string getStats()
	{
		return "Length: " + length + "\nDifficulty: " + difficulty.ToString();
	}
	
}
