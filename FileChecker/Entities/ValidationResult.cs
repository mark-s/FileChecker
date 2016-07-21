namespace FileChecker.Entities
{
    public class ValidationResult
    {

        public bool IsValid { get; private set; }
        public string Message { get; private set; }

        public ValidationResult(bool isValid, string message)
        {
            IsValid = isValid;
            Message = message;
        }


    }
}