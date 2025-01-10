using UnityEngine;

[ExecuteAlways] // Permite visualizar alterações no Editor, mesmo fora do Play Mode
public class AttachToHand : MonoBehaviour
{
    [Header("Configuração da Mão")]
    public Transform handAnchor; // Referência para a mão (LeftHandAnchor ou RightHandAnchor)

    [Header("Offset da Baqueta")]
    public Vector3 positionOffset = Vector3.zero; // Offset para ajustar a posição no editor

    [Tooltip("Rotação da baqueta em relação à mão")]
    public Vector3 rotationOffset = new Vector3(0f, 90f, 0f); // Offset de rotação

    private Rigidbody rb;

    void Start()
    {
        if (handAnchor != null)
        {
            // Anexa o objeto à mão
            AttachToHandAtStart();
        }
        else
        {
            Debug.LogError("Hand anchor is not assigned!");
        }
    }

    void AttachToHandAtStart()
    {
        // Define a DrumstickA como filha do RightHandAnchor
        transform.SetParent(handAnchor);

        // Ajusta a posição relativa aplicando o offset
        transform.localPosition = positionOffset;
        transform.localRotation = Quaternion.Euler(rotationOffset);

        // Configura o Rigidbody para manter o objeto fixo
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Mantém fixa na mão
            rb.useGravity = false; // Impede queda
        }
    }

    void LateUpdate()
    {
        // Garante que a Drumstick permaneça centralizada na mão com offset
        if (handAnchor != null)
        {
            transform.position = handAnchor.position + handAnchor.rotation * positionOffset;
            transform.rotation = handAnchor.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
