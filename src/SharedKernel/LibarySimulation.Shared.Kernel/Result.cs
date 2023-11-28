namespace LibrarySimulation.Shared.Kernel
{
    public class Result<T>
    {
        private Result() { }

        public T Response { get; set; }

        public bool Succeeded { get; set; }

        public Exception InnerException { get; set; }

        public static Result<T> Success(T response)
        {
            return new Result<T>
            {
                Response = response,
                Succeeded = true
            };
        }

        public static Result<T> Failure(Exception ex)
        {
            return new Result<T>
            {
                Succeeded = false,
                InnerException = ex
            };
        }
    }

    public class Result
    {
        private Result() { }

        public bool Succeeded { get; set; }

        public Exception InnerException { get; set; }

        public static Result Success()
        {
            return new Result
            {
                Succeeded = true
            };
        }

        public static Result Failure(Exception ex)
        {
            return new Result
            {
                Succeeded = false,
                InnerException = ex
            };
        }
    }
}