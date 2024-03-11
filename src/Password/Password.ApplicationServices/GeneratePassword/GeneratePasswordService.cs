namespace PasswordManager.Password.ApplicationServices.PasswordGenerator
{
    public class GeneratePasswordService : IGeneratePasswordService
    {
        // The character sets used to generate the password
        private const string _upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string _lowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string _digits = "0123456789";
        private const string _specialCharacters = "!@#$%^&*()_+-=[]{}|;:'\",.<>/?`~";

        private const string _allCharacters = _upperCaseLetters + _lowerCaseLetters + _digits + _specialCharacters;

        public string GeneratePassword(int passwordLength)
        {
            // Ensures that the password length is at least 8 characters
            // Which is the minimum length required by the NIST
            if (passwordLength < 8)
            {
                throw new ArgumentException("The password length must be at least 8 characters.");
            }

            // Ensures that the password length is at most 128 characters
            // Which is to prevent DoS attack
            if (passwordLength > 128)
            {
                throw new ArgumentException("The password length must be at most 128 characters.");
            }

            char[] password = new char[passwordLength];
            byte[] randomBytes = GenerateRandomBytes(passwordLength);

            // Fills the password with random characters from the allCharacters string
            for (int index = 0; index < passwordLength; index++)
            {
                password[index] = _allCharacters[randomBytes[index] % _allCharacters.Length];
            }

            randomBytes = GenerateRandomBytes(4);

            // Ensures that the password contains at least one character from each character set
            // placed at a random position.
            for (int index = 0; index < randomBytes.Length; index++)
            {
                switch (index)
                {
                    case 0:
                        password[randomBytes[index] % password.Length] = _upperCaseLetters[randomBytes[index] % _upperCaseLetters.Length];
                        break;
                    case 1:
                        password[randomBytes[index] % password.Length] = _lowerCaseLetters[randomBytes[index] % _lowerCaseLetters.Length];
                        break;
                    case 2:
                        password[randomBytes[index] % password.Length] = _digits[randomBytes[index] % _digits.Length];
                        break;
                    case 3:
                        password[randomBytes[index] % password.Length] = _specialCharacters[randomBytes[index] % _specialCharacters.Length];
                        break;
                }
            }

            return string.Concat(password);
        }

        private byte[] GenerateRandomBytes(int length)
        {
            byte[] randomBytes = new byte[length];
            RandomNumberGenerator.Create().GetBytes(randomBytes);
            return randomBytes;
        }
    }
}
