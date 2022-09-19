using EmployeeManagement.Application.Interfaces.Identity;

namespace EmployeeManagement.Infrastructure.Repositories;

public class IdentityRepository : IIdentityRepository
{
    private readonly IMongoCollection<Identity> _identityCollection;

    public IdentityRepository(IMongoDatabase mongoDatabase)
    {
        _identityCollection = mongoDatabase.GetCollection<Identity>("Identities");
    }

    public async Task<List<Identity>> GetIdentities() => await _identityCollection.Find(_ => true).ToListAsync();

    public async Task<Identity> HasAnyIdentity(string username, string email) => await _identityCollection.Find(x => x.Username == username || x.email == email).FirstOrDefaultAsync();
    
    public async Task<Identity> CheckIdentity(string userName, string password) => await _identityCollection.Find(x => x.Username == userName && x.Password == password).FirstOrDefaultAsync();
    

    public Task<Identity> GetIdentityById(string id) => _identityCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateIdentity(Identity identity) => await _identityCollection.InsertOneAsync(identity);

    public async Task UpdateIdendity(Identity identity) => await _identityCollection.ReplaceOneAsync( i => i.Id == identity.Id, identity);

    public async Task DeleteIdentity(string id) => await _identityCollection.DeleteOneAsync(i => i.Id == id);
}