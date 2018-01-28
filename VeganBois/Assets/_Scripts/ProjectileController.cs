using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour {

	Rigidbody2D rb;
	public bool isVegan;
	public int playerId;

	SpriteRenderer sprite;
	public Sprite[] veggieSprites;
	public Sprite[] meatSprites;

	public GameObject bonesPS, flowersPS;

    public Color veganC, carnC;
    public SpriteRenderer glow;

    public float minMagnitude, time2Check, tScale, smoothS;
	void Start ()
	{
		rb = GetComponent<Rigidbody2D> ();

		sprite = GetComponent<SpriteRenderer> ();
		if (isVegan)
        {
            sprite.sprite = veggieSprites[Random.Range(0, veggieSprites.Length)];
            glow.color = veganC;
        }
        else
        {
            sprite.sprite = meatSprites[Random.Range(0, meatSprites.Length)];
            glow.color = carnC;
        }

		InvokeRepeating ("checkSpeed", 1, 1);

		transform.rotation = Quaternion.Euler (0, 0, Random.Range (0.0f, 360.0f));
	}
	
	void checkSpeed()
	{
		if (rb.velocity.sqrMagnitude <= minMagnitude)
		{
            Invoke("ReCheck", time2Check);
		}
	}

    void ReCheck()
    {
        if (rb.velocity.sqrMagnitude <= minMagnitude)
        {
            StartCoroutine(KillMe());
        }
    }

	void Die()
	{
		Destroy (gameObject);
	}

    IEnumerator KillMe()
    {
        while (transform.localScale.x > tScale)
        {
            float x = Mathf.Lerp(transform.localScale.x, 0.0f, smoothS);
            float y = Mathf.Lerp(transform.localScale.y, 0.0f, smoothS); ;
            transform.localScale = new Vector3(x, y, 1.0f);
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }

	void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("Player") && other.gameObject.GetComponent<PlayerMove> ().isVegan != isVegan)
		{
			FourPlayerM manager = FindObjectOfType<FourPlayerM> ();
			//if (!manager)
				// Two player manager
			PlayerMove player = other.gameObject.GetComponent<PlayerMove> ();

			if (player.isVegan)
			{
				AudioManager.instance.Play ("hit_vegan");
				GameObject g = Instantiate (bonesPS, other.transform.position, Quaternion.identity);
				Destroy (g, 2);
			} 
			else
			{
				AudioManager.instance.Play ("hit_carnist");
				GameObject g = Instantiate (flowersPS, other.transform.position, Quaternion.identity);
				Destroy (g, 2);
			}
			
			if (manager != null)
				manager.ManageHit (playerId, int.Parse (player.id), isVegan);
			
			Die ();
		}
	}
}
