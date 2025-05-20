using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPlates : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> plates;

    [SerializeField]
    [Range(0.5f, 2f)]
    private float reactivateRate = 0.6f;

    private int maxPlates;

    public int currentPlates { get; private set; }

    private Coroutine spawnCoroutine = null;

    private void Start()
    {
        maxPlates = plates.Count;
        currentPlates = maxPlates;
    }

    public void ReactivatePlate()
    {
        if (currentPlates != maxPlates)
        {
            plates[currentPlates++].SetActive(true);
        }
    }

    public void HidePlate()
    {
        var count = plates.Count;

        if (currentPlates != 0)
        {
            plates[--currentPlates].SetActive(false);

            if(spawnCoroutine==null)
            {
                spawnCoroutine = StartCoroutine(SpawnCoroutine());
            }
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        while(currentPlates < maxPlates)
        {
            yield return new WaitForSeconds(reactivateRate);

            ReactivatePlate();
        }

        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }
}
