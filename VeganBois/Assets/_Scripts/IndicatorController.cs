using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
	public SpriteRenderer indicator;
	public SpriteRenderer target;
    public SpriteRenderer outline;
    public Color[] colors;
    public float alpha;

	void Start ()
	{
		int id = int.Parse (GetComponent<PlayerMove> ().id);
		indicator.color = colors [id - 1];
		target.color = colors [id - 1];
        outline.color = new Color(colors[id - 1].r, colors[id - 1].g, colors[id - 1].b, alpha);
    }
}
