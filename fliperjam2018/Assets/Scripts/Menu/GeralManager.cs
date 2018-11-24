using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GeralManager : MonoBehaviour {

    public GameObject pauseGO; //Objeto que ira aperecer como pause

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
        if(SceneManager.GetActiveScene().name == "Menu")
            if(Input.anyKeyDown && !Input.GetButtonDown("Cancel"))
            {
                SceneManager.LoadScene("Fase");
            }
            else
            {
                Application.Quit();
            }

        if(SceneManager.GetActiveScene().name == "Fase")
        {
            if(Input.GetButtonDown("Cancel"))
            {
                if (pauseGO.activeInHierarchy == true)
                {
                    SceneManager.LoadScene("Menu");
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = 0.02f;
                }
                else
                {
                    pauseGO.SetActive(true);
                    Time.timeScale = 0;
                    Time.fixedDeltaTime = 0.001f; //padrão é 0.02
                }
            }

            if (Input.anyKey && !Input.GetButton("Cancel"))
            {
                if(pauseGO.activeInHierarchy == true)
                {
                    Time.timeScale = 1;
                    Time.fixedDeltaTime = 0.02f;
                    pauseGO.SetActive(false);
                }
            }
        }
	}

    private void OnLevelWasLoaded(int level)
    {
        pauseGO.SetActive(false);
    }

}
