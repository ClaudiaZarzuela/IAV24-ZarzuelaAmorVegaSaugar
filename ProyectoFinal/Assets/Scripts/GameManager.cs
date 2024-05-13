using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    private GameObject currentSelectedAnimal = null;
    private Button selectedButton = null;
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
}
