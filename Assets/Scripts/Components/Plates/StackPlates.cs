using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPlates : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Plates GOs")]
    private List<GameObject> plates;

    [SerializeField]
    [Tooltip("How much time to reactivate hidden plate")]
    [Range(0.5f, 2f)]
    private float reactivateRate = 0.6f;

    /// <summary>
    /// Maximum plates
    /// </summary>
    private int maxPlates;

    /// <summary>
    /// Currently activated plates
    /// </summary>
    public int currentPlates { get; private set; }

    /// <summary>
    /// Reactivate coroutine
    /// </summary>
    private Coroutine reactivateCoroutine = null;

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

            if(reactivateCoroutine==null)
            {
                reactivateCoroutine = StartCoroutine(ReactivateCoroutine());
            }
        }
    }

    private IEnumerator ReactivateCoroutine()
    {
        while(currentPlates < maxPlates)
        {
            yield return new WaitForSeconds(reactivateRate);

            ReactivatePlate();
        }

        StopCoroutine(reactivateCoroutine);
        reactivateCoroutine = null;
    }
}
