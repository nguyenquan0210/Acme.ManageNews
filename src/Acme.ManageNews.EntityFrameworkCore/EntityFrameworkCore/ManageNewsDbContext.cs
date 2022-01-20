using Acme.ManageNews.Entities;
using Acme.ManageNews.Enums;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Acme.ManageNews.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ManageNewsDbContext :
    AbpDbContext<ManageNewsDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }
    //Manage News
    public DbSet<News> News { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ActiveUser> ActiveUsers { get; set; }

    public DbSet<Advertise> Advertises { get; set; }

    public DbSet<City> Cities { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Events> Eventses { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Rating> Ratings { get; set; }

    public DbSet<Services> Servicesses { get; set; }

    public DbSet<Save> Saves { get; set; }

    public DbSet<Topic> Topics { get; set; }

    #endregion

    public ManageNewsDbContext(DbContextOptions<ManageNewsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureIdentityServer();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<Contact>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Contacts",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Company).IsRequired().HasMaxLength(100);

            b.Property(x => x.Leader).IsRequired().HasMaxLength(50);

            b.Property(x => x.Position).IsRequired().HasMaxLength(50);

            b.Property(x => x.License).IsRequired().HasMaxLength(int.MaxValue);

            b.Property(x => x.Email).IsRequired().HasMaxLength(100);

            b.Property(x => x.Hotline).IsRequired().HasMaxLength(100);

            b.Property(x => x.Address).IsRequired().HasMaxLength(255);

            b.Property(x => x.ContactAdvertise).IsRequired().HasMaxLength(int.MaxValue);
        });

        builder.Entity<ActiveUser>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "ActiveUsers",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
        });
        builder.Entity<Advertise>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Advertises",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props

            b.Property(x => x.Title).IsRequired().HasMaxLength(255);

            b.Property(x => x.Description).IsRequired().HasMaxLength(int.MaxValue);

            b.Property(x => x.Url).IsRequired().IsUnicode(false).HasMaxLength(int.MaxValue);

            b.Property(x => x.UrlImg).IsRequired().IsUnicode(false).HasMaxLength(int.MaxValue);

            b.HasOne<Order>().WithMany().HasForeignKey(x => x.OrderId).IsRequired();
        });
        
        builder.Entity<Category>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Categories",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
           
            b.Property(x => x.Name).IsRequired();
        });
        builder.Entity<City>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Cities",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });
        builder.Entity<Comment>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Comments",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            
            b.Property(x => x.Title).IsRequired().HasMaxLength(255);


            b.HasOne<News>().WithMany().HasForeignKey(x => x.NewsId).IsRequired();

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);
        });
        builder.Entity<Events>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Eventses",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);

            b.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
        });
        builder.Entity<News>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "News",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
          
            b.Property(x => x.Title).IsRequired().HasMaxLength(255);

            b.Property(x => x.Description).IsRequired().HasMaxLength(255);

            b.Property(x => x.Content).IsRequired().HasMaxLength(int.MaxValue);

            b.Property(x => x.Img).IsRequired().HasMaxLength(100);

            b.Property(x => x.Keyword).IsRequired().HasMaxLength(255);

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();

            b.HasOne<Events>().WithMany().HasForeignKey(x => x.EventId).IsRequired();

            b.HasOne<City>().WithMany().HasForeignKey(x => x.CityId).IsRequired();

            b.HasOne<Topic>().WithMany().HasForeignKey(x => x.TopicId).IsRequired();
        });
        builder.Entity<Order>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Oders",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
           
            b.Property(x => x.Title).IsRequired().HasMaxLength(255);

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired();
            b.HasOne<Services>().WithMany().HasForeignKey(x => x.ServiceId).IsRequired();
        });
        builder.Entity<Rating>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Ratings",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
           
            b.Property(x => x.Checkrating).IsRequired().HasMaxLength(50);


            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);

            b.HasOne<News>().WithMany().HasForeignKey(x => x.NewsId).IsRequired();
        });
        builder.Entity<Services>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Services",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            
            b.Property(x => x.Title).IsRequired().HasMaxLength(50);

            b.Property(x => x.Description).IsRequired().HasMaxLength(255);

        });
        builder.Entity<Save>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Saves",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
           
            b.Property(x => x.check).IsRequired().HasMaxLength(255);

            b.HasOne<News>().WithMany().HasForeignKey(x => x.NewsId).IsRequired();

            b.HasOne<IdentityUser>().WithMany().HasForeignKey(x => x.UserId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);
        });
        builder.Entity<Topic>(b =>
        {
            b.ToTable(ManageNewsConsts.DbTablePrefix + "Topics",
                ManageNewsConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
      
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);

        });

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ManageNewsConsts.DbTablePrefix + "YourEntities", ManageNewsConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        

    }
}
