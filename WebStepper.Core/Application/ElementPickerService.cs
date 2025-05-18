using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStepper.Core.Domain;
using WebStepper.Core.Interfaces;
using Newtonsoft.Json;

namespace WebStepper.Core.Application
{
    /// <summary>
    /// Service for picking elements on the webpage and generating selector options
    /// </summary>
    public class ElementPickerService : IElementPickerService
    {
        private IWebView2Bridge _webView2Bridge;
        private readonly ILogService _logService;
        private bool _isElementPickerActive;

        /// <inheritdoc/>
        public event EventHandler<SelectorOptionsEventArgs> SelectorOptionsGenerated;

        public ElementPickerService(ILogService logService)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
        }

        public void Initialize(IWebView2Bridge webView2Bridge)
        {
            if (_webView2Bridge != null)
            {
                // Remove existing event handler to avoid duplicates
                _webView2Bridge.WebMessageReceived -= OnWebMessageReceived;
            }

            _webView2Bridge = webView2Bridge ?? throw new ArgumentNullException(nameof(webView2Bridge));

            // Register for WebMessage events from the WebView2
            _webView2Bridge.WebMessageReceived += OnWebMessageReceived;
        }

        /// <inheritdoc/>
        public async Task ActivateElementPicker()
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot activate element picker.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (_isElementPickerActive)
            {
                _logService.LogWarning("Element picker is already active");
                return;
            }

            _isElementPickerActive = true;

            // Inject the element picker script
            string script = await LoadElementPickerScript();
            await _webView2Bridge.ExecuteScript(script);

            _logService.LogInfo("Element picker activated");
        }

        /// <inheritdoc/>
        public async Task DeactivateElementPicker()
        {
            if (!_isElementPickerActive)
            {
                _logService.LogWarning("Element picker is not active");
                return;
            }

            // Remove the element picker script
            await _webView2Bridge.ExecuteScript("if(window.__elementPickerActive) { window.__deactivateElementPicker(); }");

            _isElementPickerActive = false;
            _logService.LogInfo("Element picker deactivated");
        }

        /// <inheritdoc/>
        public async Task TestSelector(string selector, string selectorType = "css")
        {
            if (_webView2Bridge == null)
            {
                _logService.LogError("WebView2Bridge not initialized. Cannot activate Test Selector.");
                throw new InvalidOperationException("WebView2Bridge not initialized");
            }

            if (string.IsNullOrWhiteSpace(selector))
            {
                throw new ArgumentException("Selector cannot be null or empty", nameof(selector));
            }

            _logService.LogInfo($"Testing selector: {selector} (type: {selectorType})");

            string script = @"
                (function() {
                    try {
                        const selectorType = '" + selectorType + @"';
                        const selector = '" + selector.Replace("'", "\\'") + @"';
                        let elements = [];

                        if (selectorType === 'css') {
                            elements = Array.from(document.querySelectorAll(selector));
                        } else if (selectorType === 'xpath') {
                            const xpathResult = document.evaluate(selector, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                            for (let i = 0; i < xpathResult.snapshotLength; i++) {
                                elements.push(xpathResult.snapshotItem(i));
                            }
                        }

                        // Highlight the elements
                        const originalStyles = [];
                        elements.forEach((el) => {
                            originalStyles.push({
                                element: el,
                                outlineStyle: el.style.outlineStyle,
                                outlineWidth: el.style.outlineWidth,
                                outlineColor: el.style.outlineColor,
                                backgroundColor: el.style.backgroundColor
                            });

                            el.style.outlineStyle = 'solid';
                            el.style.outlineWidth = '2px';
                            el.style.outlineColor = 'red';
                            el.style.backgroundColor = 'rgba(255, 0, 0, 0.1)';
                        });

                        // Revert styles after 2 seconds
                        setTimeout(() => {
                            originalStyles.forEach((style) => {
                                style.element.style.outlineStyle = style.outlineStyle;
                                style.element.style.outlineWidth = style.outlineWidth;
                                style.element.style.outlineColor = style.outlineColor;
                                style.element.style.backgroundColor = style.backgroundColor;
                            });
                        }, 2000);

                        return {
                            matchCount: elements.length,
                            success: true
                        };
                    } catch (error) {
                        return {
                            matchCount: 0,
                            error: error.message,
                            success: false
                        };
                    }
                })();
            ";

            try
            {
                string result = await _webView2Bridge.ExecuteScript(script);
                var response = JsonConvert.DeserializeObject<dynamic>(result);

                if (response.success)
                {
                    _logService.LogInfo($"Selector test successful: {response.matchCount} elements matched");
                }
                else
                {
                    _logService.LogWarning($"Selector test failed: {response.error}");
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error testing selector: {ex.Message}");
                throw;
            }
        }

        private async Task<string> LoadElementPickerScript()
        {
            // In a real implementation, this would load from a resource file
            // For this example, we'll inline the script
            return @"
                (function() {
                    // Avoid duplicate initialization
                    if (window.__elementPickerActive) {
                        return;
                    }

                    window.__elementPickerActive = true;
                    let hoveredElement = null;
                    let highlightElement = document.createElement('div');

                    // Create highlight element
                    highlightElement.style.position = 'absolute';
                    highlightElement.style.border = '2px solid #4285F4';
                    highlightElement.style.backgroundColor = 'rgba(66, 133, 244, 0.1)';
                    highlightElement.style.pointerEvents = 'none';
                    highlightElement.style.zIndex = '10000';
                    highlightElement.style.display = 'none';
                    document.body.appendChild(highlightElement);

                    // Generate CSS selector for an element
                    function getCssSelector(element) {
                        if (!element) return null;

                        // Try ID selector if available
                        if (element.id) {
                            return '#' + element.id;
                        }

                        // Try class selector if available
                        if (element.className) {
                            const classes = element.className.split(' ').filter(c => c);
                            if (classes.length > 0) {
                                const classSelector = '.' + classes.join('.');
                                // Check if this uniquely identifies the element
                                if (document.querySelectorAll(classSelector).length === 1) {
                                    return classSelector;
                                }
                            }
                        }

                        // Generate a path
                        let path = '';
                        let current = element;

                        while (current && current !== document.body && current !== document) {
                            let selector = current.tagName.toLowerCase();

                            if (current.id) {
                                selector += '#' + current.id;
                                path = selector + (path ? ' > ' + path : '');
                                break;
                            } else {
                                // Add classes
                                if (current.className) {
                                    const classes = current.className.split(' ').filter(c => c);
                                    if (classes.length > 0) {
                                        selector += '.' + classes.join('.');
                                    }
                                }

                                // Check position among siblings
                                let sibling = current;
                                let index = 1;

                                while (sibling = sibling.previousElementSibling) {
                                    if (sibling.tagName === current.tagName) {
                                        index++;
                                    }
                                }

                                if (index > 1) {
                                    selector += ':nth-of-type(' + index + ')';
                                }
                            }

                            path = selector + (path ? ' > ' + path : '');
                            current = current.parentElement;
                        }

                        return path;
                    }

                    // Generate XPath for an element
                    function getXPath(element) {
                        if (!element) return null;

                        const idx = (sib, name) => sib
                            ? idx(sib.previousElementSibling, name||sib.localName) + (sib.localName == name)
                            : 1;
                        const segs = elm => !elm || elm.nodeType !== 1
                            ? ['']
                            : elm.id && document.getElementById(elm.id) === elm
                                ? [`//*[@id='${elm.id}']`]
                                : [...segs(elm.parentNode), `${elm.localName}[${idx(elm)}]`];

                        return segs(element).join('/').replace(/\/$/, '');
                    }

                    // Generate a simple selector
                    function getSimpleSelector(element) {
                        if (!element) return null;

                        // Try ID
                        if (element.id) {
                            return '#' + element.id;
                        }

                        // Try name attribute
                        if (element.name) {
                            const nameSelector = `[name='${element.name}']`;
                            if (document.querySelectorAll(nameSelector).length === 1) {
                                return nameSelector;
                            }
                        }

                        // Try for specific input types
                        if (element.tagName === 'INPUT' && element.type) {
                            const typeSelector = `input[type='${element.type}']`;
                            const inputs = document.querySelectorAll(typeSelector);

                            if (inputs.length === 1) {
                                return typeSelector;
                            }
                        }

                        // Try classes
                        if (element.className) {
                            const classes = element.className.split(' ').filter(c => c);

                            for (const cls of classes) {
                                const classSelector = '.' + cls;
                                if (document.querySelectorAll(classSelector).length === 1) {
                                    return classSelector;
                                }
                            }

                            // Try combinations of 2 classes
                            if (classes.length >= 2) {
                                for (let i = 0; i < classes.length; i++) {
                                    for (let j = i + 1; j < classes.length; j++) {
                                        const twoClassSelector = '.' + classes[i] + '.' + classes[j];
                                        if (document.querySelectorAll(twoClassSelector).length === 1) {
                                            return twoClassSelector;
                                        }
                                    }
                                }
                            }
                        }

                        // Try data attributes
                        for (const attr of element.attributes) {
                            if (attr.name.startsWith('data-')) {
                                const dataSelector = `[${attr.name}='${attr.value}']`;
                                if (document.querySelectorAll(dataSelector).length === 1) {
                                    return dataSelector;
                                }
                            }
                        }

                        // Fall back to tag + attribute combinations
                        const tag = element.tagName.toLowerCase();

                        if (element.hasAttribute('type')) {
                            const attrSelector = `${tag}[type='${element.getAttribute('type')}']`;
                            if (document.querySelectorAll(attrSelector).length === 1) {
                                return attrSelector;
                            }
                        }

                        if (element.hasAttribute('role')) {
                            const attrSelector = `${tag}[role='${element.getAttribute('role')}']`;
                            if (document.querySelectorAll(attrSelector).length === 1) {
                                return attrSelector;
                            }
                        }

                        return null;
                    }

                    // Calculate specificity score
                    function calculateSpecificityScore(selector) {
                        try {
                            let score = 0;

                            // ID selectors
                            const idCount = (selector.match(/#[a-zA-Z0-9_-]+/g) || []).length;
                            score += idCount * 100;

                            // Class selectors, attribute selectors, and pseudo-classes
                            const classCount = (selector.match(/\.[a-zA-Z0-9_-]+|\[[a-zA-Z0-9_-]+[*^$|]?=/g) || []).length;
                            score += classCount * 10;

                            // Type selectors and pseudo-elements
                            const typeCount = (selector.match(/[a-zA-Z0-9_-]+|::[a-zA-Z0-9_-]+/g) || []).length;
                            score += typeCount;

                            return score;
                        } catch (e) {
                            return 0;
                        }
                    }

                    // Get selector recommendations
                    function getSelectorOptions(element) {
                        const options = [];

                        if (!element) return options;

                        // ID selector
                        if (element.id) {
                            const idSelector = '#' + element.id;
                            options.push({
                                type: 'CSS',
                                value: idSelector,
                                specificityScore: 100,
                                matchCount: document.querySelectorAll(idSelector).length
                            });
                        }

                        // Simple selector
                        const simpleSelector = getSimpleSelector(element);
                        if (simpleSelector) {
                            options.push({
                                type: 'CSS',
                                value: simpleSelector,
                                specificityScore: calculateSpecificityScore(simpleSelector),
                                matchCount: document.querySelectorAll(simpleSelector).length
                            });
                        }

                        // Full CSS path
                        const cssPath = getCssSelector(element);
                        options.push({
                            type: 'CSS',
                            value: cssPath,
                            specificityScore: calculateSpecificityScore(cssPath),
                            matchCount: document.querySelectorAll(cssPath).length
                        });

                        // XPath
                        const xpath = getXPath(element);
                        try {
                            const xpathResult = document.evaluate(xpath, document, null, XPathResult.ORDERED_NODE_SNAPSHOT_TYPE, null);
                            options.push({
                                type: 'XPath',
                                value: xpath,
                                specificityScore: 50, // Arbitrary score for XPath
                                matchCount: xpathResult.snapshotLength
                            });
                        } catch (e) {
                            // XPath evaluation failed
                        }

                        // Tag + attribute selectors
                        const tag = element.tagName.toLowerCase();

                        for (const attr of element.attributes) {
                            if (attr.name !== 'style' && attr.name !== 'class' && attr.name !== 'id') {
                                const attrSelector = `${tag}[${attr.name}='${attr.value}']`;
                                try {
                                    const matchCount = document.querySelectorAll(attrSelector).length;
                                    if (matchCount > 0) {
                                        options.push({
                                            type: 'CSS',
                                            value: attrSelector,
                                            specificityScore: calculateSpecificityScore(attrSelector),
                                            matchCount: matchCount
                                        });
                                    }
                                } catch (e) {
                                    // Invalid selector
                                }
                            }
                        }

                        // Sort by specificity score and match count
                        options.sort((a, b) => {
                            // First sort by match count (prefer unique selectors)
                            if (a.matchCount === 1 && b.matchCount !== 1) return -1;
                            if (b.matchCount === 1 && a.matchCount !== 1) return 1;

                            // Then by specificity
                            return b.specificityScore - a.specificityScore;
                        });

                        return options;
                    }

                    // Handler for mouse move
                    function handleMouseMove(e) {
                        const target = e.target;

                        if (target !== hoveredElement) {
                            hoveredElement = target;

                            // Update highlight
                            const rect = target.getBoundingClientRect();
                            highlightElement.style.left = rect.left + window.scrollX + 'px';
                            highlightElement.style.top = rect.top + window.scrollY + 'px';
                            highlightElement.style.width = rect.width + 'px';
                            highlightElement.style.height = rect.height + 'px';
                            highlightElement.style.display = 'block';
                        }
                    }

                    // Handler for element selection
                    function handleElementSelection(e) {
                        e.preventDefault();
                        e.stopPropagation();

                        const target = e.target;
                        const selectorOptions = getSelectorOptions(target);

                        // Get element attributes and content
                        const elementInfo = {
                            tagName: target.tagName.toLowerCase(),
                            attributes: {},
                            textContent: target.textContent.trim().substring(0, 100),
                            innerHtml: target.innerHTML.substring(0, 100),
                            rect: target.getBoundingClientRect()
                        };

                        // Get all attributes
                        for (const attr of target.attributes) {
                            elementInfo.attributes[attr.name] = attr.value;
                        }

                        // Send data back to C#
                        window.chrome.webview.postMessage({
                            type: 'elementSelected',
                            selectorOptions: selectorOptions,
                            elementInfo: elementInfo
                        });

                        // Keep highlighting the selected element
                        const rect = target.getBoundingClientRect();
                        highlightElement.style.left = rect.left + window.scrollX + 'px';
                        highlightElement.style.top = rect.top + window.scrollY + 'px';
                        highlightElement.style.width = rect.width + 'px';
                        highlightElement.style.height = rect.height + 'px';
                        highlightElement.style.border = '2px solid #FF4081';
                        highlightElement.style.backgroundColor = 'rgba(255, 64, 129, 0.1)';
                    }

                    // Deactivate element picker
                    window.__deactivateElementPicker = function() {
                        document.removeEventListener('mousemove', handleMouseMove);
                        document.removeEventListener('click', handleElementSelection, true);
                        if (highlightElement.parentNode) {
                            highlightElement.parentNode.removeChild(highlightElement);
                        }
                        window.__elementPickerActive = false;

                        // Send message to C#
                        window.chrome.webview.postMessage({
                            type: 'elementPickerDeactivated'
                        });
                    };

                    // Activate event listeners
                    document.addEventListener('mousemove', handleMouseMove);
                    document.addEventListener('click', handleElementSelection, true);

                    // Send message to C#
                    window.chrome.webview.postMessage({
                        type: 'elementPickerActivated'
                    });
                })();
            ";
        }

        private void OnWebMessageReceived(object sender, WebMessageEventArgs e)
        {
            try
            {
                // Deserialize the message
                var message = JsonConvert.DeserializeObject<dynamic>(e.WebMessageAsJson);
                string messageType = (string)message.type;

                if (messageType == "elementSelected")
                {
                    _logService.LogInfo("Element selected by user");
                    var options = new List<SelectorOption>();

                    foreach (var option in message.selectorOptions)
                    {
                        options.Add(new SelectorOption
                        {
                            Type = (string)option.type,
                            Value = (string)option.value,
                            SpecificityScore = (int)option.specificityScore,
                            MatchCount = (int)option.matchCount,
                            MatchPreview = (string)option.value
                        });
                    }

                    // Element information
                    var elementInfo = new Dictionary<string, object>();
                    elementInfo["tagName"] = (string)message.elementInfo.tagName;
                    elementInfo["textContent"] = (string)message.elementInfo.textContent;

                    // Attributes
                    var attributes = new Dictionary<string, string>();
                    foreach (var attr in message.elementInfo.attributes)
                    {
                        attributes[attr.Name] = (string)attr.Value;
                    }
                    elementInfo["attributes"] = attributes;

                    // Automatically deactivate the element picker
                    _ = DeactivateElementPicker();

                    // Raise event with the options
                    SelectorOptionsGenerated?.Invoke(this, new SelectorOptionsEventArgs
                    {
                        SelectorOptions = options,
                        ElementInfo = elementInfo
                    });

                    _logService.LogInfo($"Generated {options.Count} selector options");
                }
                else if (messageType == "elementPickerActivated")
                {
                    _logService.LogInfo("Element picker activated");
                }
                else if (messageType == "elementPickerDeactivated")
                {
                    _isElementPickerActive = false;
                    _logService.LogInfo("Element picker deactivated");
                }
            }
            catch (Exception ex)
            {
                _logService.LogError($"Error processing web message: {ex.Message}");
            }
        }
    }
}
