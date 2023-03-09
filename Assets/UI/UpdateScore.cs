using UnityEngine;
using UnityEngine.UIElements;

public class UpdateScore : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        Label score = root.Q<Label>("score");

        score.text = "SCORE: " + player.GetComponent<PlayerAttributes>().EnemiesHit;
    }
}
