using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FourPlayerM : MonoBehaviour {
    private PlayerMove[] pM;
    private int vegans, carnivores;
    public Sprite vSprite, cSprite;
    public static int rounds = 1;
    public static int[] points = {0,0,0,0};
    public Text[] texts;
    public Text announce, announce2;
    public CameraShake shake;
    public float intensity, duration;
    public int maxRounds = 10;
    public GameObject win;

    public static FourPlayerM InstanceFourPlayer;

    Color[] colorsArray;
    private bool killCarnivore = true;
    void Start ()
    {
        win = GameObject.Find("Win");
        shake = Camera.main.GetComponent<CameraShake>();
        announce = GameObject.Find("State Text").GetComponent<Text>();
        announce2 = GameObject.Find("State Text2").GetComponent<Text>();
        win.SetActive(false);
        if (InstanceFourPlayer != null) Destroy(InstanceFourPlayer.gameObject);
        InstanceFourPlayer = this;

        DontDestroyOnLoad(gameObject);

        vegans = 2;
        carnivores = 2;
        PrepareArray();

        colorsArray = pM[0].gameObject.GetComponent<IndicatorController>().colors;
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].color = colorsArray[i];
        }
        UpdatetScores();
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        announce.enabled = true;
        announce.text = "Round " + rounds.ToString();
        yield return new WaitForSeconds(1.0f);
        announce.text = "Go!!!";
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < pM.Length; i++)
        {
            pM[i].move = true;
        }
        announce.enabled = false;
        yield return null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            killCarnivore = !killCarnivore;
            Debug.Log("Kill carnivores? " + killCarnivore);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ManageHit(1, 2, killCarnivore);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ManageHit(1, 3, killCarnivore);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            ManageHit(1, 4, killCarnivore);
        }
    }

    private void PrepareArray()
    {
        pM = new PlayerMove[4];
        PlayerMove[] player = GameObject.FindObjectsOfType<PlayerMove>();
        for (int i = 0; i < player.Length; i++)
        {
            pM[int.Parse(player[i].id) - 1] = player[i];
            pM[int.Parse(player[i].id) - 1].move = false;
        }

        AssignTeams();
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
        shake.Shake(intensity, duration);
        if (vegan)
        {
            //points
            points[p1 - 1] += carnivores * 10;
            //Animation
            //pM[p2 - 1].Transformation();
            GoVegan(p2 - 1);
            ++vegans;
            --carnivores;
        }
        else
        {
            //points
            points[p1 - 1] += vegans * 10;
            //Animation
            //pM[p2 - 1].Transformation();
            GoCarnivore(p2 - 1);
            --vegans;
            ++carnivores;
        }
        UpdatetScores();
        CheckEndRound();
    }

    void CheckEndRound()
    {
        if (carnivores >= 4 || vegans >= 4)
        {
            StartCoroutine(EndRound());
            //END thiiis
            //Animation
            //Invoke scene load
        }
    }

    IEnumerator EndRound()
    {
        announce.enabled = true;
        if (vegans == 0)
        {
            announce.text = "It's steak time!";
        }
        else
        {
            announce.text = "You got veganized!";
        }
        yield return new WaitForSeconds(0.5f);
        //a
        yield return new WaitForSeconds(0.5f);
        Reloadlevel();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("Title");
        yield return null;
    }

    void Reloadlevel()
    {
        ++rounds;
        if (rounds > maxRounds)
        {
            win.SetActive(true);
            int max = 0;
            int index = 0;
            for (int i = 0; i < points.Length; i++)
            {
                if (points[i] > max)
                {
                    max = points[i];
                    index = i;
                }
            }
            announce2.text = "Player " + (index + 1).ToString() + " wins!";
            announce2.color = colorsArray[index];
        }
        else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void UpdatetScores()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].text = points[i].ToString();
        }
    }
}
