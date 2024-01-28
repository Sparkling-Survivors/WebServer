using System.ComponentModel.DataAnnotations;

namespace SharedData.Models;

public class PlayerSetting
{
    [Key]
    public string UserId { get; set; }
    public string PlayerSettingString { get; set; }
}