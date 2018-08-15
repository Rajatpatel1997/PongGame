using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
	public float speed;
	public Text result;
	public Button restart;

	// Use this for initialization
	void Start () {

		GetComponent<Rigidbody2D> ().velocity = Vector2.right * speed;
		result.text = "";
		speed = 30;
		restart.gameObject.SetActive (false);
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		// Note: 'col' holds the collision information. If the
		// Ball collided with a racket, then:
		//   col.gameObject is the racket
		//   col.transform.position is the racket's position
		//   col.collider is the racket's collider

		// Hit the left Racket?
		if (col.gameObject.name == "RacketLeft") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
				col.transform.position,
				col.collider.bounds.size.y);

			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(1, y).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
		}

		// Hit the right Racket?
		if (col.gameObject.name == "RacketRight") {
			// Calculate hit Factor
			float y = hitFactor(transform.position,
				col.transform.position,
				col.collider.bounds.size.y);

			// Calculate direction, make length=1 via .normalized
			Vector2 dir = new Vector2(-1, y).normalized;

			// Set Velocity with dir * speed
			GetComponent<Rigidbody2D>().velocity = dir * speed;
		}

		if (col.gameObject.name == "WallLeft") {
		
			result.text = "Player 2 Win!!";
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0).normalized * 0;
			restart.gameObject.SetActive (true);
		}

		if (col.gameObject.name == "WallRight") {

			result.text = "Player 1 Win!!";
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0).normalized * 0;
			restart.gameObject.SetActive (true);
		}
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos,
		float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}
}
