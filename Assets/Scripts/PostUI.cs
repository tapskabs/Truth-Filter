using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PostUI : MonoBehaviour
{
    [System.Serializable]
    public class Post
    {
        public string content;
        public bool isTrue;
    }

    public TextMeshProUGUI postText;
    public Button trueButton;
    public Button falseButton;
    public Button skipButton;

    private Post currentPost;
    private System.Action<int> onDecisionMade; // callback to parent

    public void Setup(Post post, System.Action<int> callback)
    {
        currentPost = post;
        postText.text = post.content;
        onDecisionMade = callback;

        trueButton.onClick.AddListener(() => MakeDecision(1));
        falseButton.onClick.AddListener(() => MakeDecision(0));
        skipButton.onClick.AddListener(() => MakeDecision(2));
    }

    private void MakeDecision(int choice)
    {
        onDecisionMade?.Invoke(choice);
        Destroy(gameObject); // remove post after decision
    }
}
