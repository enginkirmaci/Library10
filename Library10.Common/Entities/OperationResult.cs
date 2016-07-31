using Library10.Common.Enums;

namespace Library10.Common.Entities
{
    public class OperationResult
    {
        public OperationResultType Type { get; set; }
        public object ReturnObject { get; set; }

        static public OperationResult None = new OperationResult()
        {
            Type = OperationResultType.None
        };

        static public OperationResult Success = new OperationResult()
        {
            Type = OperationResultType.Success
        };

        static public OperationResult Warning = new OperationResult()
        {
            Type = OperationResultType.Warning
        };

        static public OperationResult Error = new OperationResult()
        {
            Type = OperationResultType.Error
        };

        static public OperationResult FatalError = new OperationResult()
        {
            Type = OperationResultType.FatalError
        };

        static public OperationResult SomeError = new OperationResult()
        {
            Type = OperationResultType.SomeError
        };

        public int GetErrorCode()
        {
            if (ReturnObject != null && ReturnObject is int)
                return (int)ReturnObject;

            return -1;
        }

        public void IsSomeError(OperationResultType newResult)
        {
            switch (Type)
            {
                case OperationResultType.Success:
                    if (newResult == OperationResultType.Error)
                        Type = OperationResultType.SomeError;
                    break;

                case OperationResultType.Error:
                    if (newResult == OperationResultType.Success)
                        Type = OperationResultType.SomeError;
                    break;

                case OperationResultType.None:
                    Type = newResult;
                    break;
            }
        }
    }
}