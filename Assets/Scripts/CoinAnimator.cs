using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CoinAnimator : MonoBehaviour
{
    public Image targetImage; // Componente Image a ser animado
    public Sprite[] sprites; // Lista de sprites para a animação
    public float delayBetweenImages = 0.5f; // Tempo de espera entre cada imagem em segundos

    private int currentIndex = 0; // Índice do sprite atual na lista

    private void Start()
    {
        // Inicia a animação
        StartCoroutine(AnimateImages());
    }

    private IEnumerator AnimateImages()
    {
        while (true)
        {
            // Atualiza o sprite do componente Image
            targetImage.sprite = sprites[currentIndex];

            // Aguarda o tempo de espera entre as imagens
            yield return new WaitForSeconds(delayBetweenImages);

            // Avança para o próximo sprite
            currentIndex = (currentIndex + 1) % sprites.Length;
        }
    }
}
