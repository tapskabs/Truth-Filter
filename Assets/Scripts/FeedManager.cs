using UnityEngine;

public class FeedManager : MonoBehaviour
{
    public GameObject postPrefab; // prefab with PostUI
    public Transform feedParent;  // UI container for posts

    private PostUI.Post[] posts = new PostUI.Post[]
    {
        new PostUI.Post { content = "Study proves chocolate boosts IQ!", isTrue = false },
        new PostUI.Post { content = "Mayor resigns after AI deepfake scandal", isTrue = true },
        new PostUI.Post { content = "NASA confirms alien tweet was fake", isTrue = true }
    };

    private int trustScore = 50;

    void Start()
    {
        foreach (var post in posts)
        {
            var obj = Instantiate(postPrefab, feedParent);
            obj.GetComponent<PostUI>().Setup(post, OnDecisionMade);
        }
    }

    void OnDecisionMade(int choice)
    {
        // 0 = False, 1 = True, 2 = Skip
        Debug.Log("Player made choice: " + choice);

        // simple scoring
        if (choice == 0 && !posts[0].isTrue) trustScore += 5;
        else if (choice == 1 && posts[0].isTrue) trustScore += 5;
        else if (choice != 2) trustScore -= 5;

        Debug.Log("Trust Score: " + trustScore);
    }
}
