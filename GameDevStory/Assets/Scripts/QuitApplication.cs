using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour {
	public void Quit() {
		// Do things before quitting (if applicable)
		// Don't use this on iOS (breaks Apple's dev guidelines, causes a crash instead of proper springboard anim)
		Application.Quit();
	}
}
