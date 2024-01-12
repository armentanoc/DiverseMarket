using SalesApp.DomainLayer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesApp.UI.Util
{
    internal static class ValidationUtils
    {
        internal static bool IsInputAValidName(string input, string? hintText, bool? allowSpaces)
        {
            bool isValid = input.Length > 1;

            if (hintText != null) isValid = isValid && !input.Equals(hintText);

            if (allowSpaces == false) isValid = isValid && !input.Contains(" ");

            return isValid;
        }

        internal static bool IsEmailValid(string input)
        {
            string regex = @"^[a-zA-Z0-9]+[a-zA-Z0-9_.-]*@[a-zA-Z]+\.[a-zA-Z]+(\.[a-zA-Z]+)?$";

            return Regex.IsMatch(input, regex, RegexOptions.IgnoreCase);
        }
        internal static bool IsUsernameValid(string input)
        {
            return AuthenticationService.IsUsernameAvailable(input) && IsInputAValidName(input, "Username", false);
        }

        internal static bool IsPasswordValid(string input)
        {
            return Regex.IsMatch(input, @"(?=.*[a-zA-Z])(?=.*[0-9])") && !input.Contains(" ") && input.Length > 7;
        }

        internal static bool IsCPFValid(string input)
        {
            input = new string(input.Where(char.IsDigit).ToArray());

            if (input.Length != 11)
                return false;

            if (input.All(c => c == input[0]))
                return false;

            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (input[i] - '0') * (10 - i);
            int remainder = (sum * 10) % 11;
            if (remainder == 10) remainder = 0;
            if (input[9] != remainder + '0')
                return false;

            sum = 0;
            for (int i = 0; i < 10; i++)
                sum += (input[i] - '0') * (11 - i);
            remainder = (sum * 10) % 11;

            if (remainder == 10) remainder = 0;

            if (input[10] != remainder + '0')
                return false;

            return true;
        }
    }
}
