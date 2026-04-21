using UnityEngine;

public class SelfDestructCountdown : MonoBehaviour
{
    public float lifeTime = 3f;

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
}