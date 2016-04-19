using UnityEngine;
using System.Collections;

public class SkyboxController : MonoBehaviour {

	public Material[] skyboxes;
	public int currentSkybox = 0;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.X)) {
			Debug.Log ("Change skybox");
			currentSkybox++;
			if (currentSkybox == skyboxes.Length) {
				currentSkybox = 0;
			}

			var skybox = GetComponent<Skybox>();
			skybox.material = skyboxes [currentSkybox];
		}
	}
}
