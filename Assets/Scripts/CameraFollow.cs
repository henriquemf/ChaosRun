using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Player player;  // Referência ao script do jogador
    public float minSpeed = 1.0f;  // Velocidade mínima da câmera
    private Vector3 offset;
    private float previousDistance;

    void Start()
    {
        // Offset entre o jogador e a câmera no início
        offset = transform.position - player.transform.position;
        previousDistance = Player.DistanceTravelled;
    }

    void Update()
    {
        // Calcula a nova posição da câmera
        float targetX = player.transform.position.x + offset.x;
        float speed = Mathf.Max(minSpeed, Mathf.Abs(player.CurrentSpeed));

        // Verifica se o jogador está parado ou diminuiu a velocidade
        if (Player.DistanceTravelled <= previousDistance || player.CurrentSpeed < minSpeed)
        {
            transform.Translate(Vector3.right * minSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 newPos = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        }

        // Atualiza a distância anterior do jogador
        previousDistance = Player.DistanceTravelled;
    }
}
