using Microsoft.EntityFrameworkCore;
using AuthApiProject.Models;


public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    { }
    
    //creates user.db file from the User model 
    public DbSet<User> Users { get; set; }
}