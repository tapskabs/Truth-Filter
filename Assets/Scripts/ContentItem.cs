using UnityEngine;

[CreateAssetMenu(fileName = "NewContentItem", menuName = "Post/Content Item")]
public class ContentItem : ScriptableObject
{
    [TextArea] public string content;

    public enum CorrectAnswer { True, False }
    public CorrectAnswer correctAnswer;

    public enum Effect { None, DoublePoints }
    public Effect specialEffect = Effect.None;
    [TextArea] public string investigationHint;
}
