using Autodesk.Revit.ApplicationServices; // Provides Revit-specific services
using Autodesk.Revit.DB.Events; // Library for Revit events
using Autodesk.Revit.UI; // Library for Revit's user interface
using System; // Base library for general functions like exceptions, etc.
using System.Diagnostics; // Used for debugging and logging information

namespace DockableTestAddinManager
{
    public class App : IExternalApplication
    {
        // Path to the external command assembly
        private string _addInPath = typeof(App).Assembly.Location;

        // Variables to store various information such as name, text, images, etc.
        string name = string.Empty;
        string text = string.Empty;
        string toolTip = string.Empty;
        string longDescription = string.Empty;
        string largeImageName = string.Empty;
        string imageName = string.Empty;
        string LargeImageName = string.Empty;
        string className = string.Empty;

        // Declaration of a static instance of DockablePaneTest
        private static DockablePaneTest _panoPane;

        // Store the Revit application to use it in the plugin
        private static UIApplication _uiApplication;

        // Declaration of static variables for the user-controlled application
        public static UIControlledApplication uiControlledApplication;

        public static ExternalCommandData CommandData { get; set; } // Store command information

        public Result OnStartup(UIControlledApplication application)
        {
            uiControlledApplication = application; // Save the application in a static variable

            // Create a custom tab in the Revit interface named "Dock Test"
            string tabName = "Dock Test";
            application.CreateRibbonTab(tabName); // Create a new tab
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "Dock TEST"); // Create a panel in this tab

            // Define parameters for the button to open the DockablePane
            name = "btnOpen"; // Technical name of the button
            text = "Launch TEST"; // Text displayed on the button
            toolTip = "Launch TEST"; // Tooltip for the button (displayed when hovering)
            className = CommandStart.GetPath(); // Path to the class containing the logic executed when the button is clicked

            // Create a button using the defined parameters
            PushButtonData btnOpenData = new PushButtonData(
                name, // Button name
                text, // Displayed text
                _addInPath, // Path to the assembly (application code)
                className) // Class invoked when the button is clicked
            {
                ToolTip = toolTip, // Define the tooltip
            };

            // Add the button to the ribbon panel
            PushButton btnOpen = ribbonPanel.AddItem(btnOpenData) as PushButton;

            // Register the event that will trigger once the Revit application is fully initialized
            uiControlledApplication.ControlledApplication.ApplicationInitialized += RegisterDockablePanes;

            return Result.Succeeded; // Indicate that the startup was successful
        }

        // Method to register DockablePanes (custom windows like the Properties panel)
        private void RegisterDockablePanes(object sender, ApplicationInitializedEventArgs e)
        {
            try
            {
                // Create a new instance of DockablePaneTest (the custom window)
                DockablePaneTest PanoPane = new DockablePaneTest();
                _panoPane = PanoPane; // Save the instance for later use

                // Create an instance of the Revit user interface
                UIApplication uiapp = new UIApplication(sender as Application);
                _uiApplication = uiapp; // Save the application for later use

                // Create a unique identifier for the DockablePane
                DockablePaneId dockablePaneId = new DockablePaneId(new Guid("{191A027E-C03F-425A-919F-BFB68D04C047}"));

                // Register the DockablePane in the Revit application so it can be used
                uiapp.RegisterDockablePane(dockablePaneId, "DockableTestAddinManager", _panoPane as IDockablePaneProvider);
            }
            catch (Exception ex)
            {
                // In case of error, log the error in a debug file for troubleshooting
                Debug.Write(ex);
            }
        }

        // Add this method to App.cs
        public static DockablePaneTest GetPanoDockablePane()
        {
            return _panoPane; // Return the instance of DockablePaneTest
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
