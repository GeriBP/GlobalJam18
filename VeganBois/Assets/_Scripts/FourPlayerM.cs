using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourPlayerM : MonoBehaviour {
    private PlayerMove[] pM;
    private int vegans, carnivores;

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

        //shuffle
        for (int i = 0; i < pM.Length; i++)
        {
            int rnd = Random.Range(0, pM.Length);
            PlayerMove temp = pM[rnd];
            pM[rnd] = pM[i];
            pM[i] = temp;
        }
        for (int i = 0; i < pM.Length; i++)
        {
            if (i < 2)
            {
                pM[i].isVegan = true;
            }
            else
            {
                pM[i].isVegan = false;
            }
            Debug.Log(i + " : " + pM[i].id + " is he vegan? " + pM[i].isVegan);
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
