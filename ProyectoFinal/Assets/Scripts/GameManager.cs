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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < stags.Length; ++i) stags[i].gameObject.GetComponent<Animator>().Play("Idle");
            for (int i = 0; i < wolfs.Length; ++i) wolfs[i].gameObject.GetComponent<Animator>().Play("Idle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < stags.Length; ++i) stags[i].gameObject.GetComponent<Animator>().Play("Walk");
            for (int i = 0; i < wolfs.Length; ++i) wolfs[i].gameObject.GetComponent<Animator>().Play("Walk");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            for (int i = 0; i < stags.Length; ++i) stags[i].gameObject.GetComponent<Animator>().Play("Run");
            for (int i = 0; i < wolfs.Length; ++i) wolfs[i].gameObject.GetComponent<Animator>().Play("Run");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            for (int i = 0; i < stags.Length; ++i) stags[i].gameObject.GetComponent<Animator>().Play("Eat");
            for (int i = 0; i < wolfs.Length; ++i) wolfs[i].gameObject.GetComponent<Animator>().Play("Eat");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            for (int i = 0; i < stags.Length; ++i) stags[i].gameObject.GetComponent<Animator>().Play("Death");
            for (int i = 0; i < wolfs.Length; ++i) wolfs[i].gameObject.GetComponent<Animator>().Play("Death");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            for (int i = 0; i < wolfs.Length; ++i) wolfs[i].gameObject.GetComponent<Animator>().Play("Attack");
        }
    }
}
