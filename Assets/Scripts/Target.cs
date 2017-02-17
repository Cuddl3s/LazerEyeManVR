using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Target : MonoBehaviour
{
    public float forceAdjust;
    private Vector3 toPlayer;

    public enum HitType
    {
        GoIn, On
    }

    // Use this for initialization
    void Start ()
	{
        transform.localPosition = Random.onUnitSphere * Random.Range(10f, 30f);
        toPlayer = (Camera.main.transform.position - transform.position).normalized;

	    transform.rotation = Quaternion.FromToRotation(transform.forward, toPlayer) * transform.rotation;
        Invoke("shoot", 2);

    }
	
	// Update is called once per frame
	void Update () {
       
    }

    void shoot()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * forceAdjust, ForceMode.VelocityChange);
    }

    public void hitTarget()
    {
        
            destroy();
        
            
    }

    public void hitDelayed()
    {
        StartCoroutine(ExecuteAfterTime(0.05f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        destroy();
    }

    void destroy()
    {
        if (ShooterPlayer.INSTANCE.shooting)
        {
            GameManager.INSTANCE.targetDestroyed(transform.position);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        GameManager.INSTANCE.targetDestroyed(transform.position);
        Destroy(gameObject);
        ShooterPlayer.INSTANCE.getHit();
    }
}
