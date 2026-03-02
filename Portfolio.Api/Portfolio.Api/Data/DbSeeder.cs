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
            FullName = "Nguyễn Trọng Hải",
            Title = "Full Stack Developer",
            Bio = "A passionate developer with a strong interest in technology, logical problem-solving, and building innovative solutions.",
            AvatarUrl = "/images/avatar/avatar.jpg",
            Email = "haint@example.com",
            Phone = "+84 123 456 789",
            Location = "Ha Noi, Vietnam",
            GitHubUrl = "https://github.com/yourusername",
            LinkedInUrl = "https://linkedin.com/in/yourusername",
            FacebookUrl = "https://facebook.com/yourusername",
            CVUrl = "/assets/cv.pdf"
        };
        context.Profiles.Add(profile);

        // Seed Skills
        var skills = new List<Skill>
        {
            // Frontend
            new Skill { Name = "HTML/CSS", Category = "Frontend", Hours = 500, IconUrl = "/images/icons/html.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "JavaScript", Category = "Frontend", Hours = 450, IconUrl = "/images/icons/js.png", Level = 4, DisplayOrder = 2 },
            new Skill { Name = "React", Category = "Frontend", Hours = 300, IconUrl = "/images/icons/react.png", Level = 4, DisplayOrder = 3 },
            new Skill { Name = "Bootstrap", Category = "Frontend", Hours = 200, IconUrl = "/images/icons/bootstrap.png", Level = 4, DisplayOrder = 4 },

            // Backend
            new Skill { Name = "C#", Category = "Backend", Hours = 600, IconUrl = "/images/icons/csharp.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "ASP.NET Core", Category = "Backend", Hours = 500, IconUrl = "/images/icons/dotnet.png", Level = 5, DisplayOrder = 2 },
            new Skill { Name = "Node.js", Category = "Backend", Hours = 300, IconUrl = "/images/icons/nodejs.png", Level = 3, DisplayOrder = 3 },
            new Skill { Name = "Python", Category = "Backend", Hours = 250, IconUrl = "/images/icons/python.png", Level = 3, DisplayOrder = 4 },

            // Database
            new Skill { Name = "SQL Server", Category = "Database", Hours = 400, IconUrl = "/images/icons/sqlserver.png", Level = 4, DisplayOrder = 1 },
            new Skill { Name = "MySQL", Category = "Database", Hours = 300, IconUrl = "/images/icons/mysql.png", Level = 4, DisplayOrder = 2 },
            new Skill { Name = "MongoDB", Category = "Database", Hours = 200, IconUrl = "/images/icons/mongodb.png", Level = 3, DisplayOrder = 3 },
            new Skill { Name = "Entity Framework", Category = "Database", Hours = 350, IconUrl = "/images/icons/ef.png", Level = 4, DisplayOrder = 4 },

            // Tools
            new Skill { Name = "Git", Category = "Tools", Hours = 500, IconUrl = "/images/icons/git.png", Level = 5, DisplayOrder = 1 },
            new Skill { Name = "Docker", Category = "Tools", Hours = 150, IconUrl = "/images/icons/docker.png", Level = 3, DisplayOrder = 2 },
            new Skill { Name = "Postman", Category = "Tools", Hours = 200, IconUrl = "/images/icons/postman.png", Level = 4, DisplayOrder = 3 },
            new Skill { Name = "VS Code", Category = "Tools", Hours = 600, IconUrl = "/images/icons/vscode.png", Level = 5, DisplayOrder = 4 },
        };
        context.Skills.AddRange(skills);

        // Seed Projects
        var projects = new List<Project>
        {
            new Project
            {
                Title = "Portfolio Website",
                Description = "Personal portfolio website built with ASP.NET Core backend and vanilla JavaScript frontend. Features include dynamic content management, contact form, and responsive design.",
                Role = "Full Stack Developer",
                ThumbnailUrl = "/images/projects/portfolio.jpg",
                DemoUrl = "https://yourportfolio.com",
                GitHubUrl = "https://github.com/yourusername/portfolio",
                Technologies = "[\"C#\",\"ASP.NET Core\",\"JavaScript\",\"HTML/CSS\",\"SQL Server\"]",
                StartDate = DateTime.Now.AddMonths(-3),
                EndDate = DateTime.Now,
                IsFeatured = true,
                DisplayOrder = 1
            },
            new Project
            {
                Title = "E-Commerce Platform",
                Description = "Full-featured e-commerce platform with product catalog, shopping cart, payment integration, and admin dashboard.",
                Role = "Backend Developer",
                ThumbnailUrl = "/images/projects/ecommerce.jpg",
                DemoUrl = "https://demo-ecommerce.com",
                GitHubUrl = "https://github.com/yourusername/ecommerce",
                Technologies = "[\"C#\",\"ASP.NET Core\",\"React\",\"SQL Server\",\"Stripe API\"]",
                StartDate = DateTime.Now.AddMonths(-6),
                EndDate = DateTime.Now.AddMonths(-3),
                IsFeatured = true,
                DisplayOrder = 2
            },
            new Project
            {
                Title = "Task Management App",
                Description = "Collaborative task management application with real-time updates, team boards, and project tracking.",
                Role = "Full Stack Developer",
                ThumbnailUrl = "/images/projects/taskapp.jpg",
                DemoUrl = "https://demo-taskapp.com",
                GitHubUrl = "https://github.com/yourusername/taskapp",
                Technologies = "[\"Node.js\",\"Express\",\"MongoDB\",\"Socket.io\",\"Vue.js\"]",
                StartDate = DateTime.Now.AddMonths(-4),
                EndDate = DateTime.Now.AddMonths(-2),
                IsFeatured = false,
                DisplayOrder = 3
            }
        };
        context.Projects.AddRange(projects);

        // Seed Social Links
        var socialLinks = new List<SocialLink>
        {
            new SocialLink { Platform = "GitHub", Url = "https://github.com/yourusername", IconClass = "fab fa-github", DisplayOrder = 1 },
            new SocialLink { Platform = "LinkedIn", Url = "https://linkedin.com/in/yourusername", IconClass = "fab fa-linkedin", DisplayOrder = 2 },
            new SocialLink { Platform = "Facebook", Url = "https://facebook.com/yourusername", IconClass = "fab fa-facebook", DisplayOrder = 3 },
            new SocialLink { Platform = "Email", Url = "mailto:haint@example.com", IconClass = "fas fa-envelope", DisplayOrder = 4 }
        };
        context.SocialLinks.AddRange(socialLinks);

        // Save all changes
        context.SaveChanges();
    }
}
