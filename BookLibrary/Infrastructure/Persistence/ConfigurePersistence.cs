using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Domain.Readers;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Infrastructure.Persistence;

public static class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dataSourceBuild = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("Default"));
        dataSourceBuild.EnableDynamicJson();
        var dataSource = dataSourceBuild.Build();

        services.AddDbContext<ApplicationDbContext>(
            options => options
                .UseNpgsql(
                    dataSource,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .UseSnakeCaseNamingConvention()
                .ConfigureWarnings(w => w.Ignore(CoreEventId.ManyServiceProvidersCreatedWarning)));

        services.AddScoped<ApplicationDbContextInitialiser>();
        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<AuthorRepository>();
        services.AddScoped<IAuthorRepository>(provider => provider.GetRequiredService<AuthorRepository>());
        services.AddScoped<IAuthorQueries>(provider => provider.GetRequiredService<AuthorRepository>());

        services.AddScoped<BookRepository>();
        services.AddScoped<IBookRepository>(provider => provider.GetRequiredService<BookRepository>());
        services.AddScoped<IBookQueries>(provider => provider.GetRequiredService<BookRepository>());
        
        services.AddScoped<GenreRepository>();
        services.AddScoped<IGenreRepository>(provider => provider.GetRequiredService<GenreRepository>());
        services.AddScoped<IGenreQueries>(provider => provider.GetRequiredService<GenreRepository>());

        services.AddScoped<LibrarianRepository>();
        services.AddScoped<ILibrarianRepository>(provider => provider.GetRequiredService<LibrarianRepository>());
        services.AddScoped<ILibrarianQueries>(provider => provider.GetRequiredService<LibrarianRepository>());
        
        services.AddScoped<LoanRepository>();
        services.AddScoped<ILoanRepository>(provider => provider.GetRequiredService<LoanRepository>());
        services.AddScoped<ILoanQueries>(provider => provider.GetRequiredService<LoanRepository>());
        
        services.AddScoped<PublisherRepository>();
        services.AddScoped<IPublisherRepository>(provider => provider.GetRequiredService<PublisherRepository>());
        services.AddScoped<IPublisherQueries>(provider => provider.GetRequiredService<PublisherRepository>());
        
        services.AddScoped<ReaderRepository>();
        services.AddScoped<IReaderRepository>(provider => provider.GetRequiredService<ReaderRepository>());
        services.AddScoped<IReaderQueries>(provider => provider.GetRequiredService<ReaderRepository>());
    }
}