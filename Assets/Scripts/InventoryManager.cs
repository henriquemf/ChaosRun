using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public List<Sprite> spriteList;
    private List<Sprite> selectedSprites;

    private int buttonIndex;

    public Image image1;
    public Image image2;
    public Image image3;

    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;

    public Shoot shoot;

    private void Start()
    {
        RandomizeItems();
        AssignImagesAndTexts();
    }

    public void GetButton1()
    {
        buttonIndex = 0;
        SelectItem();
    }

    public void GetButton2()
    {
        buttonIndex = 1;
        SelectItem();
    }

    public void GetButton3()
    {
        buttonIndex = 2;
        SelectItem();
    }

    private void SelectItem()
    {
        if (buttonIndex >= 0 && buttonIndex < selectedSprites.Count)
        {
            Sprite selectedItem = selectedSprites[buttonIndex];
            if (selectedSprites[buttonIndex] == spriteList[0])
            {
                StatsManager.playerASpeed -= 0.02f;
                shoot.shootTimeout -= 0.02f;
            }
            else if (selectedSprites[buttonIndex] == spriteList[1])
            {
                StatsManager.playerMSpeed += 1f;
                Player.moveSpeed += 1f;
            }
            else if (selectedSprites[buttonIndex] == spriteList[2])
            {
                StatsManager.RandomizePrefabs();
            }
            else if (selectedSprites[buttonIndex] == spriteList[3])
            {
                StatsManager.playerShootLevel += 1;
            }
        }
    }

    public void RandomizeItems()
    {
        selectedSprites = new List<Sprite>();

        if (spriteList.Count >= 3)
        {
            // Definir as probabilidades para cada item
            float[] probabilities = { 0.3f, 0.3f, 0.25f, 0.15f };

            for (int i = 0; i < 3; i++)
            {
                // Gerar um valor aleatório entre 0 e 1
                float randomValue = Random.value;
                float cumulativeProbability = 0f;

                for (int j = 0; j < spriteList.Count; j++)
                {
                    cumulativeProbability += probabilities[j];

                    // Se o valor aleatório estiver dentro da faixa de probabilidade atual
                    if (randomValue <= cumulativeProbability)
                    {
                        Sprite selectedSprite = spriteList[j];
                        selectedSprites.Add(selectedSprite);
                        break;
                    }
                }
            }
        }
        else
        {
            // Debug.Log("Not enough sprites in the list");
        }
        AssignImagesAndTexts();
    }

    public void AssignImagesAndTexts()
    {
        if (selectedSprites.Count >= 3)
        {
            image1.sprite = selectedSprites[0];
            image2.sprite = selectedSprites[1];
            image3.sprite = selectedSprites[2];

            if (selectedSprites[0] == spriteList[0])
            {
                text1.text = "SHOOT FASTER!";
            }
            else if (selectedSprites[0] == spriteList[1])
            {
                text1.text = "RUN FASTER!";
            }
            else if (selectedSprites[0] == spriteList[2])
            {
                text1.text = "NEW FIRE MODE";
            }
            else if (selectedSprites[0] == spriteList[3])
            {
                text1.text = "SHOOT LEVEL UP";
            }

            if (selectedSprites[1] == spriteList[0])
            {
                text2.text = "SHOOT FASTER!";
            }
            else if (selectedSprites[1] == spriteList[1])
            {
                text2.text = "RUN FASTER!";
            }
            else if (selectedSprites[1] == spriteList[2])
            {
                text2.text = "NEW FIRE MODE";
            }
            else if (selectedSprites[1] == spriteList[3])
            {
                text2.text = "SHOOT LEVEL UP";
            }

            if (selectedSprites[2] == spriteList[0])
            {
                text3.text = "SHOOT FASTER!";
            }
            else if (selectedSprites[2] == spriteList[1])
            {
                text3.text = "RUN FASTER!";
            }
            else if (selectedSprites[2] == spriteList[2])
            {
                text3.text = "NEW FIRE MODE";
            }
            else if (selectedSprites[2] == spriteList[3])
            {
                text3.text = "SHOOT LEVEL UP";
            }
        }
    }

    public void TemporaryInventory()
    {

    }
}
