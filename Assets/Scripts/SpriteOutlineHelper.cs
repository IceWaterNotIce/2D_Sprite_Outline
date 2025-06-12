using UnityEngine;

public static class SpriteOutlineHelper
{
    // 靜態方法：為指定 GameObject 添加外框
    public static void AddOutline(GameObject target, Color outlineColor, float outlineSize)
    {
        if (target == null)
        {
            Debug.LogWarning("Target GameObject is null!");
            return;
        }

        SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("Target has no SpriteRenderer component!");
            return;
        }

        GameObject outline = new GameObject("Outline");
        outline.transform.parent = target.transform;
        outline.transform.localScale = Vector3.one * (1 + outlineSize);
        outline.transform.localPosition = Vector3.zero;

        SpriteRenderer outlineRenderer = outline.AddComponent<SpriteRenderer>();
        outlineRenderer.sprite = spriteRenderer.sprite;
        outlineRenderer.color = outlineColor;
        outlineRenderer.sortingOrder = spriteRenderer.sortingOrder - 1;
    }

    // 靜態方法：移除所有外框
    public static void RemoveAllOutlines(GameObject target)
    {
        if (target == null) return;

        foreach (Transform child in target.transform)
        {
            if (child.name == "Outline")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    // 靜態方法：為 Sprite 添加滑鼠懸停高亮效果
    public static void AddMouseOverHighlight(GameObject target, Color highlightColor, float outlineSize = 0.1f)
    {
        if (target == null) return;

        // 確保目標有 Collider2D（用於滑鼠檢測）
        if (target.GetComponent<Collider2D>() == null)
        {
            Debug.LogWarning("Target needs a Collider2D for mouse interaction!");
            target.AddComponent<BoxCollider2D>(); // 自動添加一個簡單的 Collider
        }

        // 添加 MonoBehaviour 組件來處理滑鼠事件
        HighlightBehaviour behaviour = target.GetComponent<HighlightBehaviour>();
        if (behaviour == null)
        {
            behaviour = target.AddComponent<HighlightBehaviour>();
        }

        behaviour.SetHighlightSettings(highlightColor, outlineSize);
    }
}

// 內部類別：處理滑鼠事件的 MonoBehaviour
internal class HighlightBehaviour : MonoBehaviour
{
    private Color highlightColor;
    private float outlineSize;
    private GameObject currentOutline;

    public void SetHighlightSettings(Color color, float size)
    {
        highlightColor = color;
        outlineSize = size;
    }

    private void OnMouseEnter()
    {
        // 滑鼠進入時添加外框
        currentOutline = new GameObject("TempOutline");
        currentOutline.transform.parent = transform;
        currentOutline.transform.localScale = Vector3.one * (1 + outlineSize);
        currentOutline.transform.localPosition = Vector3.zero;

        SpriteRenderer outlineRenderer = currentOutline.AddComponent<SpriteRenderer>();
        outlineRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        outlineRenderer.color = highlightColor;
        outlineRenderer.sortingOrder = GetComponent<SpriteRenderer>().sortingOrder - 1;
    }

    private void OnMouseExit()
    {
        // 滑鼠離開時移除外框
        if (currentOutline != null)
        {
            Destroy(currentOutline);
        }
    }
}