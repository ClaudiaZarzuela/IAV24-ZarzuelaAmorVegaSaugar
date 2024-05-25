using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    [SerializeField]
    public TMPro.TextMeshProUGUI rabitsNum;
    [SerializeField]
    public TMPro.TextMeshProUGUI bushesNum;
    int rabitsNumber = 10;
    int bushesNumber = 5;

    void Start()
    {
        SetRabits();
        SetBushes();
    }

    public void AddRabits()
    {
        rabitsNumber++;
        SetRabits();
    }

    public void RemoveRabits()
    {
        rabitsNumber--;
        SetRabits();
    }

    public void SetRabits()
    {
        rabitsNum.text = rabitsNumber.ToString();
        RabitController.rabitsNum = rabitsNumber;
    }

    public void AddBushes()
    {
        bushesNumber++;
        SetBushes();
    }

    public void RemoveBushes()
    {
        bushesNumber--;
        SetBushes();
    }

    public void SetBushes()
    {
        bushesNum.text = bushesNumber.ToString();
        BushesController.bushesNum = bushesNumber;
    }
}
