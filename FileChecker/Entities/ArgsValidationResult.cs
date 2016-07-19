namespace FileChecker.Entities
{
    // Holds the results of the validation of the values passed into Main()
    public class ArgsValidationResult
    {

        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        public ArgsValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }


    }
}