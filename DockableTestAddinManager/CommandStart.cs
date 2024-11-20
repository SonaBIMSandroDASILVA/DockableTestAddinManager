using Autodesk.Revit.DB; // Library to work with the Revit database
using Autodesk.Revit.UI; // Library for Revit's user interface
using System;

namespace DockableTestAddinManager
{
    // Attribute to specify that this command is executed manually (not automatically at startup)
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]

    public class CommandStart : IExternalCommand
    {
        // Variables to store command data and document references
        private ExternalCommandData _commandData { get; set; } // Stores information about the external command
        private UIDocument _uidoc; // Represents the active document in Revit
        private UIApplication _uiapp; // Represents the Revit application instance
        private Document _doc; // Represents the current document in Revit
        private Autodesk.Revit.ApplicationServices.Application _appServices; // Represents Revit's application services

        // Main method executed when the command is launched
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Initialize Revit application and document references
            _commandData = commandData;
            _uiapp = _commandData.Application;
            _appServices = _commandData.Application.Application;
            _uidoc = _commandData.Application.ActiveUIDocument;
            _doc = _uidoc.Document;

            // Unique GUID for the DockablePane (custom user interface panel)
            DockablePaneId dockablePaneId = new DockablePaneId(new Guid("{191A027E-C03F-425A-919F-BFB68D04C047}"));

            // Retrieve the DockablePane using its GUID and the active Revit application
            DockablePane dockablePane = _uiapp.GetDockablePane(dockablePaneId);

            // Show the DockablePane (the user will be able to interact with it once it's visible)
            if (!dockablePane.IsShown())
            {
                dockablePane.Show();
            }

            // Retrieve the instance of the custom DockablePane
            DockablePaneTest panoPane = App.GetPanoDockablePane();

            // Return a successful result
            return Result.Succeeded;
        }

        // Static method to return the full class path with its namespace
        public static string GetPath()
        {
            return typeof(CommandStart).Namespace + "." + nameof(CommandStart);
        }
    }
}
