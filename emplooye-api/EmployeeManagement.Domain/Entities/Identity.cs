

using System.Collections;

namespace EmployeeManagement.Domain.Entities;

public class Identity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("Username")]
    public string Username { get; set; }
    
    [BsonElement("Password")]
    public string Password { get; set; }
    
    [BsonElement("Roles")]
    public string[] HasRoles { get; set; }
    
    [BsonElement("Email")]
    public string email { get; set; }
    
    [BsonElement("PhoneNumber")]
    public string PhoneNumber { get; set; }
}
