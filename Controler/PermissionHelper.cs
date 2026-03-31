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

        private static Dictionary<string, HashSet<Feature>> RoleFeatures = new()
        {
            { "Admin", new(Enum.GetValues(typeof(Feature)).Cast<Feature>()) },
            { "HR", new() { Feature.Employees, Feature.Departments, Feature.Contracts, Feature.Attendance, Feature.Payroll } },
            { "Finance", new() { Feature.Payroll, Feature.Invoicing, Feature.Accounting, Feature.Reports } },
            { "Sales", new() { Feature.Clients, Feature.Sales, Feature.Invoicing, Feature.Quotes, Feature.Products } },
            { "Usuario", new() { Feature.Attendance, Feature.Quotes } }
        };

        private static Dictionary<string, HashSet<Action>> RoleActions = new()
        {
            { "Admin", new(Enum.GetValues(typeof(Action)).Cast<Action>()) },
            { "HR", new() { Action.View, Action.Create, Action.Edit, Action.Delete, Action.Export } },
            { "Finance", new() { Action.View, Action.Create, Action.Edit, Action.Export } },
            { "Sales", new() { Action.View, Action.Create, Action.Edit, Action.Export } },
            { "Usuario", new() { Action.View } }
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
                $"No tienes permiso para acceder a {feature}.\n\nContacta al administrador.",
                "Acceso Denegado",
                System.Windows.Forms.MessageBoxButtons.OK,
                System.Windows.Forms.MessageBoxIcon.Warning
            );
        }
    }
}
