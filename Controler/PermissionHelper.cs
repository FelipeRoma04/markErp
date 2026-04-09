using System;
using System.Collections.Generic;
using System.Linq;

namespace Proyecto.Controler
{
    /// <summary>
    /// Granular role-based permission system
    /// </summary>
    public static class PermissionHelper
    {
        public enum Feature
        {
            Departments, Employees, Contracts, Attendance, Payroll,
            Clients, Sales, Invoicing, Products, Quotes,
            Accounting, Assets, Audit, Reports, Admin
        }

        public enum Action
        {
            View, Create, Edit, Delete, Export
        }

        private static Dictionary<string, HashSet<Feature>> RoleFeatures = new Dictionary<string, HashSet<Feature>>()
        {
            { "Admin", new HashSet<Feature>(Enum.GetValues(typeof(Feature)).Cast<Feature>()) },
            { "Editor", new HashSet<Feature>(Enum.GetValues(typeof(Feature)).Cast<Feature>()) },
            { "Lectura", new HashSet<Feature>(Enum.GetValues(typeof(Feature)).Cast<Feature>()) },
            { "HR", new HashSet<Feature> { Feature.Employees, Feature.Departments, Feature.Contracts, Feature.Attendance, Feature.Payroll } },
            { "Finance", new HashSet<Feature> { Feature.Payroll, Feature.Invoicing, Feature.Accounting, Feature.Reports } },
            { "Sales", new HashSet<Feature> { Feature.Clients, Feature.Sales, Feature.Invoicing, Feature.Quotes, Feature.Products } },
            { "Usuario", new HashSet<Feature> { Feature.Attendance, Feature.Quotes } }
        };

        private static Dictionary<string, HashSet<Action>> RoleActions = new Dictionary<string, HashSet<Action>>()
        {
            { "Admin", new HashSet<Action>(Enum.GetValues(typeof(Action)).Cast<Action>()) },
            { "Editor", new HashSet<Action> { Action.View, Action.Create, Action.Edit, Action.Export } },
            { "Lectura", new HashSet<Action> { Action.View } },
            { "HR", new HashSet<Action> { Action.View, Action.Create, Action.Edit, Action.Delete, Action.Export } },
            { "Finance", new HashSet<Action> { Action.View, Action.Create, Action.Edit, Action.Export } },
            { "Sales", new HashSet<Action> { Action.View, Action.Create, Action.Edit, Action.Export } },
            { "Usuario", new HashSet<Action> { Action.View } }
        };

        /// <summary>
        /// Check if current user's role has permission for feature
        /// </summary>
        public static bool HasFeatureAccess(Feature feature)
        {
            var role = UserSession.Role ?? "Usuario";
            return RoleFeatures.ContainsKey(role) && RoleFeatures[role].Contains(feature);
        }

        /// <summary>
        /// Check if current user's role can perform action
        /// </summary>
        public static bool CanPerformAction(Action action)
        {
            var role = UserSession.Role ?? "Usuario";
            return RoleActions.ContainsKey(role) && RoleActions[role].Contains(action);
        }

        /// <summary>
        /// Check both feature and action access
        /// </summary>
        public static bool HasPermission(Feature feature, Action action)
        {
            return HasFeatureAccess(feature) && CanPerformAction(action);
        }

        /// <summary>
        /// Deny access message
        /// </summary>
        public static void DenyAccess(Feature feature)
        {
            System.Windows.Forms.MessageBox.Show(
                "No tienes permiso para acceder a " + feature + ".\n\nContacta al administrador.",
                "Acceso Denegado",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning
            );
        }

        // Task 26: Role-based UI control methods
        /// <summary>
        /// Task 26: Apply UI permissions to delete buttons based on role
        /// Only Admin and Editor can see delete functionality
        /// </summary>
        public static void ApplyDeleteButtonPermissions(System.Windows.Forms.Button btnDelete)
        {
            var role = UserSession.Role ?? "Usuario";
            bool canDelete = RoleActions.ContainsKey(role) && RoleActions[role].Contains(Action.Delete);
            
            if (btnDelete != null)
            {
                btnDelete.Visible = canDelete;
                btnDelete.Enabled = canDelete;
            }
        }

        /// <summary>
        /// Task 26: Apply UI permissions to edit buttons
        /// Lectura role can only view, not edit
        /// </summary>
        public static void ApplyEditButtonPermissions(System.Windows.Forms.Button btnEdit)
        {
            var role = UserSession.Role ?? "Usuario";
            bool canEdit = RoleActions.ContainsKey(role) && RoleActions[role].Contains(Action.Edit);
            
            if (btnEdit != null)
            {
                btnEdit.Visible = canEdit;
                btnEdit.Enabled = canEdit;
            }
        }

        /// <summary>
        /// Task 26: Apply UI permissions to create buttons
        /// Lectura role cannot create
        /// </summary>
        public static void ApplyCreateButtonPermissions(System.Windows.Forms.Button btnCreate)
        {
            var role = UserSession.Role ?? "Usuario";
            bool canCreate = RoleActions.ContainsKey(role) && RoleActions[role].Contains(Action.Create);
            
            if (btnCreate != null)
            {
                btnCreate.Visible = canCreate;
                btnCreate.Enabled = canCreate;
            }
        }

        /// <summary>
        /// Task 26: Apply UI permissions to export buttons
        /// Only Editor, HR, Finance, Sales can export
        /// </summary>
        public static void ApplyExportButtonPermissions(System.Windows.Forms.Button btnExport)
        {
            var role = UserSession.Role ?? "Usuario";
            bool canExport = RoleActions.ContainsKey(role) && RoleActions[role].Contains(Action.Export);
            
            if (btnExport != null)
            {
                btnExport.Visible = canExport;
                btnExport.Enabled = canExport;
            }
        }

        /// <summary>
        /// Task 26: Apply UI permissions to entire form based on feature
        /// Lectura role sees read-only versions of forms
        /// </summary>
        public static void ApplyFormPermissions(System.Windows.Forms.Form form, Feature feature)
        {
            var role = UserSession.Role ?? "Usuario";
            bool hasFeatureAccess = RoleFeatures.ContainsKey(role) && RoleFeatures[role].Contains(feature);
            bool canEdit = RoleActions.ContainsKey(role) && RoleActions[role].Contains(Action.Edit);
            bool canDelete = RoleActions.ContainsKey(role) && RoleActions[role].Contains(Action.Delete);

            if (!hasFeatureAccess)
            {
                form.Enabled = false;
                return;
            }

            // Disable all buttons if only View permission
            if (!canEdit && !canDelete)
            {
                foreach (System.Windows.Forms.Control control in form.Controls)
                {
                    if (control is System.Windows.Forms.Button && control.Name != "btnClose")
                    {
                        control.Enabled = false;
                    }
                }
            }

            // Set controls to ReadOnly if Lectura role
            if (role == "Lectura")
            {
                foreach (System.Windows.Forms.Control control in GetAllControls(form))
                {
                    if (control is System.Windows.Forms.TextBox)
                        ((System.Windows.Forms.TextBox)control).ReadOnly = true;
                    else if (control is System.Windows.Forms.ComboBox)
                        ((System.Windows.Forms.ComboBox)control).Enabled = false;
                }
            }
        }

        /// <summary>
        /// Task 26: Recursively get all controls from a form (including nested containers)
        /// </summary>
        private static List<System.Windows.Forms.Control> GetAllControls(System.Windows.Forms.Control parent)
        {
            var controls = new List<System.Windows.Forms.Control>();
            foreach (System.Windows.Forms.Control control in parent.Controls)
            {
                controls.Add(control);
                controls.AddRange(GetAllControls(control));
            }
            return controls;
        }

        /// <summary>
        /// Task 26: Check if user can perform feature action
        /// Returns enabled status for UI controls
        /// </summary>
        public static bool IsFeatureEnabled(Feature feature)
        {
            var role = UserSession.Role ?? "Usuario";
            return RoleFeatures.ContainsKey(role) && RoleFeatures[role].Contains(feature);
        }
    }
}
