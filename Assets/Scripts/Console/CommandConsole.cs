using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class CommandConsole : MonoBehaviour
{
    public GameObject consolePanel;
    public TMP_InputField inputField;
    public TMP_Text outputText;
    public Button toggleConsoleButton;
    private InputSystem_Actions controls;
    private CommandRegistry registry;

    private void Awake()
    {
        controls = new InputSystem_Actions();

        
        controls.UI.ToggleConsole.performed += ctx => ToggleConsole();
        controls.UI.SubmitCommand.performed += ctx => {
            if (consolePanel.activeSelf && inputField.isFocused)
                SubmitCommand();
        };

        
        toggleConsoleButton.onClick.AddListener(ToggleConsole);

        registry = new CommandRegistry();
        registry.Register(new HelpCommand(registry));
        registry.Register(new AliassesCommand(registry));
        registry.Register(new PlayAnimationCommand());

        Application.logMessageReceived += HandleUnityLog;
        HideConsole();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    public void SubmitCommand()
    {
        string input = inputField.text.Trim();

        if (string.IsNullOrEmpty(input))
            return;

        AppendOutput($"> {input}");

        string[] split = input.Split(' ');
        string commandName = split[0];
        string[] args = split.Length > 1 ? input.Substring(commandName.Length + 1).Split(' ') : new string[0];

        var command = registry.GetCommand(commandName);
        if (command != null)
        {
            command.Execute(args);
        }
        else
        {
            Debug.LogWarning($"Unknown command: {commandName}");
        }

        inputField.text = "";
        inputField.ActivateInputField();
    }

    private void AppendOutput(string message)
    {
        outputText.text = message;
    }

    private void HandleUnityLog(string condition, string stackTrace, LogType type)
    {
        AppendOutput(condition);
    }

    public void ToggleConsole()
    {
        if (consolePanel.activeSelf)
            HideConsole();
        else
            ShowConsole();
    }

    private void ShowConsole()
    {
        consolePanel.SetActive(true);
        inputField.ActivateInputField();
    }

    private void HideConsole()
    {
        consolePanel.SetActive(false);
    }
}
