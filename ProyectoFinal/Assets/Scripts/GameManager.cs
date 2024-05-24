using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;


public class GameManager : MonoBehaviour
{

    private GameObject currentSelectedAnimal = null;
    private Button selectedButton = null;
    [SerializeField]
    public GameObject mainCamera = null;
    [SerializeField]
    public GameObject[] individualCameras = null;

    [SerializeField]
    public GameObject[] stags = null;
    [SerializeField]
    public GameObject[] wolfs = null;

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
