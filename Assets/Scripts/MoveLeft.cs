using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float MoveSpeed;

    private void Update()
    {
        transform.position += Vector3.left * MoveSpeed * Time.deltaTime;
    }
}
