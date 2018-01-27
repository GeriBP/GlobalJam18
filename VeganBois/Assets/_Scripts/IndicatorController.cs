using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
	public SpriteRenderer sprite;
	public Color[] colors;

	void Start ()
	{
		int id = int.Parse (GetComponent<PlayerMove> ().id);
		sprite.color = colors [id - 1];
	}
}
