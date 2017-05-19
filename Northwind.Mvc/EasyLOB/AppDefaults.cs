namespace EasyLOB.Mvc
{
    public static partial class AppDefaults
    {
        #region Properties

        public static string AppName { get { return "Northwind"; } }

        public static string[] AppPath
        {
            get
            {
                return new string[] {
                    "Northwind",
                    "Northwind-Custom",
                    "Activity",
                    "Activity-Custom",
                    "AuditTrail",
                    "AuditTrail-Custom",
                    "Authentication",
                    "Dashboards",
                    "Identity",
                    "Identity-Custom",
                    "Reports",
                    "Tasks"
                };
            }
        }

        public static string AppVersion { get { return "1.0"; } }

        public static string Domain { get { return ""; } } // ???
        //public static string Domain { get { return "Northwind"; } }

        public static string TitleSeparator { get { return " : "; } }

        #endregion Properties

        #region Properties CSS Class

        // Group
        //     Label
        //     Editor
        //         Lookup
        //     Validator

        public static string CssClassGroup { get { return "form-group z-group"; } }

        public static string CssClassLabel { get { return "control-label z-label"; } }

        public static string CssClassLabelRequired { get { return "control-label z-labelRequired"; } }

        public static string CssClassEditor { get { return "form-control input-sm z-editor"; } }

        public static string CssClassEditorLookup { get { return "form-control input-sm z-editorLookup"; } }

        public static string CssClassForm { get { return "z-form"; } }
        //public static string CssClassForm { get { return "form-inline z-form"; } }

        public static string CssClassFormButtons { get { return "z-formButtons"; } }

        public static string CssClassLookup { get { return "input-group z-lookup"; } }

        public static string CssClassPanel { get { return "panel-body z-panel"; } }

        public static string CssClassValidator { get { return "z-validator"; } }

        #endregion Properties CSS Class

        #region Properties Syncfusion

        public static int SyncfusionRecordsByPage { get { return 10; } }

        public static int SyncfusionRecordsByLookup { get { return 5; } }

        public static int SyncfusionRecordsBySearch { get { return 100; } }

        public static int SyncfusionRecordsForFiltering { get { return 100; } }

        public static string SyncfusionTheme { get { return "bootstrap-theme"; } }

        #endregion Properties Syncfusion
    }
}