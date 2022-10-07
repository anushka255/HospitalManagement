using API.Entities;

namespace API.DTOs;

public class UserDTO
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string PhotoUrl { get; set; }
    public string Gender { get; set; }
    public UserTypes UserType { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastActive { get; set; }
}