namespace Club
{
    using Club.Core.DataModels;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;

    public static class DbInitializer
    {
        public static async Task Initialize(MyDataContext context)
        {
            context.Database.EnsureCreated();

            // Check if the user already exists
            if (await context.Users.AnyAsync(u => u.Email == "abbassnouraldeenn@gmail.com"))
            {
                return; // User already exists
            }

            // Add a new user
            var newUser = new User
            {
                Email = "abbassnouraldeenn@gmail.com",
                Password = "1234",
                Name = "abbass noureddine",
                DOB = new DateTime(1990, 1, 1),
                Gender = "Male",
                Roles = new List<string> { "admin", "User" }
            };

            context.Users.Add(newUser);
            await context.SaveChangesAsync();
        }
    }

}
