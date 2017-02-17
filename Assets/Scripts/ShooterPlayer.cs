using UnityEngine;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using NUnit.Framework.Internal;
using UnityEngine.UI;
using VolumetricLines;

public class ShooterPlayer : MonoBehaviour
{

    public bool shooting;
    public float maxEnergy;
    public float currentEnergy;
    public float energyLossPerSecond;
    public bool overHeat;
    public GlassesManager GlassesManager;
    public AudioSource beam;
    public AudioSource heartBeat;
    public AudioSource grunt;
    public Transform body;
    public static ShooterPlayer INSTANCE;
    public RawImage energyBar;
    private float initialEnergyBarWidth;
    public int maxlifes;
    private int lifes;

    public VolumetricLineBehavior VolumetricLineBehavior;
    private VolumetricLineBehavior left;
    private VolumetricLineBehavior right;



    // Use this for initialization
    void Start ()
	{
	    shooting = false;
	    currentEnergy = maxEnergy;
	    overHeat = false;
	    initialEnergyBarWidth = energyBar.rectTransform.rect.width;
	    if (!INSTANCE)
	    {
	        INSTANCE = this;
	    }

	    if (maxlifes == 0)
	    {
	        lifes = 3;
	    }
	    else
	    {
	        lifes = maxlifes;
	    }
        left = (VolumetricLineBehavior)Instantiate(VolumetricLineBehavior, GlassesManager.transform, false);
        left.SetLinePropertiesAtStart = true;
        left.StartPos = 1.5f * Vector3.left;
        left.EndPos = Vector3.forward * 150;
        right = (VolumetricLineBehavior)Instantiate(VolumetricLineBehavior, GlassesManager.transform, false);
        right.SetLinePropertiesAtStart = true;
        right.StartPos = 1.5f * Vector3.right;
        right.EndPos = Vector3.forward * 150;
	    
	    
	    left.LineWidth = 1;
	    right.LineWidth = 1;
	    left.LineColor = Color.red;
	    right.LineColor = Color.red;
        left.gameObject.SetActive(false);
        right.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update ()
	{
	    body.transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
        if (GameManager.INSTANCE.isRunning)
	    {
            if (GvrViewer.Instance.Triggered || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
            {
                //if (!shooting)
                //{
                if (!overHeat)
                {
                    shoot();
                }
//                }
//                else
//                {
                    
//                }
            }
            else if (GvrViewer.Instance.Triggered || Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Ended && shooting)
            {
                stopShooting(true);
            }
            if (shooting)
            {
                currentEnergy -= Time.deltaTime * energyLossPerSecond;
                updateEnergyBar();
                if (currentEnergy <= 0)
                {
                    stopShooting(false);
                }
            }
            else
            {
                if (currentEnergy < maxEnergy)
                {
                    currentEnergy += Time.deltaTime * energyLossPerSecond * 0.75f;
                    updateEnergyBar();
                }
                else if (overHeat)
                {
                    overHeat = false;
                }
            }
        }
	}

    void shoot()
    {
        GlassesManager.open();
        left.gameObject.SetActive(true);
        right.gameObject.SetActive(true);
        beam.Play();
        beam.loop = true;
        shooting = true;
    }


    void updateEnergyBar()
    {
        energyBar.rectTransform.sizeDelta = new Vector2((currentEnergy / maxEnergy) * initialEnergyBarWidth,
            energyBar.rectTransform.rect.height);

    }

    void stopShooting(bool clicked)
    {

            GlassesManager.close();
            beam.Stop();
            left.gameObject.SetActive(false);
            right.gameObject.SetActive(false);
            shooting = false;
            overHeat = !clicked;
         
    }

    public void getHit()
    {
        grunt.Play();
        lifes--;
        if (lifes == 1)
        {
            heartBeat.loop = true;
            heartBeat.Play();
        }
        else if (lifes == 0)
        {
            //Stop game;
            heartBeat.Stop();
            GameManager.INSTANCE.stopGame();
            
        }
    }

    public void reset()
    {
        if (shooting)
        {
            stopShooting(true);
        }
        lifes = maxlifes;
    }
}
