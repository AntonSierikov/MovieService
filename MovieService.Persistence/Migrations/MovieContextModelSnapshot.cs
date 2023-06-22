﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieService.Persistence;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MovieService.Persistence.Migrations
{
    [DbContext(typeof(MovieContext))]
    partial class MovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MovieService.Persistence.Entities.ActorDal", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTimeOffset>("Birthday")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday");

                    b.Property<string>("ExtraId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("extra_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("ProfilePhotoUrl")
                        .HasColumnType("text")
                        .HasColumnName("profile_photo_url");

                    b.HasKey("Id");

                    b.HasIndex("ExtraId")
                        .IsUnique();

                    b.ToTable("actors");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.CastDal", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ActorId")
                        .HasColumnType("uuid")
                        .HasColumnName("actor_id");

                    b.Property<string>("Character")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("character");

                    b.Property<string>("ExtraId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("extra_id");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid")
                        .HasColumnName("movie_id");

                    b.HasKey("Id");

                    b.HasIndex("ActorId");

                    b.HasIndex("ExtraId")
                        .IsUnique();

                    b.HasIndex("MovieId");

                    b.ToTable("casts");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.GenreDal", b =>
                {
                    b.Property<int>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("genre");

                    b.HasKey("GenreId");

                    b.HasIndex("Genre")
                        .IsUnique();

                    b.ToTable("genres");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.JobState", b =>
                {
                    b.Property<string>("JobId")
                        .HasColumnType("text")
                        .HasColumnName("job_id");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("state");

                    b.HasKey("JobId");

                    b.ToTable("job_states");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieDal", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<decimal>("AverageMark")
                        .HasColumnType("numeric")
                        .HasColumnName("average_mark");

                    b.Property<decimal>("BoxOfficeAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("box_office_amount");

                    b.Property<string>("BoxOfficeCurrency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("box_office_currency");

                    b.Property<decimal>("BudgetAmount")
                        .HasColumnType("numeric")
                        .HasColumnName("budget_amount");

                    b.Property<string>("BudgetCurrency")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("budget_currency");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("MovieExtraId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("movie_extra_id");

                    b.Property<decimal>("Popularity")
                        .HasColumnType("numeric")
                        .HasColumnName("popularity");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("poster_url");

                    b.Property<string>("ProductionCompany")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("production_company");

                    b.Property<DateTimeOffset>("ReleasedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("released_at");

                    b.Property<int>("Runtime")
                        .HasColumnType("integer")
                        .HasColumnName("runtime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<int>("VoteCount")
                        .HasColumnType("integer")
                        .HasColumnName("vote_count");

                    b.HasKey("Id");

                    b.HasIndex("MovieExtraId")
                        .IsUnique();

                    b.ToTable("movies");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieGenreDal", b =>
                {
                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid")
                        .HasColumnName("movie_id");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("genre_id");

                    b.HasKey("MovieId", "GenreId");

                    b.HasIndex("GenreId");

                    b.ToTable("movie_genres");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieMarkDal", b =>
                {
                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid")
                        .HasColumnName("movie_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("FK_MovieMark")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FK_UserMark")
                        .HasColumnType("uuid");

                    b.Property<int>("Mark")
                        .HasColumnType("integer")
                        .HasColumnName("mark");

                    b.HasKey("MovieId", "UserId");

                    b.HasIndex("FK_MovieMark");

                    b.HasIndex("FK_UserMark");

                    b.ToTable("movie_marks");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieReviewDal", b =>
                {
                    b.Property<Guid>("ReviewId")
                        .HasColumnType("uuid")
                        .HasColumnName("review_id");

                    b.Property<bool>("Approved")
                        .HasColumnType("boolean")
                        .HasColumnName("approved");

                    b.Property<Guid>("FK_MovieReview")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FK_UserReview")
                        .HasColumnType("uuid");

                    b.Property<bool>("Hidden")
                        .HasColumnType("boolean")
                        .HasColumnName("hidden");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid")
                        .HasColumnName("movie_id");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("review_text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("ReviewId");

                    b.HasIndex("FK_MovieReview");

                    b.HasIndex("FK_UserReview");

                    b.ToTable("reviews");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.UserDal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<int>("Gender")
                        .HasColumnType("integer")
                        .HasColumnName("gender");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("login");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("ProfilePhoto")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("profile_photo");

                    b.Property<DateTimeOffset>("RegisteredAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("registered_at");

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.CastDal", b =>
                {
                    b.HasOne("MovieService.Persistence.Entities.ActorDal", "Actor")
                        .WithMany("Casts")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieService.Persistence.Entities.MovieDal", "Movie")
                        .WithMany("Casts")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieGenreDal", b =>
                {
                    b.HasOne("MovieService.Persistence.Entities.GenreDal", "Genre")
                        .WithMany("MovieGeners")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieService.Persistence.Entities.MovieDal", "Movie")
                        .WithMany("MovieGeners")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieMarkDal", b =>
                {
                    b.HasOne("MovieService.Persistence.Entities.MovieDal", "Movie")
                        .WithMany("Marks")
                        .HasForeignKey("FK_MovieMark")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieService.Persistence.Entities.UserDal", "User")
                        .WithMany("Marks")
                        .HasForeignKey("FK_UserMark")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieReviewDal", b =>
                {
                    b.HasOne("MovieService.Persistence.Entities.MovieDal", "Movie")
                        .WithMany("Reviews")
                        .HasForeignKey("FK_MovieReview")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieService.Persistence.Entities.UserDal", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("FK_UserReview")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.ActorDal", b =>
                {
                    b.Navigation("Casts");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.GenreDal", b =>
                {
                    b.Navigation("MovieGeners");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.MovieDal", b =>
                {
                    b.Navigation("Casts");

                    b.Navigation("Marks");

                    b.Navigation("MovieGeners");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("MovieService.Persistence.Entities.UserDal", b =>
                {
                    b.Navigation("Marks");

                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
