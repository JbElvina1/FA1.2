using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransformHUD : MonoBehaviour
{
    public PlayerTransformController player;
    public PlayerHealth health;

    [Header("Look")]
    public float fontSize = 18f;
    public Vector2 margin = new Vector2(20f, -20f);
    public Vector2 padding = new Vector2(14f, 10f);
    public Color backgroundColor = new Color(0f, 0f, 0f, 0.45f);

    TextMeshProUGUI text;

    void Awake()
    {
        var bg = new GameObject("HUDBackground", typeof(RectTransform), typeof(Image),
            typeof(VerticalLayoutGroup), typeof(ContentSizeFitter));
        var bgRT = bg.GetComponent<RectTransform>();
        bgRT.SetParent(transform, false);
        bgRT.anchorMin = bgRT.anchorMax = bgRT.pivot = new Vector2(0f, 1f);
        bgRT.anchoredPosition = margin;

        var img = bg.GetComponent<Image>();
        img.color = backgroundColor;
        img.raycastTarget = false;

        var layout = bg.GetComponent<VerticalLayoutGroup>();
        layout.padding = new RectOffset((int)padding.x, (int)padding.x, (int)padding.y, (int)padding.y);
        layout.childControlWidth = layout.childControlHeight = true;
        layout.childForceExpandWidth = layout.childForceExpandHeight = false;

        var fitter = bg.GetComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        var textGO = new GameObject("HUDText", typeof(RectTransform), typeof(TextMeshProUGUI));
        textGO.transform.SetParent(bg.transform, false);
        text = textGO.GetComponent<TextMeshProUGUI>();
        text.fontSize = fontSize;
        text.enableAutoSizing = false;
        text.alignment = TextAlignmentOptions.TopLeft;
        text.textWrappingMode = TextWrappingModes.NoWrap;
        text.richText = true;
        text.raycastTarget = false;
    }

    void Update()
    {
        if (player == null || text == null) return;

        Transform t = player.transform;
        Vector3 moved = t.position - player.StartPosition;

        string hp = health != null
            ? $"\n<b>Health</b>  {health.Health}/{health.maxHealth}   <b>Hits</b>  {health.Hits}"
            : "";

        text.text =
            $"<b>Position</b>  {t.position.x:F1}, {t.position.y:F1}, {t.position.z:F1}\n" +
            $"<b>Moved</b>  {moved.x:F1}, {moved.y:F1}, {moved.z:F1}  (dist {player.TotalDistance:F1})\n" +
            $"<b>Rotation</b>  {t.eulerAngles.y:F0} deg  (total {player.TotalRotation:F0})\n" +
            $"<b>Scale</b>  {t.localScale.x:F2}  (x{player.ScaleFactor:F2}, {(player.Growing ? "growing" : "shrinking")})" +
            hp +
            "\n\n<size=80%>[WASD] Move  [X] Rotate  [Y] Scale  [R] Reset</size>";
    }
}