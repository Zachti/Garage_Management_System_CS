using System.Text;

namespace Garage {
    internal class Utilities {
        
        public static float[] GetFloatArray(int i_WheelsNumber) {
            string input = GetInputOrEmpty();
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
            string strFloatNum = GetInputOrEmpty();

            return (T)Convert.ChangeType(strFloatNum, typeof(T));

        }
        
        public static string GetNumberAsString(int i_MinNumOfDigits, int i_MaxNumOfDigits) 
        {
            string strNumber = GetInputOrEmpty();

            validateNumberInRange(strNumber.Length, i_MinNumOfDigits, i_MaxNumOfDigits);

            if (!strNumber.All(char.IsDigit))
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            return strNumber;
        }
    
        public static string GetAlphabeticString() {
            string input = GetInputOrEmpty();

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

            string input = GetInputOrEmpty();

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
        
        public static int EnumMenuToIntChoiceWithValidation<T>(string i_Message, int i_Min, int i_Max) where T : Enum {
            EnumMenuToEnumChoice<T>(i_Message);
            int choice = GetSingleDigit();
            validateNumberInRange(choice, i_Min, i_Max);
            return choice;
        }
    
        private static void validateNumberInRange(int i_Number, int i_Min, int i_Max) {
            if (i_Number < i_Min || i_Number > i_Max) {
                throw new ValueOutOfRangeException(new Exception("Invalid input, please try again"), i_Min, i_Max);
            }
        }
    
        public static string GetInputOrEmpty() => Console.ReadLine() ?? string.Empty;
    }
}   