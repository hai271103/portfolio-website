// ===========================
// MAIN APPLICATION
// ===========================

// State Management
const AppState = {
  profile: null,
  skills: [],
  projects: [],
  socialLinks: [],
  isLoading: false,
  theme: localStorage.getItem('theme') || 'light',
};

// ===========================
// INITIALIZATION
// ===========================
document.addEventListener('DOMContentLoaded', async () => {
  console.log('🚀 Portfolio App Initializing...');
  
  // Initialize theme
  initializeTheme();
  
  // Load data from API
  await loadAllData();
  
  // Setup event listeners
  initializeEventListeners();
  
  // Initialize scroll animations
  initializeScrollAnimations();
  
  console.log('✅ Portfolio App Ready!');
});

// ===========================
// DATA LOADING
// ===========================
async function loadAllData() {
  showLoadingState();
  
  try {
    console.log('🔄 Starting to load all data...');
    
    // Load all data in parallel
    const [profileResult, skillsResult, projectsResult, socialLinksResult] = await Promise.all([
      API.profile.get(),
      API.skills.getAll(),
      API.projects.getAll(),
      API.socialLinks.getActive(),
    ]);

    console.log('📊 API Results:', {
      profile: profileResult,
      skills: skillsResult,
      projects: projectsResult,
      socialLinks: socialLinksResult
    });

    // Update state - Load skills first as tech badges depend on them
    if (skillsResult.success) {
      AppState.skills = skillsResult.data;
      console.log('✅ Skills loaded:', skillsResult.data.length, 'items');
      renderSkills();
    } else {
      console.warn('Failed to load skills:', skillsResult.error);
      loadFallbackSkills();
    }

    if (profileResult.success) {
      AppState.profile = profileResult.data;
      renderProfile(); // This will also render tech badges
    } else {
      console.warn('Failed to load profile:', profileResult.error);
      // Load default/fallback data
      loadFallbackProfile();
    }

    if (projectsResult.success) {
      AppState.projects = projectsResult.data;
      console.log('✅ Projects loaded:', projectsResult.data.length, 'items');
      renderProjects();
    } else {
      console.warn('Failed to load projects:', projectsResult.error);
      loadFallbackProjects();
    }

    if (socialLinksResult.success) {
      AppState.socialLinks = socialLinksResult.data;
      renderSocialLinks();
    }

    hideLoadingState();
  } catch (error) {
    console.error('Error loading data:', error);
    hideLoadingState();
    showErrorMessage('Failed to load data. Please try again later.');
  }
}

// ===========================
// RENDERING FUNCTIONS
// ===========================
function renderProfile() {
  const profile = AppState.profile;
  if (!profile) return;

  // Update hero section
  const nameEl = document.querySelector('#hero .name');
  if (nameEl) nameEl.textContent = profile.fullName;
  
  const titleEl = document.querySelector('#hero .title');
  if (titleEl) titleEl.textContent = profile.title;
  
  const bioEl = document.querySelector('#hero .bio');
  if (bioEl) bioEl.textContent = profile.bio;
  
  const avatarEl = document.querySelector('#hero .avatar');
  if (avatarEl) avatarEl.setAttribute('src', profile.avatarUrl);
  
  // Render tech badges from top skills
  renderTechBadges();
  
  // Update contact info (multiple locations: all .contact-email, etc.)
  document.querySelectorAll('.contact-email').forEach(el => {
    el.textContent = profile.email;
    if (el.tagName === 'A') el.href = `mailto:${profile.email}`;
  });
  
  document.querySelectorAll('.contact-phone').forEach(el => {
    el.textContent = profile.phone;
    if (el.tagName === 'A') el.href = `tel:${profile.phone}`;
  });
  
  document.querySelectorAll('.contact-location').forEach(el => {
    el.textContent = profile.location;
  });
  
  console.log('✅ Profile rendered');
}

function renderTechBadges() {
  const badgesContainer = document.getElementById('tech-badges');
  if (!badgesContainer || !AppState.skills || AppState.skills.length === 0) return;
  
  // Get top 5 skills by hours
  const topSkills = [...AppState.skills]
    .sort((a, b) => b.hours - a.hours)
    .slice(0, 5);
  
  badgesContainer.innerHTML = topSkills.map(skill => `
    <span class="tech-badge">${skill.name}</span>
  `).join('');
}

function renderSkills() {
  const skills = AppState.skills;
  console.log('🎨 renderSkills called with:', skills);
  
  if (!skills || skills.length === 0) {
    console.warn('⚠️ No skills to render');
    return;
  }

  // Group skills by category
  const groupedSkills = skills.reduce((acc, skill) => {
    if (!acc[skill.category]) {
      acc[skill.category] = [];
    }
    acc[skill.category].push(skill);
    return acc;
  }, {});

  console.log('📦 Grouped skills:', groupedSkills);

  const skillsContainer = document.querySelector('#skills .skills-container');
  console.log('📍 Skills container:', skillsContainer);
  
  if (!skillsContainer) {
    console.error('❌ Skills container not found!');
    return;
  }

  skillsContainer.innerHTML = '';

  // Render each category
  Object.entries(groupedSkills).forEach(([category, categorySkills]) => {
    const categorySection = `
      <div class="skill-category">
        <h3>${category}</h3>
        <div class="skill-grid">
          ${categorySkills.map(skill => `
            <div class="skill-card hover-lift">
              <img src="${skill.iconUrl}" alt="${skill.name}" class="skill-icon" onerror="this.style.display='none'">
              <h4>${skill.name}</h4>
            </div>
          `).join('')}
        </div>
      </div>
    `;
    skillsContainer.innerHTML += categorySection;
  });

  console.log('✅ Skills rendered');
}

function renderProjects() {
  const projects = AppState.projects;
  console.log('🎨 renderProjects called with:', projects);
  
  if (!projects || projects.length === 0) {
    console.warn('⚠️ No projects to render');
    return;
  }

  const projectsContainer = document.querySelector('#projects .projects-grid');
  console.log('📍 Projects container:', projectsContainer);
  
  if (!projectsContainer) {
    console.error('❌ Projects container not found!');
    return;
  }

  projectsContainer.innerHTML = projects.map(project => `
    <div class="project-card hover-lift">
      <img src="${project.thumbnailUrl}" alt="${project.title}" class="project-image" onerror="this.style.display='none'">
      <div class="project-content">
        <h3>${project.title}</h3>
        <p class="project-role">${project.role}</p>
        <p class="project-description">${project.description}</p>
        <div class="project-technologies">
          ${JSON.parse(project.technologies || '[]').map(tech => `
            <span class="tech-tag">${tech}</span>
          `).join('')}
        </div>
        <div class="project-links">
          ${project.demoUrl ? `<a href="${project.demoUrl}" target="_blank" class="btn btn-primary">View Demo</a>` : ''}
          ${project.gitHubUrl ? `<a href="${project.gitHubUrl}" target="_blank" class="btn btn-outline">GitHub</a>` : ''}
        </div>
      </div>
    </div>
  `).join('');

  console.log('✅ Projects rendered');
}

function renderSocialLinks() {
  const socialLinks = AppState.socialLinks;
  if (!socialLinks || socialLinks.length === 0) return;

  const socialHTML = socialLinks.map(link => `
    <a href="${link.url}" target="_blank" rel="noopener noreferrer" class="social-link" title="${link.platform}">
      <i class="${link.iconClass}"></i>
    </a>
  `).join('');

  // Render to hero section
  const heroSocialContainer = document.querySelector('.social-links-hero');
  if (heroSocialContainer) {
    heroSocialContainer.innerHTML = socialHTML;
  }

  // Render to footer
  const footerSocialContainer = document.querySelector('.footer-social');
  if (footerSocialContainer) {
    footerSocialContainer.innerHTML = socialHTML;
  }

  console.log('✅ Social links rendered');
}

// ===========================
// EVENT LISTENERS
// ===========================
function initializeEventListeners() {
  // Contact form submission
  const contactForm = document.querySelector('#contact-form');
  if (contactForm) {
    contactForm.addEventListener('submit', handleContactFormSubmit);
  }

  // Theme toggle
  const themeToggle = document.querySelector('#theme-toggle');
  if (themeToggle) {
    themeToggle.addEventListener('click', toggleTheme);
  }

  // Mobile menu toggle
  const mobileMenuToggle = document.querySelector('#mobile-menu-toggle');
  if (mobileMenuToggle) {
    mobileMenuToggle.addEventListener('click', toggleMobileMenu);
  }

  // Smooth scroll for navigation links
  document.querySelectorAll('a[href^="#"]').forEach(link => {
    link.addEventListener('click', smoothScrollTo);
  });

  // Back to top button
  const backToTopBtn = document.querySelector('#back-to-top');
  if (backToTopBtn) {
    window.addEventListener('scroll', () => {
      if (window.scrollY > 300) {
        backToTopBtn.classList.add('visible');
      } else {
        backToTopBtn.classList.remove('visible');
      }
    });
    backToTopBtn.addEventListener('click', () => {
      window.scrollTo({ top: 0, behavior: 'smooth' });
    });
  }

  console.log('✅ Event listeners initialized');
}

// ===========================
// CONTACT FORM HANDLER
// ===========================
async function handleContactFormSubmit(event) {
  event.preventDefault();

  const form = event.target;
  const formData = new FormData(form);
  const data = Object.fromEntries(formData);

  // Validate
  if (!data.name || !data.email || !data.message) {
    showToast('Please fill in all required fields', 'error');
    return;
  }

  // Show loading
  const submitBtn = form.querySelector('button[type="submit"]');
  const originalText = submitBtn.textContent;
  submitBtn.disabled = true;
  submitBtn.innerHTML = '<span class="spinner"></span> Sending...';

  try {
    const result = await API.contact.send(data);

    if (result.success) {
      showToast('Message sent successfully! I will get back to you soon.', 'success');
      form.reset();
    } else {
      showToast('Failed to send message. Please try again.', 'error');
    }
  } catch (error) {
    console.error('Contact form error:', error);
    showToast('An error occurred. Please try again.', 'error');
  } finally {
    submitBtn.disabled = false;
    submitBtn.textContent = originalText;
  }
}

// ===========================
// UTILITY FUNCTIONS
// ===========================
function showLoadingState() {
  AppState.isLoading = true;
  const overlay = document.getElementById('loading-overlay');
  if (overlay) {
    overlay.style.display = 'flex';
  }
  document.body.classList.add('loading');
}

function hideLoadingState() {
  AppState.isLoading = false;
  const overlay = document.getElementById('loading-overlay');
  if (overlay) {
    overlay.style.display = 'none';
  }
  document.body.classList.remove('loading');
  console.log('✅ Loading overlay hidden');
}

function showErrorMessage(message) {
  console.error(message);
  // Implement error UI display
}

function showToast(message, type = 'info') {
  const toast = document.createElement('div');
  toast.className = `toast toast-${type}`;
  toast.textContent = message;
  document.body.appendChild(toast);

  setTimeout(() => {
    toast.classList.add('show');
  }, 100);

  setTimeout(() => {
    toast.classList.remove('show');
    setTimeout(() => toast.remove(), 300);
  }, 3000);
}

function smoothScrollTo(event) {
  const href = event.currentTarget.getAttribute('href');
  if (!href.startsWith('#')) return;

  event.preventDefault();
  const target = document.querySelector(href);
  if (target) {
    target.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}

function toggleMobileMenu() {
  const nav = document.querySelector('.nav-menu');
  nav?.classList.toggle('active');
}

// ===========================
// THEME MANAGEMENT
// ===========================
function initializeTheme() {
  document.documentElement.setAttribute('data-theme', AppState.theme);
}

function toggleTheme() {
  AppState.theme = AppState.theme === 'light' ? 'dark' : 'light';
  localStorage.setItem('theme', AppState.theme);
  document.documentElement.setAttribute('data-theme', AppState.theme);
}

// ===========================
// SCROLL ANIMATIONS
// ===========================
function initializeScrollAnimations() {
  const observer = new IntersectionObserver(
    (entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          entry.target.classList.add('animate-fadeInUp');
          observer.unobserve(entry.target);
        }
      });
    },
    { threshold: 0.1 }
  );

  document.querySelectorAll('.animate-on-scroll').forEach((el) => {
    observer.observe(el);
  });
}

// ===========================
// FALLBACK DATA (for development)
// ===========================
function loadFallbackProfile() {
  AppState.profile = {
    fullName: 'Your Name',
    title: 'Full Stack Developer',
    bio: 'A passionate developer with a strong interest in technology.',
    avatarUrl: 'https://via.placeholder.com/300',
    email: 'your.email@example.com',
    phone: '+84 xxx xxx xxx',
    location: 'Ha Noi, Vietnam',
  };
  renderProfile();
}

function loadFallbackSkills() {
  AppState.skills = [
    { name: 'React', category: 'Frontend', hours: 300, iconUrl: 'https://via.placeholder.com/50', level: 4 },
    { name: 'C#', category: 'Backend', hours: 400, iconUrl: 'https://via.placeholder.com/50', level: 4 },
  ];
  renderSkills();
}

function loadFallbackProjects() {
  AppState.projects = [
    {
      title: 'Sample Project',
      role: 'Full Stack Developer',
      description: 'A sample project description',
      thumbnailUrl: 'https://via.placeholder.com/400x300',
      technologies: '["React", "Node.js"]',
      demoUrl: '#',
      githubUrl: '#',
    },
  ];
  renderProjects();
}

console.log('📝 main.js loaded');
