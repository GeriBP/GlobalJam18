using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
	public SpriteRenderer indicator;
	public SpriteRenderer target;
	public Color[] colors;

	void Start ()
	{
		int id = int.Parse (GetComponent<PlayerMove> ().id);
		indicator.color = colors [id - 1];
		target.color = colors [id - 1];
	}
}
