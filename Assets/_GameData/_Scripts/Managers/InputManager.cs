using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    private bool isMousePressed;
    private float horizontalInput;

    public bool IsMousePressed()
    {
        return isMousePressed;
    }

    public float GetHorizontalInput()
    {
        return horizontalInput;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        // Check if the left mouse button is pressed
        isMousePressed = Input.GetMouseButton(0);

        // Read the horizontal input from mouse movement
        horizontalInput = Input.GetAxis("Mouse X");
    }
}
