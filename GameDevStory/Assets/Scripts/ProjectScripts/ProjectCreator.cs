using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates project objects
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
		projectOne.setDifficulty(ProjectDifficulty.Tutorial);
		projectOne.setEnabled(true);
		projects.Add(projectOne.getTitle(),projectOne);

		// Project Two
		Project projectTwo = new Project();
		projectTwo.setTitle("High-performance Meeting-threading Library");
		projectTwo.setCompany("Sheehan Software");
		projectTwo.setDescription("Build a C library to assist the CEO in planning their day. Should support both Sequential and Concurrent meetings. ");
		projectTwo.setLength(10);
		projectTwo.setDifficulty(ProjectDifficulty.Easy);
		projects.Add(projectTwo.getTitle(),projectTwo);

		// Project Three
		Project projectThree = new Project();
		projectThree.setTitle("Rail Freight Software");
		projectThree.setCompany("ERail");
		projectThree.setDescription("Build software to support the tracking & management of rail assets throughout New Zealand.");
		projectThree.setLength(14);
		projectThree.setDifficulty(ProjectDifficulty.Medium);
		projects.Add(projectThree.getTitle(),projectThree);

		// Project Four
		Project projectFour = new Project();
		projectFour.setTitle("Accounting Accountants");
		projectFour.setCompany("Zeroe");
		projectFour.setDescription("Help provide consultancy services to Zeroe, to aid them in holding their accountants accountable. ");
		projectFour.setLength(21);
		projectFour.setDifficulty(ProjectDifficulty.Medium);
		projects.Add(projectFour.getTitle(),projectFour);

		// Project Five
		Project projectFive = new Project();
		projectFive.setTitle("Investment Software");
		projectFive.setCompany("Optimar");
		projectFive.setDescription("Build software to help Robert at Optimar calculate the WACC based on publicly available accounting information for various companies.");
		projectFive.setLength(30);
		projectFive.setDifficulty(ProjectDifficulty.Hard);
		projects.Add(projectFive.getTitle(),projectFive);

		return projects;
	}

}
