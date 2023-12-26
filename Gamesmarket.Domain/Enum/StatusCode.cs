namespace Gamesmarket.Domain.Enum
{
    public enum StatusCode
    {//Various HTTP application status codes
        UserNotFound = 0,
        GameNotFound = 10,
        OK = 200,
        InvalidData = 400,
        InternalServerError = 500
    }
}
