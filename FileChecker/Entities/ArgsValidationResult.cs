namespace FileChecker.Entities
{
    public class ArgsValidationResult
    {

        public bool IsValid { get; }
        public string Message { get; }

        public ArgsValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }


    }
}