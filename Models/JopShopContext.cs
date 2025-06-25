using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//created at 5
namespace JopShop.Models;

public partial class JopShopContext : DbContext
{
    public JopShopContext()
    {
    }

    public JopShopContext(DbContextOptions<JopShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Application> Applications { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ProjectsUser> ProjectsUser { get; set; }
    public DbSet<Delivery> Deliveries { get; set; }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Rating> Ratings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=Abd;Database=JopShop;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Application>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__applicat__3213E83FE86F4366");

            entity.ToTable("applications");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppliedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("applied_at");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Job).WithMany(p => p.Applications)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("FK__applicati__job_i__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Applications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__applicati__user___5CD6CB2B");
        });
modelBuilder.Entity<OrderItem>(entity =>
{
    entity.HasKey(e => e.Id).HasName("PK_order_items");

    entity.ToTable("order_items");

    entity.Property(e => e.Id).HasColumnName("id");
    entity.Property(e => e.ProductId).HasColumnName("product_id");
    entity.Property(e => e.OrderId).HasColumnName("order_id");
    entity.Property(e => e.Quantity).HasColumnName("quantity");
    entity.Property(e => e.Price).HasColumnName("price");

    entity.HasOne(d => d.Product)
        .WithMany(p => p.OrderItems)
        .HasForeignKey(d => d.ProductId)
        .OnDelete(DeleteBehavior.Restrict) // هذا هو التعديل المطلوب
        .HasConstraintName("FK__order_ite__produ__571DF1D5");

    entity.HasOne(d => d.Order)
        .WithMany(p => p.OrderItems)
        .HasForeignKey(d => d.OrderId)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("FK__order_ite__order__5812160E");
});

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart_ite__3213E83FBBF71114");

            entity.ToTable("cart_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("added_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__cart_item__produ__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__cart_item__user___619B8048");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comments__3213E83F36C879CC");

            entity.ToTable("comments");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CommentText)
                .HasColumnType("text")
                .HasColumnName("comment_text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__comments__produc__73BA3083");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__comments__user_i__72C60C4A");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__jobs__3213E83F1E44BA0A");

            entity.ToTable("jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EmployerId).HasColumnName("employer_id");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Salary)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("salary");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.Employer).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.EmployerId)
                .HasConstraintName("FK__jobs__employer_i__5629CD9C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83F5447EDC3");

            entity.ToTable("orders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("pending")
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total_price");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__orders__user_id__68487DD7");
        });

        // modelBuilder.Entity<OrderItem>(entity =>
        // {
        //     entity.HasKey(e => e.Id).HasName("PK__order_it__3213E83FD09C429B");

        //     entity.ToTable("order_items");

        //     entity.Property(e => e.Id).HasColumnName("id");
        //     entity.Property(e => e.OrderId).HasColumnName("order_id");
        //     entity.Property(e => e.Price)
        //         .HasColumnType("decimal(10, 2)")
        //         .HasColumnName("price");
        //     entity.Property(e => e.ProductId).HasColumnName("product_id");
        //     entity.Property(e => e.Quantity).HasColumnName("quantity");

        //     entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
        //         .HasForeignKey(d => d.OrderId)
        //         .HasConstraintName("FK__order_ite__order__6B24EA82");

        //     entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
        //         .HasForeignKey(d => d.ProductId)
        //         .HasConstraintName("FK__order_ite__produ__6C190EBB");
        // });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__posts__3213E83F87715685");

            entity.ToTable("posts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostText)
                .HasColumnType("text")
                .HasColumnName("post_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__posts__user_id__778AC167");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__products__3213E83FF1C7BA9E");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_url");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SellerId).HasColumnName("seller_id");

            entity.HasOne(d => d.Seller).WithMany(p => p.Products)
                .HasForeignKey(d => d.SellerId)
                .HasConstraintName("FK__products__seller__52593CB8");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__projects__3213E83F688179D4");

            entity.ToTable("projects");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Path)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("path");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__projects__user_i__6EF57B66");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83FF0D48196");

            entity.ToTable("reviews");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ReviewText)
                .HasColumnType("text")
                .HasColumnName("review_text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__reviews__user_id__7B5B524B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F9A758A71");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E616451768390").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CvPath)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cv_path");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValue("user")
                .HasColumnName("role");
            entity.Property(e => e.Suspended)
                .HasDefaultValue(false)
                .HasColumnName("suspended");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ProjectsUser>(entity =>
 {
     entity.HasKey(e => e.Id);
     entity.Property(e => e.Title).IsRequired();
     entity.Property(e => e.Description).IsRequired();
     entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
     entity.Property(e => e.Path).HasMaxLength(255).IsUnicode(false);
     entity.Property(e => e.FileName).HasMaxLength(255).IsUnicode(false);

     entity.HasOne(e => e.User)
         .WithMany(u => u.ProjectsUser)
         .HasForeignKey(e => e.UserId)
         .OnDelete(DeleteBehavior.Cascade);
 });

modelBuilder.Entity<Rating>(entity =>
{
    entity.HasKey(e => e.Id);

    entity.Property(e => e.RatedUserId).HasColumnName("RatedUserId");
    entity.Property(e => e.FreelancerId).HasColumnName("FreelancerId");
    entity.Property(e => e.RatingValue).HasColumnName("RatingValue");
    entity.Property(e => e.CreatedAt).HasColumnName("CreatedAt");

    entity.HasOne(e => e.RatedUser)
        .WithMany(u => u.Ratings) // ✅ هذا هو الأهم
        .HasForeignKey(e => e.RatedUserId)
        .OnDelete(DeleteBehavior.NoAction);

    entity.HasOne(e => e.Freelancer)
        .WithMany(u => u.GivenRatings) // ✅ للمقَيِّم
        .HasForeignKey(e => e.FreelancerId)
        .OnDelete(DeleteBehavior.NoAction);
});

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



}
