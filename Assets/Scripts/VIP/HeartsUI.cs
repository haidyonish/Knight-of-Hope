using System.Collections.Generic;
using UnityEngine;

public class HeartsUI : MonoBehaviour
{
    [SerializeField] private EntityHealth targetHealth;
    [SerializeField] private Transform heartsRoot;
    [SerializeField] private GameObject heartPrefab;
    [SerializeField] private float spacing = 0.35f;

    private readonly List<GameObject> hearts = new List<GameObject>();

    private void Start()
    {
        RefreshHearts();
    }

    public void RefreshHearts()
    {
        ClearHearts();

        int currentHealth = Mathf.RoundToInt(targetHealth.CurrentHealth);

        float startX = -(currentHealth - 1) * spacing / 2f;

        for (int i = 0; i < currentHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsRoot);

            heart.transform.localPosition =
                new Vector3(startX + i * spacing, 0f, 0f);

            hearts.Add(heart);
        }
    }

    private void ClearHearts()
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            Destroy(hearts[i]);
        }

        hearts.Clear();
    }
}