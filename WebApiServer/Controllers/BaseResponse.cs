namespace WebApiServer.Controllers;

public class BaseResponse
{
    public int code; //1: 성공 , 그 외: 실패
    public string message; //실패시 실패 이유
}