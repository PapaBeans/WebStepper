# FarmersAuto - Insurance Quote Automation

FarmersAuto is a .NET Framework 4.8 Windows Forms application that automates interactions with insurance quoting websites, particularly Farmers Insurance, using WebView2 and JSON-based templates.

## Features

- **WebView2 Integration**: Uses Microsoft's modern WebView2 control for reliable web automation
- **Template-Based Automation**: Define automation steps in easy-to-edit JSON templates
- **Step-by-Step Debugging**: Pause, resume, and single-step through automation sequences
- **Template Management**: Create, edit, and manage automation templates
- **Version History**: Track changes to templates with automatic versioning
- **Detailed Logging**: Comprehensive logging of all automation actions and errors

## System Requirements

- Windows 7 or later
- .NET Framework 4.8
- WebView2 Runtime (will be prompted to install if not found)
- 4GB RAM (minimum)
- 50MB disk space

## Installation

### Option 1: Install from Release Package

1. Download the latest release from the Releases section
2. Extract the ZIP file to a location of your choice
3. Run `FarmersAuto.exe`

### Option 2: Build from Source

1. Clone this repository or download the source code
2. Open `FarmersAuto.sln` in Visual Studio 2019 or later
3. Restore the NuGet packages
4. Build the solution (select "Release" configuration for best performance)
5. The compiled application will be in the `bin\Release` folder

## Quick Start Guide

1. Launch the application
2. Click "Load Template" to select an existing automation template, or create a new one
3. The default template targets Farmers Auto Insurance quotes
4. Click "Start" to begin the automation process
5. Use the "Pause", "Resume", "Step", or "Reset" buttons to control the automation

## Creating Custom Templates

Templates are JSON files with a specific structure:

```json
{
  "version": "1.0",
  "description": "Template description",
  "targetUrl": "https://example.com/insurance-quote",
  "steps": [
    {
      "type": "wait_for_element",
      "selector": "#formElement",
      "description": "Wait for form to load"
    },
    {
      "type": "fill_form",
      "selector": "#nameField",
      "value": "John Doe",
      "description": "Enter name"
    }
  ]
}
```

### Step Types

- **wait_for_element**: Waits for an element to appear on the page
- **fill_form**: Enters text into a form field
- **click_button**: Clicks a button or element
- **execute_script**: Executes custom JavaScript code

### Creating a New Template

1. Click "New Template" 
2. Enter a name for your template
3. Edit the template in the editor
4. Click "Validate" to check for syntax errors
5. Click "Save" when done

## Advanced Features

### Template Versioning

The application automatically maintains a history of template changes:

1. Click "Version History" when a template is loaded
2. Select a previous version to view or restore it
3. Use "Compare" to see differences between versions

### Step-by-Step Debugging

For troubleshooting automation sequences:

1. Click "Pause" during automation to halt execution
2. Click "Step" to execute just one step at a time
3. Click "Resume" to continue normal execution
4. Click "Reset" to stop and reset the automation

### Logging

The application maintains detailed logs of all actions:

1. View logs in the log panel at the bottom right
2. Export logs using File > Export Logs
3. Logs include timestamps and all automation actions

## Troubleshooting

### WebView2 Issues

- If you receive a WebView2 initialization error, make sure you have the WebView2 Runtime installed
- Download it from: https://developer.microsoft.com/en-us/microsoft-edge/webview2/

### Template Errors

- Use the "Validate" button in the template editor to check for syntax errors
- Ensure that selectors are valid CSS selectors that match elements on the page
- Test selectors in the browser's developer tools before adding them to templates

### Automation Failures

- If elements are not found, try increasing wait times between steps
- Some websites use AJAX heavily - you may need to add extra wait_for_element steps
- Check for dynamic IDs that change between page loads
- Use more general selectors like class names instead of IDs if elements are dynamic

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- Microsoft WebView2 team for the excellent browser control
- Newtonsoft.Json for JSON handling

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.