using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        print("Expl. Start() Called");
	    Invoke("Destroy", 2.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Destroy()
    {
        print("DESTROY");
        Destroy(gameObject);
    }
}
