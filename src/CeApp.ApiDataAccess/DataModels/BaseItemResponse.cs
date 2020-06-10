namespace ApiDataAccess.DataModels
{
    internal abstract class BaseItemResponse<T> : BaseResponse
    {
        public T Content { get; set; }
    }
}
