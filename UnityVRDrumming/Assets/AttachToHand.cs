using UnityEngine;

[ExecuteAlways] // Permite visualizar altera��es no Editor, mesmo fora do Play Mode
public class AttachToHand : MonoBehaviour
{
    [Header("Configura��o da M�o")]
    public Transform handAnchor; // Refer�ncia para a m�o (LeftHandAnchor ou RightHandAnchor)

    [Header("Offset da Baqueta")]
    public Vector3 positionOffset = Vector3.zero; // Offset para ajustar a posi��o no editor

    [Tooltip("Rota��o da baqueta em rela��o � m�o")]
    public Vector3 rotationOffset = new Vector3(0f, 90f, 0f); // Offset de rota��o

    private Rigidbody rb;

    void Start()
    {
        if (handAnchor != null)
        {
            // Anexa o objeto � m�o
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

        // Ajusta a posi��o relativa aplicando o offset
        transform.localPosition = positionOffset;
        transform.localRotation = Quaternion.Euler(rotationOffset);

        // Configura o Rigidbody para manter o objeto fixo
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Mant�m fixa na m�o
            rb.useGravity = false; // Impede queda
        }
    }

    void LateUpdate()
    {
        // Garante que a Drumstick permane�a centralizada na m�o com offset
        if (handAnchor != null)
        {
            transform.position = handAnchor.position + handAnchor.rotation * positionOffset;
            transform.rotation = handAnchor.rotation * Quaternion.Euler(rotationOffset);
        }
    }
}
