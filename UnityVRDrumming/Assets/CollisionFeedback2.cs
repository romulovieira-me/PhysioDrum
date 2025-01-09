using UnityEngine;

public class CollisionFeedback2 : MonoBehaviour
{
    private Renderer objRenderer;
    public Color collisionColor = Color.red;
    private Color originalColor;
    public float feedbackDuration = 0.5f;
    public int skinMaterialIndex = 0; // Index of the Skin material

    void Start()
    {
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.materials[skinMaterialIndex].color;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Change the color of the Skin material to give collision feedback
        objRenderer.materials[skinMaterialIndex].color = collisionColor;
        Invoke("ResetColor", feedbackDuration); // Reset the color
    }

    void ResetColor()
    {
        objRenderer.materials[skinMaterialIndex].color = originalColor;
    }
}
