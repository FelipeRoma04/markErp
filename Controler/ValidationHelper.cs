using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Proyecto.Controler
{
    public static class ValidationHelper
    {
        public static bool IsNotEmpty(string input)
        {
            return !string.IsNullOrWhiteSpace(input);
        }

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
                return regex.IsMatch(email);
            }
            catch { return false; }
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone, @"^\+?[\d\s\-\(\)]{7,}$");
        }

        public static bool IsValidDecimal(string value, out decimal result)
        {
            return decimal.TryParse(value, out result) && result >= 0;
        }

        public static bool IsValidInteger(string value, out int result)
        {
            return int.TryParse(value, out result) && result >= 0;
        }

        public static bool ShowValidationError(string message)
        {
            MessageBox.Show(message, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        public static void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
