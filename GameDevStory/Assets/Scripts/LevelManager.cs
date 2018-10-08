using UnityEngine;
using System;
using System.Collections.Generic;       //Allows us to use Lists.
using Random = UnityEngine.Random;      //Tells Random to use the Unity Engine random number generator.

// Instantiates the game level by setting the background, desks and NPC coordinates
public class LevelManager : Singleton<LevelManager> {
      
    public Level[] levels;                                       	// Array of level prefabs.
    public int level = 0;                                           // Default to no level.

    // Retreive the level object for the current level.
    public Level GetCurrentLevel()
    {
        if (level == -1)
            return null;
        return levels[level];
    }

    // This should be called to start the first level
    public void SwitchToFirstLevel()
    {
        SwitchToLevel(level);
    }

    // Initialises the new level, shuts down the old level if there was one.
    public void SwitchToLevel(int newlevel)
    {
        Level currentLevel = GetCurrentLevel();
        if(currentLevel != null)
        {
            currentLevel.TearDownLevel();
        }
        // set the new level
        level = newlevel;
        GetCurrentLevel().SetupLevel();
    }
}
