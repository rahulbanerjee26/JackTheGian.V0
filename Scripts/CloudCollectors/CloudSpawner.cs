using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject[] clouds;

    private float distance = 3f;

    private float minX, maxX;

    private float lastCloudPosition;

    private float controlX;

    [SerializeField]
    private GameObject[] collectables;

    private GameObject player;

    void minMax()
    {
        Vector3 bound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        maxX = bound.x - .5f;
        minX = -bound.x + .5f;
    }

    void Awake()
    {
        controlX = 0;
        minMax();
        createClouds();
        player = GameObject.Find("Player");
    }

    void randomSpawn(GameObject[] arr)
    {
        for (int i =0; i < arr.Length; i++)
        {
            GameObject temp = arr[i];
            int index = Random.Range(i, arr.Length);
            arr[i] = arr[index];
            arr[index] = temp;
        }
    }

    void createClouds()
    {
        randomSpawn(clouds);

        float positionY = 0f;

        for(int i=0; i < clouds.Length; i++)
        {
            Vector3 temp = clouds[i].transform.position;
            temp.y = positionY;

            if (controlX == 0)
            {
                temp.x = Random.Range(0.0f, maxX);
                controlX = 1;
            } else if (controlX == 1)
            {
                temp.x = Random.Range(0.0f, minX);
                controlX = 2;
            } else if (controlX == 2)
            {
                temp.x = Random.Range(1.0f, maxX);
                controlX = 3;
            } else if (controlX == 3)
            {
                temp.x = Random.Range(-1.0f, minX);
                controlX = 4;
            } else if (controlX == 4)
            {
                temp.x = Random.Range(2.0f, maxX);
                controlX = 5;
            } else if (controlX == 5)
            {
                temp.x = Random.Range(-2.0f, minX);
                controlX = 0;
            }
            lastCloudPosition = positionY;
            clouds[i].transform.position = temp;
            positionY -= distance;
        }
    }


    void PositionThePlayer()
    {

        // getting back clouds
        GameObject[] darkClouds = GameObject.FindGameObjectsWithTag("Deadly");

        // getting clouds in game
        GameObject[] cloudsInGame = GameObject.FindGameObjectsWithTag("Cloud");

        for (int i = 0; i < darkClouds.Length; i++)
        {

            if (darkClouds[i].transform.position.y == 0)
            {

                Vector3 t = darkClouds[i].transform.position;

                darkClouds[i].transform.position = new Vector3(cloudsInGame[0].transform.position.x,
                                                               cloudsInGame[0].transform.position.y,
                                                               cloudsInGame[0].transform.position.z);

                cloudsInGame[0].transform.position = t;

            }

        }

        Vector3 temp = cloudsInGame[0].transform.position;

        for (int i = 1; i < cloudsInGame.Length; i++)
        {

            if (temp.y < cloudsInGame[i].transform.position.y)
                temp = cloudsInGame[i].transform.position;

        }


        // positioning the player above the cloud
        player.transform.position = new Vector3(temp.x, temp.y + 0.8f, temp.z);


    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Deadly" || target.tag == "Cloud")
        {

            if (target.transform.position.y == lastCloudPosition)
            {

                Vector3 temp = target.transform.position;
                randomSpawn(clouds);
                randomSpawn(collectables);

                for (int i = 0; i < clouds.Length; i++)
                {

                    if (!clouds[i].activeInHierarchy)
                    {

                        if (controlX == 0)
                        {

                            temp.x = Random.Range(0, maxX);
                            controlX = 1;

                        }
                        else if (controlX == 1)
                        {

                            temp.x = Random.Range(0, minX);
                            controlX = 2;

                        }
                        else if (controlX == 2)
                        {

                            temp.x = Random.Range(1.0f, maxX);
                            controlX = 3;

                        }
                        else if (controlX == 3)
                        {

                            temp.x = Random.Range(-1.0f, minX);
                            controlX = 0;
                        }

                        temp.y -= distance;

                        lastCloudPosition = temp.y;


                        clouds[i].transform.position = temp;
                        clouds[i].SetActive(true);

                        int random = Random.Range(0, collectables.Length);

                        if (clouds[i].tag != "Deadly")
                        {

                            
                        }
                    }
                }
            }
        }
    }

    void Start()
    {
        PositionThePlayer();
    }

    void Update()
    {
        
    }
}
