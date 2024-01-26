using Microsoft.AspNetCore.Mvc;
using SharedData.Models;
using WebApiServer.Data;

namespace WebApiServer.Controllers;

public class GetSoundSettingByIdDTO : BaseResponse
{
    public SoundSetting soundSetting;
}

[Route("[controller]")]
[ApiController]
public class SoundSettingController
{
    private ApplicationDbContext _context;

    public SoundSettingController(ApplicationDbContext context)
    {
        _context = context;
    }

    #region Get

    [HttpGet("{id}")]  //이 부분은 요청할때  서버주소/SoundSetting/1이런식으로 요청해야함
    public object GetSoundSetting(string id)
    {
        var findSoundSetting = _context.SoundSettings.Where(item => item.UserId == id).FirstOrDefault();

        if (findSoundSetting == null)
        {
            BaseResponse response = new BaseResponse(){code = 0, message = "사운드 설정이 없습니다."};
            return response;
        }
        else
        {
            GetSoundSettingByIdDTO response = new GetSoundSettingByIdDTO(){code = 1, message = "사운드 조회 성공.", soundSetting = findSoundSetting};
            //return response;
            return findSoundSetting;
        }
    }

    #endregion
    
    #region Put

    [HttpPut]
    public BaseResponse UpdateSoundSetting([FromBody] SoundSetting soundSetting)
    {
        var findSoundSetting = _context.SoundSettings.Where(item => item.UserId == soundSetting.UserId).FirstOrDefault();

        if (findSoundSetting == null) //없으면 새로 등록
        {
            _context.SoundSettings.Add(soundSetting);
            _context.SaveChanges();
            
            BaseResponse response = new BaseResponse(){code = 1, message = "사운드 설정 새로 등록 성공"};
            return response;
        }
        else
        {
            findSoundSetting.UserId = soundSetting.UserId;
            findSoundSetting.Master = soundSetting.Master;
            findSoundSetting.Bgm = soundSetting.Bgm;
            findSoundSetting.Effects = soundSetting.Effects;
            _context.SaveChanges();
            
            BaseResponse response = new BaseResponse(){code = 1, message = "사운드 설정 업데이트 성공"};
            return response;
        }
    }

    #endregion
    
}