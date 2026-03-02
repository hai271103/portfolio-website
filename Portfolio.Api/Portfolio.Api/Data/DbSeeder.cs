using Portfolio.Api.Models;

namespace Portfolio.Api.Data;

public static class DbSeeder
{
    public static void SeedData(ApplicationDbContext context)
    {
        // Check if data already exists
        if (context.Profiles.Any())
        {
            return; // Database has been seeded
        }

        // Seed Profile
        var profile = new Profile
        {
            FullName = "Nguyen Trong Hai",
            Title = "Fullstack Developer",
            Bio = "Seeking a Full-time .NET Internship position where I can strengthen my core C#/.NET, SQL, and web development fundamentals, gain real-world project experience, and grow under mentor guidance.",
            AvatarUrl = "/images/avatar/avatar.jpg",
            Email = "nguyentronghai1227@gmail.com",
            Phone = "0967152703",
            Location = "Hanoi, Vietnam",
            GitHubUrl = "https://github.com/hai271103",
            LinkedInUrl = "https://linkedin.com/in/nguyen-trong-hai",
            FacebookUrl = "https://www.facebook.com/hai.trong.nguyen.105613",
            CVUrl = "/assets/cv.pdf"
        };
        context.Profiles.Add(profile);

        // Seed Skills
        var skills = new List<Skill>
        {
            // Backend
            new Skill { Name = "C#", Category = "Backend", Hours = 600, IconUrl = "/images/icons/csharp.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "ASP.NET Core MVC", Category = "Backend", Hours = 500, IconUrl = "/images/icons/dotnet.png", Level = 5, DisplayOrder = 2 },
            new Skill { Name = "ASP.NET Core Web API", Category = "Backend", Hours = 550, IconUrl = "/images/icons/dotnet.png", Level = 5, DisplayOrder = 3 },
            new Skill { Name = "Entity Framework Core", Category = "Backend", Hours = 400, IconUrl = "/images/icons/ef.png", Level = 5, DisplayOrder = 4 },
            new Skill { Name = "Repository Pattern", Category = "Backend", Hours = 200, IconUrl = "/images/icons/pattern.png", Level = 4, DisplayOrder = 5 },
            new Skill { Name = "LINQ", Category = "Backend", Hours = 350, IconUrl = "/images/icons/csharp.png", Level = 4, DisplayOrder = 6 },

            // Database
            new Skill { Name = "SQL Server", Category = "Database", Hours = 400, IconUrl = "/images/icons/sqlserver.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "MySQL", Category = "Database", Hours = 240, IconUrl = "/images/icons/mysql.png", Level = 4, DisplayOrder = 2 },
            new Skill { Name = "Database Design", Category = "Database", Hours = 300, IconUrl = "/images/icons/database.png", Level = 4, DisplayOrder = 3 },

            // Frontend
            new Skill { Name = "HTML/CSS", Category = "Frontend", Hours = 500, IconUrl = "/images/icons/html.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "JavaScript", Category = "Frontend", Hours = 360, IconUrl = "/images/icons/js.png", Level = 4, DisplayOrder = 2 },
            new Skill { Name = "ReactJS", Category = "Frontend", Hours = 300, IconUrl = "/images/icons/react.png", Level = 4, DisplayOrder = 3 },
            new Skill { Name = "Bootstrap", Category = "Frontend", Hours = 200, IconUrl = "/images/icons/bootstrap.png", Level = 4, DisplayOrder = 4 },
            new Skill { Name = "Tailwind CSS", Category = "Frontend", Hours = 150, IconUrl = "/images/icons/tailwind.png", Level = 4, DisplayOrder = 5 },
            new Skill { Name = "jQuery", Category = "Frontend", Hours = 180, IconUrl = "/images/icons/jquery.png", Level = 3, DisplayOrder = 6 },
            new Skill { Name = "Axios", Category = "Frontend", Hours = 150, IconUrl = "/images/icons/axios.png", Level = 4, DisplayOrder = 7 },

            // Authentication & Security
            new Skill { Name = "JWT Authentication", Category = "Security", Hours = 150, IconUrl = "/images/icons/jwt.png", Level = 4, DisplayOrder = 1 },
            new Skill { Name = "Google OAuth 2.0", Category = "Security", Hours = 100, IconUrl = "/images/icons/oauth.png", Level = 3, DisplayOrder = 2 },
            new Skill { Name = "BCrypt", Category = "Security", Hours = 80, IconUrl = "/images/icons/security.png", Level = 3, DisplayOrder = 3 },

            // Tools & Others
            new Skill { Name = "Git", Category = "Tools", Hours = 400, IconUrl = "/images/icons/git.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "Docker", Category = "Tools", Hours = 140, IconUrl = "/images/icons/docker.png", Level = 3, DisplayOrder = 2 },
            new Skill { Name = "Swagger", Category = "Tools", Hours = 180, IconUrl = "/images/icons/swagger.png", Level = 4, DisplayOrder = 3 },
            new Skill { Name = "Postman", Category = "Tools", Hours = 200, IconUrl = "/images/icons/postman.png", Level = 4, DisplayOrder = 4 },
            new Skill { Name = "SignalR", Category = "Tools", Hours = 120, IconUrl = "/images/icons/signalr.png", Level = 3, DisplayOrder = 5 },
        };
        context.Skills.AddRange(skills);

        // Seed Projects
        var projects = new List<Project>
        {
            new Project
            {
                Title = "Education Management System",
                Description = "An online education management system integrating AI tutoring, automatic code checking (Piston), real-time chat, and online payment. The backend provides RESTful APIs and handles core business logic.",
                Role = "Fullstack Developer",
                ThumbnailUrl = "/images/projects/education.jpg",
                DemoUrl = "",
                GitHubUrl = "https://github.com/hai271103/education-management",
                Technologies = "[\"ASP.NET Core 8.0\",\"Entity Framework Core\",\"SignalR\",\"JWT\",\"Docker\",\"Swagger\",\"BCrypt\",\"SQL Server\",\"React\",\"Tailwind CSS\",\"Axios\",\"Monaco Editor\"]",
                StartDate = new DateTime(2025, 9, 1),
                EndDate = new DateTime(2025, 12, 31),
                IsFeatured = true,
                DisplayOrder = 1
            },
            new Project
            {
                Title = "Hotel Management System",
                Description = "A hotel management website with features such as online room booking, room search, payment processing, service management, invoices, and revenue reports. Built with ASP.NET Core MVC and Entity Framework Core.",
                Role = "Fullstack Developer (.NET)",
                ThumbnailUrl = "/images/projects/hotel.jpg",
                DemoUrl = "",
                GitHubUrl = "https://github.com/hai271103/HotelManagement.git",
                Technologies = "[\"ASP.NET Core MVC 8.0\",\"C#\",\"Entity Framework Core\",\"SQL Server\",\"Bootstrap\",\"jQuery\",\"AJAX\",\"Google OAuth 2.0\",\"MailKit\"]",
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 2, 28),
                IsFeatured = true,
                DisplayOrder = 2
            },
            new Project
            {
                Title = "Booking Flight System",
                Description = "A flight booking system allowing users to search flights, book tickets, make online payments, manage complaints and feedback, and support multi-role authorization. Designed and developed RESTful APIs using ASP.NET Core Web API.",
                Role = "Backend Developer (.NET)",
                ThumbnailUrl = "/images/projects/flight.jpg",
                DemoUrl = "",
                GitHubUrl = "https://github.com/vivien07/PRN232_BookingFlight_Group4.git",
                Technologies = "[\"ASP.NET Core 8.0 Web API\",\"Entity Framework Core\",\"Repository Pattern\",\"JWT Authentication\",\"SQL Server\",\"Razor Views\",\"HTML/CSS\",\"JavaScript\",\"jQuery\",\"Bootstrap\"]",
                StartDate = new DateTime(2025, 1, 1),
                EndDate = new DateTime(2025, 2, 28),
                IsFeatured = true,
                DisplayOrder = 3
            }
        };
        context.Projects.AddRange(projects);

        // Seed Social Links
        var socialLinks = new List<SocialLink>
        {
            new SocialLink { Platform = "GitHub", Url = "https://github.com/hai271103", IconClass = "fab fa-github", DisplayOrder = 1 },
            new SocialLink { Platform = "LinkedIn", Url = "https://linkedin.com/in/nguyen-trong-hai", IconClass = "fab fa-linkedin", DisplayOrder = 2 },
            new SocialLink { Platform = "Facebook", Url = "https://www.facebook.com/hai.trong.nguyen.105613", IconClass = "fab fa-facebook", DisplayOrder = 3 },
            new SocialLink { Platform = "Email", Url = "mailto:nguyentronghai1227@gmail.com", IconClass = "fas fa-envelope", DisplayOrder = 4 }
        };
        context.SocialLinks.AddRange(socialLinks);

        // Save all changes
        context.SaveChanges();
    }
}
