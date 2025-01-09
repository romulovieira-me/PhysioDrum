using UnityEngine;

public class AttachToHand : MonoBehaviour
{
    public Transform handAnchor; // Referência para a mão (LeftHandAnchor ou RightHandAnchor)
    public Vector3 positionOffset = new Vector3(0f, 0.1f, 0.2f); // Inicialize com valores para mover a baqueta


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
        transform.localRotation = Quaternion.Euler(0f, 90f, 0f); // Ajuste a rotação para alinhar a baqueta corretamente

        Debug.Log("DrumstickA Parent: " + transform.parent.name);
        Debug.Log("Drumstick Global Position: " + transform.position);

        // Configura o Rigidbody para interagir com colisões
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false; // Permite física
            rb.useGravity = false;  // Impede queda
        }
    }

    void LateUpdate()
    {
        Debug.Log("Drumstick Global Position Atualizada: " + transform.position);
    }


    void Update()
    {
        // Garante que a drumstick fique alinhada à mão mesmo após colisões
        if (handAnchor != null)
        {
            transform.position = handAnchor.position;
            transform.rotation = handAnchor.rotation;
        }
    }



}
