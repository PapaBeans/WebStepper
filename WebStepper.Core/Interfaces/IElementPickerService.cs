using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebStepper.Core.Domain;

namespace WebStepper.Core.Interfaces
{
    /// <summary>
    /// Interface for the element picker service that allows users to select elements on web pages
    /// and generate selectors for automation.
    /// </summary>
    public interface IElementPickerService
    {
        /// <summary>
        /// Event triggered when selector options are generated after a user selects an element.
        /// Note: When this event is raised, the element picker will automatically deactivate.
        /// </summary>
        event EventHandler<SelectorOptionsEventArgs> SelectorOptionsGenerated;

        /// <summary>
        /// Activates the element picker mode to allow the user to select an element on the page.
        /// The picker will automatically deactivate once an element is selected.
        /// </summary>
        Task ActivateElementPicker();

        /// <summary>
        /// Manually deactivates the element picker mode if needed.
        /// Note: Element picker automatically deactivates after an element is selected.
        /// </summary>
        Task DeactivateElementPicker();

        /// <summary>
        /// Tests a selector by highlighting matching elements on the page for visual verification
        /// </summary>
        /// <param name="selector">Selector to test</param>
        /// <param name="selectorType">Type of selector (css or xpath)</param>
        Task TestSelector(string selector, string selectorType = "css");
    }

    /// <summary>
    /// Event arguments for selector options events
    /// </summary>
    public class SelectorOptionsEventArgs : EventArgs
    {
        /// <summary>
        /// List of selector options for the selected element
        /// </summary>
        public List<SelectorOption> SelectorOptions { get; set; }

        /// <summary>
        /// Information about the selected element including tag name, attributes, and content
        /// </summary>
        public Dictionary<string, object> ElementInfo { get; set; }
    }
}
