using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Vector3 originalPos;

    void Update()
    {
        originalPos = transform.localPosition;
    }
    
    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0.0f;
        
        while (elapsed < duration)
        {
            float y = Random.Range(originalPos.y - 1f, originalPos.y + 1f) * magnitude;
            
            transform.localPosition = new Vector3(originalPos.x, y, originalPos.z);
            
            elapsed += Time.deltaTime;
            
            yield return null;
        }
        
        transform.localPosition = originalPos;
    }
}
