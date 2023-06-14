using System.Text.RegularExpressions;

namespace FIAP_TDD.Helper
{
    public static class VerificadorDeSenhaForte
    {
        public static bool VerificarSenhaForte(string password)
        {
            // Verificar comprimento mínimo
            if (password.Length < 8)
                return false;

            // Verificar se contém letras maiúsculas, minúsculas, números e caracteres especiais
            Regex uppercaseRegex = new Regex(@"[A-Z]");
            Regex lowercaseRegex = new Regex(@"[a-z]");
            Regex digitRegex = new Regex(@"\d");
            Regex specialCharRegex = new Regex(@"\W");

            bool hasUppercase = uppercaseRegex.IsMatch(password);
            bool hasLowercase = lowercaseRegex.IsMatch(password);
            bool hasDigit = digitRegex.IsMatch(password);
            bool hasSpecialChar = specialCharRegex.IsMatch(password);

            if (!(hasUppercase && hasLowercase && hasDigit && hasSpecialChar))
                return false;

            // A senha atende a todos os critérios
            return true;
        }
    }
}
