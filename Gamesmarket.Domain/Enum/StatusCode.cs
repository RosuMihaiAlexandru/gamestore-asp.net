namespace Gamesmarket.Domain.Enum
{
    public enum StatusCode
    {//Various HTTP application status codes
        OK = 200,

        InvalidData = 400,

        Unauthorized = 401,

        PermissionDenied = 403,

        UserNotFound = 404,

        RoleNotFound = 404,

        GameNotFound = 404,

        OrderNotFound = 404,

        RoleChangeFailed = 500,

        InternalServerError = 500

    }
}
