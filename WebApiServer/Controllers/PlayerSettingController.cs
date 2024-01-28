using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using WebApiServer.Data;

namespace WebApiServer.Controllers;

public class GetPlayerSettingByIdDTO: BaseResponse
{
    public string PlayerSetting;
}

[Route("[controller]")]
[ApiController]
public class PlayerSettingController
{
    private ApplicationDbContext _context; 
    
    public PlayerSettingController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    #region Get

    [HttpGet("{id}")]  //이 부분은 요청할때  서버주소/PlayerSetting/1이런식으로 요청해야함
    public string GetPlayerSetting(string id)
    {
        var findPlayerSetting = _context.PlayerSettings.Where(item => item.UserId == id).FirstOrDefault();

        if (findPlayerSetting == null)
        {
            BaseResponse response = new BaseResponse(){code = 0, message = "플레이어 설정이 없습니다."};
            return LitJson.JsonMapper.ToJson(response);
        }
        else
        {
            GetPlayerSettingByIdDTO response = new GetPlayerSettingByIdDTO(){code = 1, message = "플레이어 조회 성공.", PlayerSetting = findPlayerSetting.PlayerSettingString};
            return LitJson.JsonMapper.ToJson(response);
        }
    }

    #endregion
    
    #region Put

    [HttpPut]
    public string UpdatePlayerSetting([FromBody] PlayerSetting playerSetting)
    {
        var findPlayerSetting = _context.PlayerSettings.Where(item => item.UserId == playerSetting.UserId).FirstOrDefault();

        if (findPlayerSetting == null) //없으면 새로 등록
        {
            _context.PlayerSettings.Add(playerSetting);
            _context.SaveChanges();
            
            BaseResponse response = new BaseResponse(){code = 1, message = "플레이어 설정 새로 등록 성공"};
            return LitJson.JsonMapper.ToJson(response);
        }
        else
        {
            findPlayerSetting.UserId = playerSetting.UserId;
            findPlayerSetting.PlayerSettingString= playerSetting.PlayerSettingString;
            _context.SaveChanges();
            
            BaseResponse response = new BaseResponse(){code = 1, message = "플레이어 설정 수정 성공"};
            return LitJson.JsonMapper.ToJson(response);
        }
    }

    #endregion
}