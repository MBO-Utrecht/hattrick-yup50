using UnityEngine;
using System.Collections;

public class HT_Explode : MonoBehaviour {

	public GameObject explosion;
	public ParticleSystem[] effects;
	private HT_GameController GM;

	private void Start()
	{
		GM = GameObject.FindObjectOfType<HT_GameController>();
	}
	
	void OnCollisionEnter2D (Collision2D collision) {
		if (collision.gameObject.tag == "Hat") {
			Instantiate (explosion, transform.position, transform.rotation);
			GM.timeLeft = 0;
			foreach (var effect in effects) {
				effect.transform.parent = null;
				effect.Stop ();
				Destroy (effect.gameObject, 1.0f);
			}
			GM.NextRound = false;
			Destroy (gameObject);
		}
	}
}
