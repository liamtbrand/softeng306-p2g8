﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectCreator : Singleton<ProjectCreator> {

	Dictionary<string,Project> projects = new Dictionary<string,Project>();

	public Dictionary<string,Project> InitialiseProjects ()
	{
		// Project One
		Project projectOne = new Project();
		projectOne.setTitle("Movie Theater Booking System");
		projectOne.setCompany("Bista Entertainment Solutions");
		projectOne.setDescription("Build a web app to handle the booking of movie tickets. Needs to support the issuance of free tickets to student software engineering clubs.");
		projectOne.setLength(7);
		projectOne.setDifficulty(ProjectDifficulty.EASY);
		projects.Add(projectOne.getTitle(),projectOne);

		// Project Two
		Project projectTwo = new Project();
		projectTwo.setTitle("Smart Campus Software");
		projectTwo.setCompany("Quality Courses");
		projectTwo.setDescription("Build software to calculate university course quality metrics, to help determine rankings.");
		projectTwo.setLength(10);
		projectTwo.setDifficulty(ProjectDifficulty.EASY);
		projects.Add(projectTwo.getTitle(),projectTwo);

		// Project Three
		Project projectThree = new Project();
		projectThree.setTitle("Rail Freight Software");
		projectThree.setCompany("ERail");
		projectThree.setDescription("Build software to support the tracking & management of rail assets throughout New Zealand.");
		projectThree.setLength(14);
		projectThree.setDifficulty(ProjectDifficulty.MEDIUM);
		projects.Add(projectThree.getTitle(),projectThree);

		// Project Four
		Project projectFour = new Project();
		projectFour.setTitle("Accounting Accountants");
		projectFour.setCompany("Zeroe");
		projectFour.setDescription("Help provide consultancy services to Zeroe, to aid them in holding their accountants accountable. ");
		projectFour.setLength(21);
		projectFour.setDifficulty(ProjectDifficulty.MEDIUM);
		projects.Add(projectFour.getTitle(),projectFour);

		// Project Five
		Project projectFive = new Project();
		projectFive.setTitle("Investment Software");
		projectFive.setCompany("Optimar");
		projectFive.setDescription("Build software to help Robert at Optimar calculate the WACC based on publicly available accounting information for various companies, in order to better inform investment decisions.");
		projectFive.setLength(30);
		projectFive.setDifficulty(ProjectDifficulty.HARD);
		projects.Add(projectFive.getTitle(),projectFive);

		return projects;
	}

}
