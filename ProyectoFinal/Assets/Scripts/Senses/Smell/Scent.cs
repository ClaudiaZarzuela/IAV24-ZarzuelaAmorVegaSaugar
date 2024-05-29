using System;
using UnityEngine;

public class Scent : MonoBehaviour, IComparable<Scent>
{
    public float timeToLive;
    public float decreaseFactor = 0.000001f;
    public Renderer _renderer;
    private float elapsedTime;
    private bool alive = true;
    private GameObject originator;

    [SerializeField]
    public string typeScent;

    void Start()
    {
        elapsedTime = 0;
        _renderer = gameObject.GetComponent<Renderer>();
    }

    public void SetOriginator(GameObject or)
    {
        originator = or;
    }
    public GameObject GetOriginator()
    {
        return originator;
    }
    public float GetIntensity()
    {
        return gameObject.transform.localScale.magnitude;
    }

    public string GetTypeScent()
    {
        return typeScent;
    }

    void Update()
    {
        if (alive)
        {
            if (gameObject.transform.localScale.x <= 0 || elapsedTime >= timeToLive)
            {
                Destroy(gameObject);
                alive = false;
            }
            else
            {
                elapsedTime += Time.deltaTime;
                gameObject.transform.localScale -= new Vector3(decreaseFactor / timeToLive, 0, decreaseFactor / timeToLive) * Time.deltaTime;
                Color color = _renderer.material.color;
                color.g += 0.1f * Time.deltaTime;
                _renderer.material.color = color;
            }
        }
    }

    public int CompareTo(Scent other)
    {
        if (other == null) return 1;

        bool isStagThis = typeScent == "Stag";
        bool isStagOther = other.GetTypeScent() == "Stag";

        if (isStagThis && !isStagOther) return -1;
        if (!isStagThis && isStagOther) return 1;

        return other.GetIntensity().CompareTo(this.GetIntensity());
    }
}
