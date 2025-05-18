using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private void Awake() {
        entryTemplate.gameObject.SetActive(false);
    }
    private IEnumerator Start(){
        yield return new WaitUntil(() => MongoDBExport.instance != null && MongoDBExport.instance.playerNames.Count > 0);

        var sortedList = MongoDBExport.instance.playerNames
            .Select((name, index) => new { Name = name,
            Score = MongoDBExport.instance.scores[index] })
            .OrderByDescending(entry => entry.Score)
            .ToList();

        float templateHeight = 20f;
        int displayCount = Mathf.Min(11, sortedList.Count);

        for (int i = 0; i < displayCount; i++) {
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
            scoreText.text = sortedList[i].Score.ToString(); // ? ¡ahora ordenado!
            nameText.text = sortedList[i].Name;


        }
    }
    
}