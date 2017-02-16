using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    private Spawner[] spawners;
    private int amountOfSameTimeSpawns;
    public float timeBetweenSpawns;

    private float currentTime;
    private bool spawning;

    // Use this for initialization
	void Start ()
	{
	    spawners = GetComponentsInChildren<Spawner>();
	    amountOfSameTimeSpawns = 1;
	    if (timeBetweenSpawns == 0f)
	    {
	        timeBetweenSpawns = 3f;
	    }
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (spawning)
	    {
	        currentTime += Time.deltaTime;
	        if (currentTime >= timeBetweenSpawns)
	        {
	            currentTime = 0f;
	            startSpawners();
	        }
	    }
	}

    public void intensityUp()
    {
        amountOfSameTimeSpawns++;
    }

    private void startSpawners()
    {
        int i = amountOfSameTimeSpawns;
        while (i > 0)
        {
            int index = Mathf.FloorToInt(Random.value * (spawners.Length));
            
            spawners[index].spawn();
            i--;
            
        }
            
    }

    public void start()
    {
        spawning = true;
    }

    public void reset()
    {
        amountOfSameTimeSpawns = 1;
        spawning = false;
    }
}
