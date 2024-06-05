using System.Text;

namespace Garage {
    internal class Utilities {
        
        public static float[] GetFloatArray(int i_WheelsNumber) {
            string input = Console.ReadLine() ?? string.Empty;
            string[] splitInput = input.Split(',');

            if (splitInput.Length != i_WheelsNumber)
            {
                throw new FormatException($"Input must contain state for {i_WheelsNumber} wheels.");
            }

            if (!splitInput.All(str => float.TryParse(str, out _)))
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            return splitInput.Select(float.Parse).ToArray();
        }
        
        public static T GetNumber<T>() where T : IConvertible
        {
            string strFloatNum = Console.ReadLine() ?? string.Empty;

            return (T)Convert.ChangeType(strFloatNum, typeof(T));

        }
        
        public static string GetNumberAsString(int i_MinNumOfDigits, int i_MaxNumOfDigits) 
        {
            string strNumber = Console.ReadLine() ?? string.Empty;

            bool isUserQuit = strNumber == "Q";

            int strLength = strNumber.Length;

            if (!isUserQuit &&(strLength < i_MinNumOfDigits || strLength > i_MaxNumOfDigits))
            {
                throw new ValueOutOfRangeException(
                    new Exception("Invalid input, please try again"),
                    i_MinNumOfDigits,
                    i_MaxNumOfDigits
                );
            }

            if (!strNumber.All(char.IsDigit) && !isUserQuit)
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            return strNumber;
        }
    
        public static string GetAlphabeticString() {
            string input = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(input) || !input.All(ch => char.IsLetter(ch) || ch == ' ')) {
                throw new FormatException("Input must contain only alphabetic characters and spaces.");
            }

            if (!input.Any(char.IsLetter)) {
                throw new FormatException("Input must contain at least one alphabetic character.");
            }

            return input;
        }
    
        public static int GetSingleDigit() 
        {

            string input = Console.ReadLine() ?? string.Empty;

            if (input?.Length != 1)
            {
                throw new FormatException("Input must be a single digit.");
            }

            if (!char.IsDigit(input[0]))
            {
                throw new FormatException("Input must be a single digit.");
            }

            return int.Parse(input);
        }
    
        public static void EnumToMenu<TEnum>(string? i_OpenMessage) where TEnum : Enum
        {
            StringBuilder menu = new StringBuilder(i_OpenMessage ?? "");
            int index = 1;
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                menu.AppendLine($"{index}. {value}");
                index++;
            }
            Console.WriteLine(menu.ToString());
        }
    
        public static T EnumMenuToEnumChoice<T>(string i_Message) where T : Enum {
            EnumToMenu<T>(i_Message);
            return (T)(object)GetSingleDigit();
        }
    }
}   