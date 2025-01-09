using UnityEngine;
using TMPro; // Certifique-se de ter importado o pacote TextMeshPro.

public class TaskManager : MonoBehaviour
{
    public TextMeshPro textMeshPro;       // Texto para as tarefas.
    public TextMeshPro titleTextMeshPro; // Texto para os títulos.

    private int currentTaskIndex = 0; // Índice da tarefa atual.

    // Lista de tarefas.
    private string[] tasks = new string[]
    {
        "6\t\t6\t\t6\t\t6\n\t\t\t\t5\t\t\t\t\n4\t\t4\t\t\t\t",
        "6\t\t6\t\t6\t\t6\n\t\t\t\t5\t\t\t\t\n4\t\t\t\t\t\t4",
        "6\t\t6\t\t6\t\t6\n\t\t5\t\t\t\t5\n4\t\t\t\t4\t\t\t"
    };

    // Lista de títulos.
    private string[] titles = new string[]
    {
        "TASK #1",
        "TASK #2",
        "TASK #3"
    };

    void Start()
    {
        // Exibe a primeira tarefa e título assim que o jogo começa.
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
        // Avança para a próxima tarefa, voltando ao início quando chega ao final.
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
            Debug.LogWarning("TextMeshPro para tarefas não está atribuído! Arraste o objeto no Inspector.");
        }

        // Atualiza o título da tarefa.
        if (titleTextMeshPro != null)
        {
            titleTextMeshPro.text = titles[currentTaskIndex];
        }
        else
        {
            Debug.LogWarning("TextMeshPro para títulos não está atribuído! Arraste o objeto no Inspector.");
        }
    }
}
