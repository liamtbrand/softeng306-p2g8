using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Creates project objects
public class ProjectCreator : Singleton<ProjectCreator> {

	Dictionary<string,Project> projects = new Dictionary<string,Project>();

	public Dictionary<string,Project> InitialiseProjects ()
	{
		/* TUTORIAL PROJECT */
		Project projectOne = new Project();
		projectOne.setTitle("Movie Theater Booking System");
		projectOne.setCompany("Bista Entertainment Solutions");
		projectOne.setDescription("Build a web app to handle the booking of movie tickets. Needs to support the issuance of free tickets to student software engineering clubs.");
		projectOne.setLength(7);
		projectOne.setDifficulty(ProjectDifficulty.Tutorial);
		projectOne.setEnabled(true);
		projects.Add(projectOne.getTitle(),projectOne);

		/* EASY PROJECTS */
		Project projectEight = new Project();
		projectEight.setTitle("Analysing Dependencies");
		projectEight.setCompany("NZ Game Developers Association");
		projectEight.setDescription("Create a Java application for extracting statistics from csv files containing information about dependencies.");
		projectEight.setLength(8);
		projectEight.setDifficulty(ProjectDifficulty.Easy);
		projects.Add(projectEight.getTitle(),projectEight);

		Project projectTwo = new Project();
		projectTwo.setTitle("High-performance Meeting-threading Library");
		projectTwo.setCompany("Sheehan Software");
		projectTwo.setDescription("Build a C library to assist the CEO in planning their day. Should support both Sequential and Concurrent meetings. ");
		projectTwo.setLength(10);
		projectTwo.setDifficulty(ProjectDifficulty.Easy);
		projects.Add(projectTwo.getTitle(),projectTwo);

		Project projectSix = new Project();
		projectSix.setTitle("Parolee System");
		projectSix.setCompany("Ian Law Enforcement Services");
		projectSix.setDescription("Build a web service for the New Zealand police to track parolees and their movements.");
		projectSix.setLength(12);
		projectSix.setDifficulty(ProjectDifficulty.Easy);
		projects.Add(projectSix.getTitle(),projectSix);

		/* MEDIUM PROJECTS */
		Project projectThree = new Project();
		projectThree.setTitle("Rail Freight Software");
		projectThree.setCompany("ERail");
		projectThree.setDescription("Build software to support the tracking & management of rail assets throughout New Zealand.");
		projectThree.setLength(14);
		projectThree.setDifficulty(ProjectDifficulty.Medium);
		projects.Add(projectThree.getTitle(),projectThree);

		Project projectNine = new Project();
		projectNine.setTitle("Smimpsons Facial Recognition");
		projectNine.setCompany("Fax Studios");
		projectNine.setDescription("Develop facial tracking software for identifying Smimpsons faces within the cartoon environment. The software should track when unrealistic faces are drawn into production.");
		projectNine.setLength(18);
		projectNine.setDifficulty(ProjectDifficulty.Medium);
		projects.Add(projectNine.getTitle(),projectNine);

		Project projectFour = new Project();
		projectFour.setTitle("Accounting Accountants");
		projectFour.setCompany("Zeroe");
		projectFour.setDescription("Help provide consultancy services to Zeroe, to aid them in holding their accountants accountable. ");
		projectFour.setLength(21);
		projectFour.setDifficulty(ProjectDifficulty.Medium);
		projects.Add(projectFour.getTitle(),projectFour);

		/* HARD PROJECTS */
		Project projectFive = new Project();
		projectFive.setTitle("Investment Software");
		projectFive.setCompany("Optimar");
		projectFive.setDescription("Build software to help Robert at Optimar calculate the WACC based on publicly available accounting information for various companies.");
		projectFive.setLength(30);
		projectFive.setDifficulty(ProjectDifficulty.Hard);
		projects.Add(projectFive.getTitle(),projectFive);

		Project projectSeven = new Project();
		projectSeven.setTitle("Serious Coders");
		projectSeven.setCompany("NZ Game Developers Association");
		projectSeven.setDescription("Create a serious game promoting diversity and inclusion within software engineering.");
		projectSeven.setLength(36);
		projectSeven.setDifficulty(ProjectDifficulty.Hard);
		projects.Add(projectSeven.getTitle(),projectSeven);

		Project projectTen = new Project();
		projectTen.setTitle("Surveillance Software");
		projectTen.setCompany("Palanco");
		projectTen.setDescription("Build a surveillance system that provides continual video recording and detects movements during closing hours.");
		projectTen.setLength(40);
		projectTen.setDifficulty(ProjectDifficulty.Hard);
		projects.Add(projectTen.getTitle(),projectTen);

		return projects;
	}

}
