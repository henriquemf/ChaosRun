using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Sprite> purpleVillage;
    public List<Sprite> orangeSwamp;
    public List<Sprite> greenSwamp;
    public List<Sprite> yellowGears;
    public List<Sprite> greenGears;
    public List<Sprite> blueGears;
    public TextMeshProUGUI distanceText;

    private Dictionary<string, List<GameObject>> layers;
    public float distancePurple, distanceOrangeSwamp, distanceGreenSwamp, distanceYellowGears, distanceGreenGears, distanceBlueGears;

    void Start()
    {
        GameObject background = GameObject.Find("Background");

        layers = new Dictionary<string, List<GameObject>>();
        string[] layerNames = { "Close", "Mid", "Far", "SuperFar", "RealFar", "BGGears", "Sky", "Sky2" };

        foreach (string layerName in layerNames)
        {
            List<GameObject> layerObjects = new List<GameObject>();
            layerObjects.Add(background.transform.Find(layerName).gameObject);
            layerObjects.Add(layerObjects[0].transform.Find(layerName + "L").gameObject);
            layerObjects.Add(layerObjects[0].transform.Find(layerName + "R").gameObject);
            
            layers.Add(layerName, layerObjects);
        }
    }

    void Update()
    {
        distanceText.text = "DISTANCE: " + Player.DistanceTravelled.ToString("F0") + "m";
        if (Player.DistanceTravelled > distancePurple && Player.DistanceTravelled < distanceOrangeSwamp)
        {
            ChangeBackground(purpleVillage);
        }
        
        if (Player.DistanceTravelled > distanceOrangeSwamp && Player.DistanceTravelled < distanceGreenSwamp)
        {
            ChangeBackground(orangeSwamp);
        }

        if (Player.DistanceTravelled > distanceGreenSwamp && Player.DistanceTravelled < distanceYellowGears)
        {
            ChangeBackground(greenSwamp);
        }

        if (Player.DistanceTravelled > distanceYellowGears && Player.DistanceTravelled < distanceGreenGears)
        {
            ChangeBackground(yellowGears);
        }

        if (Player.DistanceTravelled > distanceGreenGears && Player.DistanceTravelled < distanceBlueGears)
        {
            ChangeBackground(greenGears);
        }

        if (Player.DistanceTravelled > distanceBlueGears)
        {
            ChangeBackground(blueGears);
        }
    }

    private void ChangeBackground(List<Sprite> sprites)
    {
        int i = 0;
        foreach (var layerObjects in layers.Values)
        {
            foreach (GameObject obj in layerObjects)
            {
                obj.GetComponent<SpriteRenderer>().sprite = sprites[i % sprites.Count];
            }
            i++;
        }
    }
}

