using System.ComponentModel.DataAnnotations;

namespace SharedData.Models;

public class SoundSetting
{
    [Key]
    public string UserId { get; set; }
    public float Master { get; set; }
    public float Bgm { get; set; }
    public float Effects { get; set; }
}