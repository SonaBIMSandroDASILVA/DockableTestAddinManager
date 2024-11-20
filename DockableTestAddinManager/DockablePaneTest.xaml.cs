using Autodesk.Revit.DB; // Library to work with Revit's database
using Autodesk.Revit.UI; // Library for Revit's user interface
using System;
using System.Diagnostics; // Library for debugging and logging
using System.Reflection; // Library to access assembly metadata
using System.Windows; // Base library for general UI functions
using System.Windows.Controls; // Library for building user interface elements

namespace DockableTestAddinManager
{
    /// <summary>
    /// Interaction logic for DockablePaneTest.xaml
    /// </summary>
    public partial class DockablePaneTest : UserControl, IDockablePaneProvider
    {
        #region PrivateVariables
        // Stores the name of the class for debugging purposes
        private string _className = "DockableTestAddinManager";

        // Stores the name of the current method for error tracking
        private string _currentMethod;

        // Variables to hold references to Revit application and document objects
        private Autodesk.Revit.ApplicationServices.Application _appServices; // Revit application services
        private UIApplication _uiapp; // Revit application instance
        private UIDocument _uidoc; // Active document in Revit
        private Document _doc; // Current document in Revit
        #endregion

        // Constructor for the DockablePane
        public DockablePaneTest()
        {
            InitializeComponent(); // Initialize the user interface elements
        }

        /// <summary>
        /// Assigns the Revit application instance to local variables.
        /// </summary>
        /// <param name="UiApp">UIApplication instance from Revit</param>
        public void AssignOwner(UIApplication UiApp)
        {
            // Build a detailed error message in case of exceptions
            _currentMethod = "Class = " + _className + "\n" +
                             "Calling Method: " + MethodBase.GetCurrentMethod().DeclaringType.Name + "\n" +
                             "Current Method: " + "ClassTemplate";

            try
            {
                // Assign Revit application and document references
                _uiapp = UiApp;
                _appServices = _uiapp.Application;
                _uidoc = _uiapp.ActiveUIDocument;
                _doc = _uidoc.Document;
            }
            catch (Exception ex)
            {
                // Log the error details in a debug log for troubleshooting
                Debug.Write(_currentMethod + "\n" + "Error Details: " + ex.ToString());
                return;
            }
        }

        /// <summary>
        /// Example method triggered by a UI event.
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void AddText(object sender, RoutedEventArgs e)
        {
            // Debug log when this method is triggered
            Debug.WriteLine("HELLO"); //CHANGE THIS LINE FOR THE TEST <-------------------------------------------------------------------
        }

        /// <summary>
        /// Configures the DockablePane settings, including its position and tab behavior.
        /// </summary>
        /// <param name="data">DockablePaneProviderData for setup</param>
        public void SetupDockablePane(DockablePaneProviderData data)
        {
            // Assign this UserControl as the framework element for the DockablePane
            data.FrameworkElement = this as FrameworkElement;

            // Set the initial state of the DockablePane
            data.InitialState = new DockablePaneState
            {
                DockPosition = DockPosition.Left, // Dock the pane to the left
                TabBehind = DockablePanes.BuiltInDockablePanes.ProjectBrowser // Place it behind the Project Browser
            };
        }
    }
}
