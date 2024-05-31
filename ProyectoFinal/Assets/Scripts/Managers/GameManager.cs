using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
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
    RabitController rabbitController = null;
    [SerializeField]
    BushesController bushesController = null;

    public GameObject GetAnimal()
    {
        if (currentSelectedAnimal == null || currentSelectedAnimal.transform.parent.gameObject == null) return null;
        else return currentSelectedAnimal.transform.parent.gameObject;
    }
    private void Start()
    {
        rabbitController.InstanciateRabbits();
        bushesController.InstanciateBushes();
        ground.GetComponent<NavMeshSurface>().BuildNavMesh();
    }

    public void SelectAnimal(GameObject obj)
    {
        if(obj != null)
        {
            if (currentSelectedAnimal != null)
                currentSelectedAnimal.SetActive(false);

            obj.SetActive(true);
            currentSelectedAnimal = obj;
        }
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
            if (individualCameras[int.Parse(splitArray[1])] != null)
            {
                individualCameras[int.Parse(splitArray[1])].SetActive(true);
                mainCamera.SetActive(false);
            }
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
            if(individualCameras[i] != null)
                individualCameras[i].SetActive(false);
        }
    }
}
