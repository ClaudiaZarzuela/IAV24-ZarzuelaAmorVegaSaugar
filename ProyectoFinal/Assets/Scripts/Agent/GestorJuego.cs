using UnityEngine.SceneManagement;
using UnityEngine;
/*    
   Copyright (C) 2020-2023 Federico Peinado
   http://www.federicopeinado.com

   Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
   Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

   Autor: Federico Peinado 
   Contacto: email@federicopeinado.com
*/
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Authentication.ExtendedProtection;
using TreeEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Debug = UnityEngine.Debug;

namespace UCM.IAV.Movimiento
{

    public class GestorJuego : MonoBehaviour
    {
        public static GestorJuego instance = null;

        [SerializeField]
        GameObject scenario = null;
        [SerializeField]
        GameObject arbol = null;
        [SerializeField]
        GameObject casa = null;

        [SerializeField]
        GameObject rataPrefab = null;

        // textos UI
        [SerializeField]
        Text fRText;
        [SerializeField]
        Text ratText;

        private GameObject rataGO = null;
        private int frameRate = 60;

        // Variables de timer de framerate
        int m_frameCounter = 0;
        float m_timeCounter = 0.0f;
        float m_lastFramerate = 0.0f;
        float m_refreshTime = 0.5f;

        private int numRats;

        private bool cameraPerspective = true;

        //Lista de ratas
        public List<GameObject> rats = new List<GameObject>();

        //Indicador de click
        private bool hasClicked = false;

        [SerializeField]
        GameObject cheesePrefab = null;

        public LayerMask floor;

        [SerializeField] private GameObject Camera1 = null;
        [SerializeField] private GameObject Camera2 = null;

        private void Awake()
        {
            //Cosa que viene en los apuntes para que el gestor del juego no se destruya entre escenas
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // Lo primero que se llama al activarse (tras el Awake)
        void OnEnable()
        {

            // No necesito este delegado
            //SceneManager.activeSceneChanged += OnSceneWasSwitched;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // Delegado para hacer cosas cuando una escena termina de cargar (no necesariamente cuando ha cambiado/switched)
        // Antiguamente se usaba un método del SceneManager llamado OnLevelWasLoaded(int level), ahora obsoleto
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            rataGO = GameObject.Find("Ratas");
            ratText = GameObject.Find("NumRats").GetComponent<Text>();
            fRText = GameObject.Find("Framerate").GetComponent<Text>();
            numRats = rataGO.transform.childCount;
            ratText.text = numRats.ToString();
            //PerlinNoise();
        }

        // Se llama para poner en marcha el gestor
        private void Start()
        {
            rats = new List<GameObject>();          
            rataGO = GameObject.Find("Ratas");
            Application.targetFrameRate = frameRate;
            numRats = rataGO.transform.childCount;
            ratText.text = numRats.ToString();
            rats.Add(rataGO.transform.GetChild(0).gameObject);
            rataGO.transform.GetChild(0).gameObject.GetComponent<Separacion>().game = this;
            rataGO.transform.GetChild(0).gameObject.GetComponent<Cohesion>().game = this;
        }

        // Se llama cuando el juego ha terminado
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        // Update is called once per frame
        void Update()
        {
            // Timer para mostrar el frameRate a intervalos
            if (m_timeCounter < m_refreshTime)
            {
                m_timeCounter += Time.deltaTime;
                m_frameCounter++;
            }
            else
            {
                m_lastFramerate = (float)m_frameCounter / m_timeCounter;
                m_frameCounter = 0;
                m_timeCounter = 0.0f;
            }

            // Texto con el framerate y 2 decimales
            fRText.text = (((int)(m_lastFramerate * 100 + .5) / 100.0)).ToString();

            //Input
            if (Input.GetKeyDown(KeyCode.R))
            {
                Restart();
                PerlinNoise();
            }
            if (Input.GetKeyDown(KeyCode.T))
                HideScenario();
            if (Input.GetKeyDown(KeyCode.O))
                SpawnRata();
            if (Input.GetKeyDown(KeyCode.P))
                DespawnRata();
            if (Input.GetKeyDown(KeyCode.F))
                ChangeFrameRate();
            if (Input.GetKeyDown(KeyCode.N))
                ChangeCameraView();
            if (Input.GetKeyDown(KeyCode.M))
                CreatePerlinNoiseTrees();


            if(!hasClicked && Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Hamelin_Single") SpawnCheese();
            if(hasClicked && Input.GetMouseButtonUp(0) && SceneManager.GetActiveScene().name == "Hamelin_Single")hasClicked =false;
        }

        private void SpawnCheese()
        {
            Vector3 mousePos= Input.mousePosition;
            
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if(Physics.Raycast(ray, out RaycastHit hitData ,1000,floor))
            {
                if(Physics.Raycast(ray, out RaycastHit auxData))
                {
                    // Debug.Log(hitData.transform.gameObject);
                    if(auxData.transform.gameObject.layer != 8)
                    {                   
                        Vector3 worldPos = hitData.point;
                        hasClicked = true;
                        Instantiate(cheesePrefab, new Vector3(worldPos.x, cheesePrefab.transform.position.y,worldPos.z), cheesePrefab.transform.rotation);
                    }
                }
            }
        }

        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void HideScenario()
        {
            if (scenario == null)
                return;

            if (scenario.activeSelf)
                scenario.SetActive(false);
            else
                scenario.SetActive(true);
        }

        private void SpawnRata()
        {
            if (rataPrefab == null || rataGO == null)
                return;

            GameObject rataInst = Instantiate(rataPrefab, rataGO.transform);
            rataInst.GetComponent<Separacion>().game = this;
            rataInst.GetComponent<Cohesion>().game = this;
            rats.Add(rataInst);

            numRats++;
            ratText.text = numRats.ToString();

        }

        private void DespawnRata()
        {
            if (rataGO == null || rataGO.transform.childCount < 1)
                return;
            rats.Remove((rataGO.transform.GetChild(0).gameObject));
            Destroy(rataGO.transform.GetChild(0).gameObject);

            numRats--;
            ratText.text = numRats.ToString();
        }

        private void ChangeFrameRate()
        {
            if (frameRate == 30)
            {
                frameRate = 60;
                Application.targetFrameRate = 60;
            }
            else
            {
                frameRate = 30;
                Application.targetFrameRate = 30;
            }
        }

        private void ChangeCameraView()
        {
            if (cameraPerspective)
            {
                Camera.main.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 15, -2);
                if(SceneManager.GetActiveScene().name == "Hamelin_Multi")
                {
                    Camera1.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 15, -2);
                    Camera2.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 15, -2);
                }
                cameraPerspective = false;
            }
            else
            {
                Camera.main.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 7, -10);
                if (SceneManager.GetActiveScene().name == "Hamelin_Multi")
                {
                    Camera1.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 7, -10);
                    Camera2.GetComponent<SeguimientoCamara>().offset = new Vector3(0, 7, -10);
                }
                cameraPerspective = true;
            }
        }

        [SerializeField]
        [Range(0.01f, 0.9f)]
        private float Zoom = 0.1f;

        [SerializeField]
        [Range(0, 1)]
        private float probGenerar = 0.4f;

        [SerializeField]
        private int numObst;

        private void PerlinNoise()
        {
            for (int i = 0; i < scenario.transform.childCount; i++)
                Destroy(scenario.transform.GetChild(i).gameObject);

            float ancho = 51;
            float alto = 50;
            int actObs = 0;

            while (actObs < numObst)
            {
                float semilla = Random.Range(0, 100000);
                for (float i = -ancho * Zoom / 2; i < ancho * Zoom / 2 && actObs < numObst; i += Zoom)
                {
                    for (float j = -alto * Zoom / 2; j < alto * Zoom / 2 && actObs < numObst; j += Zoom)
                    {
                        if (Mathf.PerlinNoise(i + semilla, j + semilla) < probGenerar)
                        {
                            GameObject obstaculo = null;
                            BoxCollider box = null;
                            if (Random.Range(0, 5) == 3)
                            {
                                obstaculo = casa;
                                box = casa.GetComponent<BoxCollider>();
                            }
                            else
                            {
                                obstaculo = arbol;
                                box = arbol.GetComponent<BoxCollider>();
                            }
                            if (obstaculo != null && box != null)
                            {
                                Vector3 vec = new Vector3(i, 0, j);
                                Vector3 size = new Vector3(box.size.x / 2, 0, box.size.z / 2);
                                if (!(Physics.OverlapBox(vec / Zoom, size).Length > 0))
                                {
                                    GameObject newobst = Instantiate(obstaculo, vec / Zoom, Quaternion.identity);
                                    newobst.transform.parent = scenario.transform;
                                    actObs++;
                                }
                            }
                            else
                                return;
                        }
                    }
                }
            }
        }

        private void  CreatePerlinNoiseTrees()
        {
            float ancho = 51;
            float alto = 50;
            bool generated = false;
            BoxCollider box = arbol.GetComponent<BoxCollider>();
            Vector3 size = new Vector3(box.size.x / 2, 0, box.size.z / 2);
            do
            {
                float semilla = Random.Range(0, 100000);
                for (float i = -ancho * Zoom / 2; i < ancho * Zoom / 2 && !generated; i += Zoom)
                {
                    for (float j = -alto * Zoom / 2; j < alto * Zoom / 2 && !generated; j += Zoom)
                    {
                        if (Mathf.PerlinNoise(i + semilla, j + semilla) < probGenerar)
                        {
                            Vector3 vec = new Vector3(i, 0, j);
                            if (!(Physics.OverlapBox(vec / Zoom, size).Length > 0))
                            {
                                GameObject newobst = Instantiate(arbol, vec / Zoom, Quaternion.identity);
                                newobst.transform.parent = scenario.transform;
                                generated = true;
                            }
                        }
                    }
                }
            } while (!generated);
        }
    }
}