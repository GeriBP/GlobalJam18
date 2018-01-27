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
            GoVegan(i);
        }

        int c1 = Random.Range(0, 4);
        int c2 = Random.Range(0, 4);
        while (c1 == c2)
        {
            c2 = Random.Range(0, 4);
        }
        GoCarnivore(c1);
        GoCarnivore(c2);
    }

    private void GoVegan(int i)
    {
        pM[i].isVegan = true;
        pM[i].gameObject.layer = LayerMask.NameToLayer("Vegan");
        pM[i].gameObject.GetComponent<SpriteRenderer>().sprite = vSprite;
    }

    private void GoCarnivore(int i)
    {
        pM[i].isVegan = false;
        pM[i].gameObject.layer = LayerMask.NameToLayer("Carnivore");
        pM[i].gameObject.GetComponent<SpriteRenderer>().sprite = cSprite;
    }

    public void ManageHit(int p1, int p2, bool vegan)
    {
        if (vegan)
        {
            //points
            points[p1 - 1] = carnivores * 10;
            //Animation
            //pM[p2 - 1].Transformation();
            GoVegan(p2 - 1);
            ++vegans;
            --carnivores;
        }
        else
        {
            //points
            points[p1 - 1] = vegans * 10;
            //Animation
            //pM[p2 - 1].Transformation();
            GoCarnivore(p2 - 1);
            --vegans;
            ++carnivores;
        }
        CheckEndRound();
    }

    void CheckEndRound()
    {
        if (carnivores >= 4 || vegans >= 4)
        {
            //END thiiis
            //Animation
            //Invoke scene load
        }
    }
}
