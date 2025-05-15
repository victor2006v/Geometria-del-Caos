using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZstdSharp.Unsafe;

public class HighScore_Table : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    
    [SerializeField] private TextMeshPro postText;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro nameText;
    
    private int rank;
    
    private void Awake()
    {

        entryTemplate.gameObject.SetActive(false);

        float templateHeight = 20f;

        for (int i = 0; i < 10; i++){
            GameObject entry = Instantiate(entryTemplate.gameObject, entryContainer);
            RectTransform entryRectTransform = entry.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * i);
            entry.SetActive(true);
            rank = i + 1;

            //Convert this to text
            
            string rankString;
            switch (rank) {
                case 1:
                    rankString = "1ST";
                    break;
                case 2:
                    rankString = "2ST";
                    break;
                case 3:
                    rankString = "3RD";
                    break;
                default:
                    rankString = rank + "TH";
                    break;

            }
            postText.text = rankString;
            int score = Random.Range(0, 10000);
            scoreText.text = score.ToString();
            string name = "AAA"; 
            nameText.text = name;
        }
    }
}
