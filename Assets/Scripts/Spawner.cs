
using UnityEngine;


public class Spawner : MonoBehaviour
{

    public Target targetPrefab;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void spawn()
    {
        Instantiate(targetPrefab, 
             transform);
    }
}
