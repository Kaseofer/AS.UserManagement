// AS.UserManagement.Application/Helpers/TemporaryPasswordHelper.cs
using System.Security.Cryptography;
using System.Text;

namespace AS.UserManagement.Application.Helpers
{
    public static class TemporaryPasswordHelper
    {
        private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string Numbers = "0123456789";
        private const string SpecialCharacters = "@#$%&*+-=";
        private const string EasyToReadChars = "ABCDEFGHKLMNPQRSTUVWXYZ23456789"; // Sin confusos: O,0,I,1

        /// <summary>
        /// Genera un password temporal simple y fácil de leer
        /// </summary>
        /// <param name="length">Longitud del password (default: 8)</param>
        /// <returns>Password temporal</returns>
        public static string GenerateSimple(int length = 8)
        {
            return GenerateFromCharset(EasyToReadChars, length);
        }

        /// <summary>
        /// Genera un password temporal complejo con mayúsculas, minúsculas, números y símbolos
        /// </summary>
        /// <param name="length">Longitud del password (default: 12)</param>
        /// <returns>Password temporal complejo</returns>
        public static string GenerateComplex(int length = 12)
        {
            var charset = UppercaseLetters + LowercaseLetters + Numbers + SpecialCharacters;
            var password = new StringBuilder();

            // Garantizar al menos un carácter de cada tipo
            password.Append(GetRandomChar(UppercaseLetters));
            password.Append(GetRandomChar(LowercaseLetters));
            password.Append(GetRandomChar(Numbers));
            password.Append(GetRandomChar(SpecialCharacters));

            // Llenar el resto aleatoriamente
            for (int i = 4; i < length; i++)
            {
                password.Append(GetRandomChar(charset));
            }

            // Mezclar caracteres para que no estén en orden predecible
            return ShuffleString(password.ToString());
        }

        /// <summary>
        /// Genera un password temporal solo con números y letras (alfanumérico)
        /// </summary>
        /// <param name="length">Longitud del password (default: 10)</param>
        /// <returns>Password alfanumérico</returns>
        public static string GenerateAlphaNumeric(int length = 10)
        {
            var charset = UppercaseLetters + LowercaseLetters + Numbers;
            return GenerateFromCharset(charset, length);
        }

        /// <summary>
        /// Genera un password temporal basado en patrón memorable
        /// Formato: Palabra + Números + Símbolo (ej: "Hospital2024!")
        /// </summary>
        /// <returns>Password con patrón memorable</returns>
        public static string GeneratePatternBased()
        {
            var words = new[] { "Hospital", "Salud", "Medico", "Clinica", "Centro", "Sistema" };
            var year = DateTime.Now.Year;
            var symbols = new[] { "!", "@", "#", "$", "*" };

            var randomWord = words[RandomNumberGenerator.GetInt32(words.Length)];
            var randomSymbol = symbols[RandomNumberGenerator.GetInt32(symbols.Length)];

            return $"{randomWord}{year}{randomSymbol}";
        }

        /// <summary>
        /// Genera password temporal usando GUID (muy único pero menos memorable)
        /// </summary>
        /// <param name="length">Longitud del password (default: 8)</param>
        /// <returns>Password basado en GUID</returns>
        public static string GenerateFromGuid(int length = 8)
        {
            var guid = Guid.NewGuid().ToString("N").ToUpper();
            return guid.Length > length ? guid[..length] : guid;
        }

        /// <summary>
        /// Genera un PIN numérico temporal
        /// </summary>
        /// <param name="length">Longitud del PIN (default: 6)</param>
        /// <returns>PIN numérico</returns>
        public static string GeneratePIN(int length = 6)
        {
            return GenerateFromCharset(Numbers, length);
        }

        /// <summary>
        /// Genera password con configuración personalizada
        /// </summary>
        public static string GenerateCustom(PasswordConfig config)
        {
            var charset = "";
            var guaranteedChars = new List<char>();

            if (config.IncludeUppercase)
            {
                charset += UppercaseLetters;
                guaranteedChars.Add(GetRandomChar(UppercaseLetters));
            }

            if (config.IncludeLowercase)
            {
                charset += LowercaseLetters;
                guaranteedChars.Add(GetRandomChar(LowercaseLetters));
            }

            if (config.IncludeNumbers)
            {
                charset += Numbers;
                guaranteedChars.Add(GetRandomChar(Numbers));
            }

            if (config.IncludeSpecialChars)
            {
                charset += SpecialCharacters;
                guaranteedChars.Add(GetRandomChar(SpecialCharacters));
            }

            if (string.IsNullOrEmpty(charset))
                throw new ArgumentException("Debe incluir al menos un tipo de carácter");

            var password = new StringBuilder();

            // Agregar caracteres garantizados
            foreach (var c in guaranteedChars)
                password.Append(c);

            // Llenar el resto
            for (int i = guaranteedChars.Count; i < config.Length; i++)
            {
                password.Append(GetRandomChar(charset));
            }

            return ShuffleString(password.ToString());
        }

        #region Private Methods

        private static string GenerateFromCharset(string charset, int length)
        {
            if (length <= 0) throw new ArgumentException("La longitud debe ser mayor a 0");
            if (string.IsNullOrEmpty(charset)) throw new ArgumentException("El charset no puede estar vacío");

            var result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                result.Append(GetRandomChar(charset));
            }

            return result.ToString();
        }

        private static char GetRandomChar(string charset)
        {
            var randomIndex = RandomNumberGenerator.GetInt32(charset.Length);
            return charset[randomIndex];
        }

        private static string ShuffleString(string input)
        {
            var chars = input.ToCharArray();

            for (int i = chars.Length - 1; i > 0; i--)
            {
                var j = RandomNumberGenerator.GetInt32(i + 1);
                (chars[i], chars[j]) = (chars[j], chars[i]);
            }

            return new string(chars);
        }

        #endregion
    }

    /// <summary>
    /// Configuración para generación de password personalizado
    /// </summary>
    public class PasswordConfig
    {
        public int Length { get; set; } = 10;
        public bool IncludeUppercase { get; set; } = true;
        public bool IncludeLowercase { get; set; } = true;
        public bool IncludeNumbers { get; set; } = true;
        public bool IncludeSpecialChars { get; set; } = false;
    }
}