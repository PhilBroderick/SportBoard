namespace SportBoard.Data.DAL.Respositories
{
    public class UserPreferenceRepository : Repository<UserPreferences>, IUserPreferenceRepository
    {
        public UserPreferenceRepository(SportboardDbContext context) : base(context)
        {
        }
    }
}
