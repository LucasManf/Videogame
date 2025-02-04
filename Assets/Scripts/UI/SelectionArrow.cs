using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    private RectTransform rect;
    private int currentPosition;
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;


    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //cambiar posicion del selector
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }

        //interactuar
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(currentPosition < 0)
        {
            currentPosition = options.Length - 1;
        }
        else if(currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        //asignar la posicion actual del eje Y a el puntero de seleccion
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);

        if(_change != 0)
        {
            SoundManager.instance.PlaySFX(1);
        }
    }

    private void Interact()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
        SoundManager.instance.PlaySFX(0);
    }

}
