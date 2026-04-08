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
    }
}
