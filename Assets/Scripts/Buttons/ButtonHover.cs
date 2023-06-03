using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject buttonContainer;
    public GameObject button;
    public GameObject shadow;

    private Vector2 initialContainerOffsetsMin;
    private Vector2 initialContainerOffsetsMax;
    private Color initialContainerColor;
    private Color initialShadowColor;
    private Color initialTextColor;

    private Vector2 targetContainerOffsetsMin;
    private Vector2 targetContainerOffsetsMax;
    private Color targetContainerColor;
    private Color targetShadowColor;
    private Color targetTextColor;

    // How much to stretch the button
    public float stretchFactor = 20f;  // Stretch by 20 units
    public Color hoverBackgroundColor = Color.white;
    public Color hoverShadowColor = Color.white;
    public Color hoverTextColor = Color.black;

    // Speed of the hover effect
    public float hoverSpeed = 10f;

    private RectTransform containerRect;
    private Image containerImage;
    private Image shadowImage;
    private TMP_Text buttonText;

    void Start()
    {
        containerRect = buttonContainer.GetComponent<RectTransform>();
        containerImage = buttonContainer.GetComponent<Image>();
        shadowImage = shadow.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<TMP_Text>();

        initialContainerOffsetsMin = containerRect.offsetMin;
        initialContainerOffsetsMax = containerRect.offsetMax;
        initialContainerColor = containerImage.color;
        initialShadowColor = shadowImage.color;
        initialTextColor = buttonText.color;

        targetContainerOffsetsMin = initialContainerOffsetsMin;
        targetContainerOffsetsMax = initialContainerOffsetsMax;
        targetContainerColor = initialContainerColor;
        targetShadowColor = initialShadowColor;
        targetTextColor = initialTextColor;
    }

    void Update()
    {
        // Smoothly interpolate towards the target state
        containerRect.offsetMin = Vector2.Lerp(containerRect.offsetMin, targetContainerOffsetsMin, hoverSpeed * Time.deltaTime);
        containerRect.offsetMax = Vector2.Lerp(containerRect.offsetMax, targetContainerOffsetsMax, hoverSpeed * Time.deltaTime);
        containerImage.color = Color.Lerp(containerImage.color, targetContainerColor, hoverSpeed * Time.deltaTime);
        shadowImage.color = Color.Lerp(shadowImage.color, targetShadowColor, hoverSpeed * Time.deltaTime);
        buttonText.color = Color.Lerp(buttonText.color, targetTextColor, hoverSpeed * Time.deltaTime);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Set the target state to the hover state
        targetContainerOffsetsMin = new Vector2(initialContainerOffsetsMin.x - stretchFactor, initialContainerOffsetsMin.y);
        targetContainerOffsetsMax = new Vector2(initialContainerOffsetsMax.x + stretchFactor, initialContainerOffsetsMax.y);
        targetContainerColor = hoverBackgroundColor;
        targetShadowColor = hoverShadowColor;
        targetTextColor = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Set the target state back to the initial state
        targetContainerOffsetsMin = initialContainerOffsetsMin;
        targetContainerOffsetsMax = initialContainerOffsetsMax;
        targetContainerColor = initialContainerColor;
        targetShadowColor = initialShadowColor;
        targetTextColor = initialTextColor;
    }
}
