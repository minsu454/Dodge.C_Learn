using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float FireRate;
    public float projectileSpeed;
    public float time;
    protected virtual void Update()
    {
        time += Time.deltaTime;

    }
}
