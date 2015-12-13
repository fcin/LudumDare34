using UnityEngine;
using System.Collections;

public class ObstaclePusher : MonoBehaviour {

    public float WindSpeed;
	
	void Update () {

		this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * WindSpeed);
	}
}
