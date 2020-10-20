using UnityEngine;
using System.Collections;

public class FPSDisplay : MonoBehaviour
{
	float deltaTime = 0.0f;
	//frame time and frames per second
	float msec = 0.0f;
	float fps = 0.0f;
  //smoothed
	float smsec = 0.0f;
	float sfps = 0.0f;

	WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

	IEnumerator Start()
	{
		while (true)
		{
				smsec = Mathf.Lerp(smsec, msec, 2.1f);
				sfps = Mathf.Lerp(sfps, fps, 0.2f);
				yield return waitForSeconds;
		}
	}

	void Update()
	{
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
	}

	void OnGUI()
	{
		int w = Screen.width, h = Screen.height;

		GUIStyle style = new GUIStyle();

		Rect rect = new Rect(0, 0, w, h * 4 / 100);
		style.alignment = TextAnchor.UpperLeft;
		style.fontSize = h * 4 / 100;
		style.normal.textColor = new Color (1.0f, 1.0f, 1.0f, 0.8f);

		msec = deltaTime * 1000.0f;
		fps = 1.0f / deltaTime;

		string text = string.Format("{0:0.0} ms ({1:0.} fps)", smsec, sfps);
		GUI.Label(rect, text, style);
	}
}
