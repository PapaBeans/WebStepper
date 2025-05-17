/**
 * ElementPicker.js
 * A JavaScript module for visually selecting elements on a web page and generating optimal CSS selectors
 */

(function() {
    // State
    let enabled = false;
    let hoveredElement = null;
    let overlayElement = null;
    let hoverBoxElement = null;
    let messageBoxElement = null;
    let selectedElement = null;
    let callbackName = '';
    
    // Configuration
    const config = {
        overlayZIndex: 9999,
        overlayColor: 'rgba(0, 0, 0, 0.3)',
        hoverBoxColor: 'rgba(52, 152, 219, 0.5)',
        hoverBoxBorder: '2px solid rgba(52, 152, 219, 1)',
        messageBoxBackground: 'white',
        messageBoxColor: 'black',
        messageBoxPadding: '8px 12px',
        messageBoxBorder: '1px solid #ccc',
        messageBoxZIndex: 10000,
        messageBoxMaxWidth: '300px'
    };

    /**
     * Initialize the element picker
     * @param {string} callback - Name of the callback function to call when an element is selected
     */
    function initialize(callback) {
        callbackName = callback || 'onElementSelected';
        
        // Create overlay if it doesn't exist
        if (!overlayElement) {
            overlayElement = document.createElement('div');
            overlayElement.style.position = 'fixed';
            overlayElement.style.top = '0';
            overlayElement.style.left = '0';
            overlayElement.style.width = '100%';
            overlayElement.style.height = '100%';
            overlayElement.style.backgroundColor = config.overlayColor;
            overlayElement.style.zIndex = config.overlayZIndex;
            overlayElement.style.pointerEvents = 'none'; // Allow click events to pass through
            overlayElement.style.display = 'none';
            
            document.body.appendChild(overlayElement);
        }
        
        // Create hover box if it doesn't exist
        if (!hoverBoxElement) {
            hoverBoxElement = document.createElement('div');
            hoverBoxElement.style.position = 'absolute';
            hoverBoxElement.style.border = config.hoverBoxBorder;
            hoverBoxElement.style.backgroundColor = config.hoverBoxColor;
            hoverBoxElement.style.zIndex = config.overlayZIndex + 1;
            hoverBoxElement.style.display = 'none';
            hoverBoxElement.style.pointerEvents = 'none'; // Allow click events to pass through
            
            document.body.appendChild(hoverBoxElement);
        }
        
        // Create message box if it doesn't exist
        if (!messageBoxElement) {
            messageBoxElement = document.createElement('div');
            messageBoxElement.style.position = 'fixed';
            messageBoxElement.style.bottom = '20px';
            messageBoxElement.style.right = '20px';
            messageBoxElement.style.backgroundColor = config.messageBoxBackground;
            messageBoxElement.style.color = config.messageBoxColor;
            messageBoxElement.style.padding = config.messageBoxPadding;
            messageBoxElement.style.border = config.messageBoxBorder;
            messageBoxElement.style.zIndex = config.messageBoxZIndex;
            messageBoxElement.style.maxWidth = config.messageBoxMaxWidth;
            messageBoxElement.style.display = 'none';
            messageBoxElement.style.borderRadius = '4px';
            messageBoxElement.style.boxShadow = '0 2px 8px rgba(0,0,0,0.1)';
            messageBoxElement.style.fontFamily = 'Arial, sans-serif';
            messageBoxElement.style.fontSize = '14px';
            
            document.body.appendChild(messageBoxElement);
        }
    }

    /**
     * Start the element picker
     */
    function start() {
        if (enabled) return;
        enabled = true;
        
        // Show overlay and message
        overlayElement.style.display = 'block';
        updateMessage('Move cursor over elements to highlight them. Click to select an element.');
        
        // Attach event listeners
        document.addEventListener('mousemove', handleMouseMove);
        document.addEventListener('click', handleClick);
        document.addEventListener('keydown', handleKeyDown);
    }

    /**
     * Stop the element picker
     */
    function stop() {
        if (!enabled) return;
        enabled = false;
        
        // Hide elements
        overlayElement.style.display = 'none';
        hoverBoxElement.style.display = 'none';
        messageBoxElement.style.display = 'none';
        
        // Remove event listeners
        document.removeEventListener('mousemove', handleMouseMove);
        document.removeEventListener('click', handleClick);
        document.removeEventListener('keydown', handleKeyDown);
        
        // Clear references
        hoveredElement = null;
        selectedElement = null;
    }

    /**
     * Handle mouse movement
     * @param {MouseEvent} event - The mouse event
     */
    function handleMouseMove(event) {
        if (!enabled) return;
        
        // Get element under cursor (ignoring our overlay elements)
        const elements = document.elementsFromPoint(event.clientX, event.clientY);
        let target = null;
        
        for (const elem of elements) {
            if (elem !== overlayElement && 
                elem !== hoverBoxElement && 
                elem !== messageBoxElement) {
                target = elem;
                break;
            }
        }
        
        if (target && target !== hoveredElement) {
            hoveredElement = target;
            highlightElement(target);
            
            // Update message with element info
            const selector = generateBestSelector(target);
            updateMessage(`Element: ${getElementDescription(target)}<br>Selector: <code>${selector}</code>`);
        }
    }

    /**
     * Handle click events
     * @param {MouseEvent} event - The click event
     */
    function handleClick(event) {
        if (!enabled) return;
        
        event.preventDefault();
        event.stopPropagation();
        
        if (hoveredElement) {
            selectedElement = hoveredElement;
            const selectorInfo = generateSelectors(selectedElement);
            
            // Call back to C# application with selected element info
            if (window[callbackName] && typeof window[callbackName] === 'function') {
                window[callbackName](JSON.stringify(selectorInfo));
            } else {
                console.error(`Callback function "${callbackName}" not found`);
            }
            
            // Stop picking
            stop();
        }
        
        return false;
    }

    /**
     * Handle keyboard events
     * @param {KeyboardEvent} event - The keyboard event
     */
    function handleKeyDown(event) {
        if (!enabled) return;
        
        // Cancel on Escape key
        if (event.key === 'Escape') {
            stop();
            
            // Notify cancellation
            if (window[callbackName] && typeof window[callbackName] === 'function') {
                window[callbackName]('{"canceled":true}');
            }
        }
    }

    /**
     * Highlight an element with a box
     * @param {HTMLElement} element - The element to highlight
     */
    function highlightElement(element) {
        if (!element) return;
        
        const rect = element.getBoundingClientRect();
        
        hoverBoxElement.style.top = `${window.scrollY + rect.top}px`;
        hoverBoxElement.style.left = `${window.scrollX + rect.left}px`;
        hoverBoxElement.style.width = `${rect.width}px`;
        hoverBoxElement.style.height = `${rect.height}px`;
        hoverBoxElement.style.display = 'block';
    }

    /**
     * Update the message box with a message
     * @param {string} message - The message to display
     */
    function updateMessage(message) {
        if (!messageBoxElement) return;
        
        messageBoxElement.innerHTML = message;
        messageBoxElement.style.display = 'block';
    }

    /**
     * Get a human-readable description of an element
     * @param {HTMLElement} element - The element to describe
     * @return {string} A description of the element
     */
    function getElementDescription(element) {
        if (!element) return 'Unknown Element';
        
        let description = element.tagName.toLowerCase();
        
        if (element.id) {
            description += `#${element.id}`;
        }
        
        if (element.className && typeof element.className === 'string') {
            const classes = element.className.trim().split(/\s+/);
            if (classes.length > 0 && classes[0] !== '') {
                description += `.${classes.join('.')}`;
            }
        }
        
        // If it's an input, add the type
        if (element.tagName.toLowerCase() === 'input' && element.type) {
            description += `[type=${element.type}]`;
        }
        
        // Add text content preview if it exists
        const textContent = element.textContent?.trim();
        if (textContent && textContent.length > 0) {
            const preview = textContent.length > 20 ? 
                textContent.substring(0, 20) + '...' : 
                textContent;
            description += ` "${preview}"`;
        }
        
        return description;
    }

    /**
     * Generate the best CSS selector for an element
     * @param {HTMLElement} element - The element to generate a selector for
     * @return {string} The CSS selector
     */
    function generateBestSelector(element) {
        const selectors = generateSelectors(element);
        return selectors.optimal;
    }

    /**
     * Generate multiple CSS selectors for an element
     * @param {HTMLElement} element - The element to generate selectors for
     * @return {Object} Various selector strategies
     */
    function generateSelectors(element) {
        if (!element) return { optimal: '', alternatives: [] };
        
        // ID-based selector (most reliable if ID exists)
        let idSelector = '';
        if (element.id) {
            idSelector = `#${CSS.escape(element.id)}`;
        }
        
        // Class-based selector
        let classSelector = '';
        if (element.className && typeof element.className === 'string') {
            const classes = element.className.trim().split(/\s+/).filter(c => c);
            if (classes.length > 0) {
                classSelector = element.tagName.toLowerCase() + 
                    classes.map(c => `.${CSS.escape(c)}`).join('');
            }
        }
        
        // Attribute-based selectors
        const attributes = [];
        
        // Special handling for input elements
        if (element.tagName.toLowerCase() === 'input' && element.type) {
            attributes.push(`input[type="${element.type}"]`);
            
            if (element.name) {
                attributes.push(`input[type="${element.type}"][name="${CSS.escape(element.name)}"]`);
            }
        }
        
        // Common identifying attributes
        ['name', 'placeholder', 'aria-label', 'data-test', 'data-testid', 'data-cy', 'data-qa']
            .forEach(attr => {
                if (element.hasAttribute(attr)) {
                    attributes.push(
                        `${element.tagName.toLowerCase()}[${attr}="${CSS.escape(element.getAttribute(attr))}"]`
                    );
                }
            });
        
        // Position-based selector (least reliable, most stable)
        let positionSelector = '';
        try {
            positionSelector = getCssSelectorByPosition(element);
        } catch (error) {
            console.error('Error generating position selector:', error);
        }
        
        // Determine the optimal selector
        let optimal = idSelector; // ID is the most reliable if available
        
        if (!optimal) {
            // If no ID, try a class selector if it uniquely identifies the element
            if (classSelector && document.querySelectorAll(classSelector).length === 1) {
                optimal = classSelector;
            } 
            // If no unique class selector, try attribute selectors
            else if (attributes.length > 0) {
                // Find the first attribute selector that uniquely identifies the element
                for (const attrSelector of attributes) {
                    if (document.querySelectorAll(attrSelector).length === 1) {
                        optimal = attrSelector;
                        break;
                    }
                }
            }
            
            // If still no optimal selector, use position as a last resort
            if (!optimal) {
                optimal = positionSelector;
            }
        }
        
        return {
            optimal,
            id: idSelector || null,
            class: classSelector || null,
            attributes: attributes.length > 0 ? attributes : null,
            position: positionSelector,
            alternatives: [
                idSelector, 
                classSelector,
                ...attributes,
                positionSelector
            ].filter(s => s)
        };
    }

    /**
     * Generate a CSS selector based on element's position in the DOM
     * @param {HTMLElement} element - The element to generate a selector for
     * @return {string} The position-based CSS selector
     */
    function getCssSelectorByPosition(element) {
        if (!element) return '';
        if (element === document.body) return 'body';
        if (element === document.documentElement) return 'html';
        
        let current = element;
        let path = [];
        
        while (current && current.nodeType === Node.ELEMENT_NODE) {
            let selector = current.tagName.toLowerCase();
            
            // Add positional selector if element has siblings of the same type
            const parent = current.parentNode;
            if (parent) {
                const siblings = Array.from(parent.children).filter(
                    e => e.tagName === current.tagName
                );
                
                if (siblings.length > 1) {
                    const index = siblings.indexOf(current) + 1;
                    selector += `:nth-child(${index})`;
                }
            }
            
            path.unshift(selector);
            
            // Move up to parent
            current = current.parentNode;
            
            // Stop at document or body
            if (current === document.body || current === document.documentElement) {
                path.unshift(current.tagName.toLowerCase());
                break;
            }
        }
        
        return path.join(' > ');
    }

    /**
     * Test a CSS selector and return information about matching elements
     * @param {string} selector - The CSS selector to test
     * @return {Object} Information about the matching elements
     */
    function testSelector(selector) {
        try {
            const elements = document.querySelectorAll(selector);
            
            return {
                isValid: true,
                count: elements.length,
                message: elements.length === 1 
                    ? 'Selector uniquely identifies one element.' 
                    : `Selector matches ${elements.length} elements.`
            };
        } catch (error) {
            return {
                isValid: false,
                count: 0,
                message: `Invalid selector: ${error.message}`
            };
        }
    }

    /**
     * Highlight all elements that match a selector
     * @param {string} selector - The CSS selector to test
     * @param {string} color - Color for the highlight
     */
    function highlightMatchingElements(selector, color = 'rgba(255, 127, 80, 0.5)') {
        // Remove any previous highlight elements
        const previousHighlights = document.querySelectorAll('.element-picker-highlight');
        previousHighlights.forEach(element => element.remove());
        
        try {
            const elements = document.querySelectorAll(selector);
            
            elements.forEach(element => {
                const rect = element.getBoundingClientRect();
                const highlight = document.createElement('div');
                
                highlight.className = 'element-picker-highlight';
                highlight.style.position = 'absolute';
                highlight.style.top = `${window.scrollY + rect.top}px`;
                highlight.style.left = `${window.scrollX + rect.left}px`;
                highlight.style.width = `${rect.width}px`;
                highlight.style.height = `${rect.height}px`;
                highlight.style.backgroundColor = color;
                highlight.style.border = '2px dashed orange';
                highlight.style.zIndex = config.overlayZIndex - 1;
                highlight.style.pointerEvents = 'none';
                
                document.body.appendChild(highlight);
                
                // Remove the highlight after 2 seconds
                setTimeout(() => {
                    highlight.remove();
                }, 2000);
            });
            
            return {
                count: elements.length,
                message: `Highlighted ${elements.length} matching elements.`
            };
        } catch (error) {
            return {
                count: 0,
                message: `Error: ${error.message}`
            };
        }
    }

    // Expose the API
    window.ElementPicker = {
        initialize,
        start,
        stop,
        testSelector,
        highlightMatchingElements,
        getElementDescription,
        generateSelectors
    };
})();