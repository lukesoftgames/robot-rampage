using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    private float numBuildings = 5;
    private float baseDist = 25;
    public GameObject buildingPrefab;
    private Vector3 initLoc;
    private float levelEndDist = 0;
    private AudioSource audioData;
    public GameObject robotPrefab;
    public GameObject backgroundMusic;
    private bool playing;
    private GameObject[] enemies;
    public Text winText;
    public GameObject player;
    public GameObject flag;
    public GameObject menu;


    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        levelEndDist = numBuildings * baseDist+ 10;
        flag.transform.position += new Vector3(levelEndDist, 0, 0);
        audioData = GetComponent<AudioSource>();
        initLoc.y = -3.4f;
        for (int i = 0; i < numBuildings; i++)
        {
            float dist = baseDist + Random.Range(-5, 5);
            Vector3 pos = transform.position;
            pos = initLoc + new Vector3(i * dist, 0, 0);
            Instantiate(buildingPrefab, pos, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("temp_core") == null)
        {
            Debug.Log("LOOOOOSSSTT");
            if (!playing)
            {
                menu.SetActive(true);
                //backgroundMusic.GetComponent<AudioSource>().Stop();
                playing = true;
                winText.text = "YOU LOST!";
                winText.enabled = true;
                StartCoroutine(FadeTextToFullAlpha(1f, winText));
            }
            IEnumerator fadeSound1 = AudioFadeOut.FadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.5f);
            StartCoroutine(fadeSound1);
            StopCoroutine(fadeSound1);
            player.GetComponent<PlayerMovement>().won = true;
            Debug.Log("WIN");
            robotPrefab.GetComponent<RobotMovement>().won = true;
        }
        if (robotPrefab.transform.position.x >= levelEndDist)
        {

            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (enemies.Length>0){
                foreach (GameObject enemy in enemies)
                {
                    enemy.GetComponent<TankController>().damage(100);
                }
            }

            if (!playing)
            {
                menu.SetActive(true);
                //backgroundMusic.GetComponent<AudioSource>().Stop();
                audioData.Play();
                playing = true;
                winText.enabled = true;
                StartCoroutine(FadeTextToFullAlpha(1f, winText));
            }
            IEnumerator fadeSound1 = AudioFadeOut.FadeOut(backgroundMusic.GetComponent<AudioSource>(), 0.5f);
            StartCoroutine(fadeSound1);
            StopCoroutine(fadeSound1);
            player.GetComponent<PlayerMovement>().won = true;
            Debug.Log("WIN");
            robotPrefab.GetComponent<RobotMovement>().won = true;
        }
    }



    public static class AudioFadeOut
    {

        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

    }


    public IEnumerator FadeTextToFullAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
