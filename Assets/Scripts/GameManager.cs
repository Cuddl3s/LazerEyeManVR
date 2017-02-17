using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public SpawnerManager SpawnerManager;
    public int upIntensityEvery;
    private int currentUpIntensityEvery;

    public static GameManager INSTANCE;

    private int destroyedCubes;
    public ParticleSystem explosion;
    private bool running;
    public bool isRunning
    {
        get { return running; }
    }

    private int multiplicator;
    private int currentPoints;
    public Text pointsText;
    public int points
    {
        get { return currentPoints; }
        set
        {
            currentPoints = value;
            
            pointsText.text = currentPoints.ToString("D6");
        }
    }

    // Use this for initialization
	void Start ()
	{
	    reset();
	    if (!INSTANCE) INSTANCE = this;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void targetDestroyed(Vector3 position)
    {
        points += multiplicator * 100;
        Instantiate(explosion, position, Quaternion.identity);
        if (++destroyedCubes % upIntensityEvery == 0)
        {
            currentUpIntensityEvery += 2;
            SpawnerManager.intensityUp();
            multiplicator++;
        }
        
    }

    public void startGame()
    {
        reset();
        SpawnerManager.start();
        running = true;
    }

    public void stopGame()
    {

        running = false;
        SpawnerManager.reset();
        ShooterPlayer.INSTANCE.reset();
        foreach (Target target in FindObjectsOfType<Target>())
        {
            Vector3 position = target.transform.position;
            Instantiate(explosion, position, Quaternion.identity);
            Destroy(target.gameObject);      
        }
    }

    void reset()
    {
        currentUpIntensityEvery = upIntensityEvery;
        running = false;
        destroyedCubes = 0;
        points = 0;
        multiplicator = 1;
    }

    public void playerHit()
    {
        multiplicator = 1;
    }
}
