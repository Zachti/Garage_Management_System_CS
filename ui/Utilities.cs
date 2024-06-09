using System.Text;

namespace Garage {
    internal class Utilities {
        
        public static float[] WheelsPressureToArray(int i_ArrayLength) 
        {
            string input = GetInputOrEmpty();
            string[] splitInput = input.Split(',');

            if (splitInput.Length != i_ArrayLength && splitInput.Length != 1)
            {
                throw new FormatException($"Input must contain state for {i_ArrayLength} wheels.");
            }

            if (!splitInput.All(str => float.TryParse(str, out _)))
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            float[] wheelsData = splitInput.Select(float.Parse).ToArray();
            return wheelsData.Length == 1 ? Enumerable.Repeat(wheelsData[0], i_ArrayLength).ToArray() : wheelsData;
        }
        
        public static T GetNumber<T>() where T : IConvertible
        {
            string strFloatNum = GetInputOrEmpty();

            return (T)Convert.ChangeType(strFloatNum, typeof(T));

        }
        
        public static string GetNumberAsString(int i_MinNumOfDigits, int i_MaxNumOfDigits, string? i_Message = null) 
        {
            string strNumber = GetInputOrEmpty();

            ValidateNumberInRange(strNumber.Length, i_MinNumOfDigits, i_MaxNumOfDigits, i_Message);

            if (!strNumber.All(char.IsDigit))
            {
                throw new FormatException("Input must contain only numeric digits.");
            }

            return strNumber;
        }
    
        public static string GetAlphabeticString() 
        {
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
            StringBuilder menu = new StringBuilder(i_OpenMessage ?? "").AppendLine();
            int index = 1;
            foreach (TEnum value in Enum.GetValues(typeof(TEnum)))
            {
                menu.AppendLine($"{index}. {value.ToString().Replace("_", " ")}");
                index++;
            }
            Console.WriteLine(menu.ToString());
        }
    
        public static T EnumMenuToEnumChoice<T>(string i_Message) where T : Enum 
        {
            EnumToMenu<T>(i_Message);
            return (T)(object)GetSingleDigit();
        }
        
        public static int EnumMenuToIntChoiceWithValidation<T>(string i_Message, int i_Min, int i_Max) where T : Enum 
        {
            EnumToMenu<T>(i_Message);
            int choice = GetSingleDigit();
            ValidateNumberInRange(choice, i_Min, i_Max);
            return choice;
        }
    
        public static void ValidateNumberInRange(float i_Number, float i_Min, float i_Max, string? i_Message = null) 
        {
            if (i_Number < i_Min || i_Number > i_Max) 
            {
                throw new ValueOutOfRangeException(i_Number, i_Min, i_Max, i_Message);
            }
        }
    
        public static string GetInputOrEmpty() => Console.ReadLine() ?? string.Empty;
    }
}   