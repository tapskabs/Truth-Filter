using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    public List<ContentItem> contentItems;
    private int currentIndex = 0;

    public ContentItem GetNextContent()
    {
        if (contentItems.Count == 0) return null;
        ContentItem item = contentItems[currentIndex];
        currentIndex = (currentIndex + 1) % contentItems.Count;
        return item;
    }

    public ContentItem PeekCurrent()
    {
        if (contentItems.Count == 0) return null;
        int idx = currentIndex % contentItems.Count;
        return contentItems[idx];
    }

    public void ResetOrder()
    {
        currentIndex = 0;
    }
}
