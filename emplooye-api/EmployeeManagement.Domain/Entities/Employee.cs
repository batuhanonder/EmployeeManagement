namespace EmployeeManagement.Domain.Entities;

public class Employee
{ 
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    [BsonElement("name")]
    public string Name { get; set; }
    [BsonElement("email")]
    public string Email{ get; set; }
    [BsonElement("jobTitle")]
    public string JobTitle{ get; set; }
    [BsonElement("phone")]
    public string Phone{ get; set; }
    [BsonElement("imageUrl")]
    public string ImageUrl{ get; set; }
}