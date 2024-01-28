using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using WebApiServer.Data;
namespace WebApiServer.Controllers;

public class GetKeySettingByIdDTO : BaseResponse
{
    public string KeySettingString;
}

[Route("[controller]")]
[ApiController]
public class KeySettingController
{
    private ApplicationDbContext _context; 
    
    public KeySettingController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    #region Get

    [HttpGet("{id}")]  //이 부분은 요청할때  서버주소/KeySetting/1이런식으로 요청해야함
    public string GetKeySetting(string id)
    {
        var findKeySetting = _context.KeySettings.Where(item => item.UserId == id).FirstOrDefault();

        if (findKeySetting == null)
        {
            BaseResponse response = new BaseResponse(){code = 0, message = "키 설정이 없습니다."};
            return LitJson.JsonMapper.ToJson(response);
        }
        else
        {
            GetKeySettingByIdDTO response = new GetKeySettingByIdDTO(){code = 1, message = "키 조회 성공.", KeySettingString = findKeySetting.KeySettingString};
            return LitJson.JsonMapper.ToJson(response);
        }
    }

    #endregion
    
    #region Put

    [HttpPut]
    public string UpdateKeySetting([FromBody] KeySetting keySetting)
    {
        var findKeySetting = _context.KeySettings.Where(item => item.UserId == keySetting.UserId).FirstOrDefault();

        if (findKeySetting == null) //없으면 새로 등록
        {
            _context.KeySettings.Add(keySetting);
            _context.SaveChanges();
            
            BaseResponse response = new BaseResponse(){code = 1, message = "키 설정 새로 등록 성공"};
            return LitJson.JsonMapper.ToJson(response);
        }
        else
        {
            findKeySetting.UserId = keySetting.UserId;
            findKeySetting.KeySettingString= keySetting.KeySettingString;
            _context.SaveChanges();
            
            BaseResponse response = new BaseResponse(){code = 1, message = "키 설정 업데이트 성공"};
            return LitJson.JsonMapper.ToJson(response);
        }
    }

    #endregion
}