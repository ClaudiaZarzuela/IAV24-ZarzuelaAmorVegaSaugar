using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;


public class GameManager : MonoBehaviour
{
    #region references
    static private GameManager _instance;
    static public GameManager Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion

    #region methods
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    private GameObject currentSelectedAnimal = null;
    private Button selectedButton = null;
    [SerializeField]
    public GameObject mainCamera = null;
    [SerializeField]
    public GameObject[] individualCameras = null;
    [SerializeField]
    public GameObject ground = null;

    [SerializeField] 
    public GameObject rabbit = null;

    [SerializeField]
    AnimalController rabbitController = null;
    [SerializeField]
    BushesController bushesController = null;

    private void Start()
    {
        InstanciateRabbits();
        bushesController.InstanciateBushes();
        ground.GetComponent<NavMeshSurface>().BuildNavMesh();
    }
    public void InstanciateRabbits()
    {
        Vector3 randomPoint;
        NavMeshHit hit;
        Transform parent = new GameObject("Rabbits").transform;

        for (int i = rabbitController.currRabitsNum; i < AnimalController.rabitsNum; i++)
        {

            randomPoint = transform.position + Random.insideUnitSphere * 20;
            if (NavMesh.SamplePosition(randomPoint, out hit, 20, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
            }
            else
            {
                Debug.Log("Rabbit: " + i);
                randomPoint = transform.position + Random.insideUnitSphere * Random.Range(-5.0f, 5.0f);
            }
            randomPoint.y = 0.0f;
            GameObject newRabbit = Instantiate(rabbit, randomPoint, Quaternion.identity);
            newRabbit.transform.SetParent(parent);
            rabbitController.rabbits.Add(newRabbit);
            rabbitController.currRabitsNum++;

        }
    }
    public void SelectAnimal(GameObject obj)
    {
        if (currentSelectedAnimal != null)
            currentSelectedAnimal.SetActive(false);

        obj.SetActive(true);
        currentSelectedAnimal = obj;
    }

    public void SelectButton(Button button)
    {
        if (selectedButton != null)
            selectedButton.GetComponent<Image>().color = Color.white;

        button.GetComponent<Image>().color = Color.yellow;
        selectedButton = button;

    }

    public void ChangeToIndividualCamera()
    {
        if(currentSelectedAnimal != null)
        {
            string name = currentSelectedAnimal.transform.parent.name;
            string[] splitArray = name.Split('_'); 
            individualCameras[int.Parse(splitArray[1])].SetActive(true);
            mainCamera.SetActive(false);
        }
    }

    public void ChangeToMainCamera()
    {
        mainCamera.SetActive(true);
        DisableIndividualCameras();
    }

    private void DisableIndividualCameras()
    {
        for(int i = 0; i < individualCameras.Length; i++)
        {
            individualCameras[i].SetActive(false);
        }
    }

}
