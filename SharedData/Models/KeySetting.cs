using System.ComponentModel.DataAnnotations;

namespace SharedData.Models;

public class KeySetting
{
    [Key]
    public string UserId { get; set; }
    public string KeySettingString { get; set; }
}