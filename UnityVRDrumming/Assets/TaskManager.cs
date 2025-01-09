using UnityEngine;
using TMPro; // Certifique-se de ter importado o pacote TextMeshPro.

public class TaskManager : MonoBehaviour
{
    public TextMeshPro textMeshPro;       // Texto para as tarefas.
    public TextMeshPro titleTextMeshPro; // Texto para os t�tulos.

    private int currentTaskIndex = 0; // �ndice da tarefa atual.

    // Lista de tarefas.
    private string[] tasks = new string[]
    {
        "6\t\t6\t\t6\t\t6\n\t\t\t\t5\t\t\t\t\n4\t\t4\t\t\t\t",
        "6\t\t6\t\t6\t\t6\n\t\t\t\t5\t\t\t\t\n4\t\t\t\t\t\t4",
        "6\t\t6\t\t6\t\t6\n\t\t5\t\t\t\t5\n4\t\t\t\t4\t\t\t"
    };

    // Lista de t�tulos.
    private string[] titles = new string[]
    {
        "TASK #1",
        "TASK #2",
        "TASK #3"
    };

    void Start()
    {
        // Exibe a primeira tarefa e t�tulo assim que o jogo come�a.
        UpdateTask();
    }

    void Update()
    {
        // Verifica se a tecla ENTER foi pressionada.
        if (Input.GetKeyDown(KeyCode.Return)) // Return representa a tecla ENTER.
        {
            OnNextButtonClick();
        }
    }

    public void OnNextButtonClick()
    {
        // Avan�a para a pr�xima tarefa, voltando ao in�cio quando chega ao final.
        currentTaskIndex = (currentTaskIndex + 1) % tasks.Length;
        UpdateTask();
    }

    private void UpdateTask()
    {
        // Atualiza o texto da tarefa.
        if (textMeshPro != null)
        {
            textMeshPro.text = tasks[currentTaskIndex];
        }
        else
        {
            Debug.LogWarning("TextMeshPro para tarefas n�o est� atribu�do! Arraste o objeto no Inspector.");
        }

        // Atualiza o t�tulo da tarefa.
        if (titleTextMeshPro != null)
        {
            titleTextMeshPro.text = titles[currentTaskIndex];
        }
        else
        {
            Debug.LogWarning("TextMeshPro para t�tulos n�o est� atribu�do! Arraste o objeto no Inspector.");
        }
    }
}
