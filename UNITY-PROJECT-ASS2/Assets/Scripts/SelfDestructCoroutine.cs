using System.Collections;
using UnityEngine;

public class SelfDestructCoroutine : MonoBehaviour
{
    public float destroyDelay = 3f;

    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}