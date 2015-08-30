using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public float timer;

	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;

		if (timer <= 0) {
			timer = 0;

			Debug.Log("Dead");
			Application.LoadLevel("Level 1");
		}
	}

	void OnGUI(){
		GUI.Box (new Rect(10, 10, 50, 20), "" + timer.ToString("00.00"));
	}
}
