using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourPlayerM : MonoBehaviour {
    private PlayerMove[] pM;
    private int vegans, carnivores;
    public Sprite vSprite, cSprite;
    private int rounds = 0;
    private int[] points = {0,0,0,0};

    public static FourPlayerM InstanceFourPlayer;
	void Start ()
    {
        if (InstanceFourPlayer != null) Destroy(gameObject);
        InstanceFourPlayer = this;

        DontDestroyOnLoad(gameObject);

        vegans = 2;
        carnivores = 2;
        PrepareArray();
    }

    private void PrepareArray()
    {
        pM = new PlayerMove[4];
        PlayerMove[] player = GameObject.FindObjectsOfType<PlayerMove>();
        for (int i = 0; i < player.Length; i++)
        {
            pM[int.Parse(player[i].id) - 1] = player[i];
        }

        AssignTeams();

        for (int i = 0; i < pM.Length; i++)
        {
            Debug.Log(i + " : " + pM[i].id + " is he vegan? " + pM[i].isVegan);
        }
    }

    private void AssignTeams()
    {
        for (int i = 0; i < pM.Length; i++)
        {
            GoVegan(i, true);
        }

        int c1 = Random.Range(0, 4);
        int c2 = Random.Range(0, 4);
        while (c1 == c2)
        {
            c2 = Random.Range(0, 4);
        }
        GoCarnivore(c1, true);
        GoCarnivore(c2, true);
    }

    private void GoVegan(int i, bool begin)
    {
        pM[i].isVegan = true;
        pM[i].gameObject.layer = LayerMask.NameToLayer("Vegan");
        pM[i].gameObject.GetComponent<SpriteRenderer>().sprite = vSprite;
        if (!begin)
        {

        }
    }

    private void GoCarnivore(int i, bool begin)
    {
        pM[i].isVegan = false;
        pM[i].gameObject.layer = LayerMask.NameToLayer("Carnivore");
        pM[i].gameObject.GetComponent<SpriteRenderer>().sprite = cSprite;
        if (!begin)
        {

        }
    }

    public void ManageHit(int p1, int p2, bool vegan)
    {
        //TODO
        if (vegan)
        {
            ++vegans;
            --carnivores;
        }
    }
}
