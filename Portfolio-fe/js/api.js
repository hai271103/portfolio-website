// ===========================
// API CONFIGURATION
// ===========================
const API_CONFIG = {
  baseURL: 'https://localhost:7001/api', // Change to your API URL
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  }
};

// ===========================
// HTTP CLIENT
// ===========================
class ApiClient {
  constructor(config) {
    this.baseURL = config.baseURL;
    this.timeout = config.timeout;
    this.headers = config.headers;
  }

  async request(endpoint, options = {}) {
    const url = `${this.baseURL}${endpoint}`;
    const config = {
      ...options,
      headers: {
        ...this.headers,
        ...options.headers,
      },
    };

    try {
      const controller = new AbortController();
      const timeoutId = setTimeout(() => controller.abort(), this.timeout);

      const response = await fetch(url, {
        ...config,
        signal: controller.signal,
      });

      clearTimeout(timeoutId);

      if (!response.ok) {
        throw new Error(`HTTP Error: ${response.status} - ${response.statusText}`);
      }

      const data = await response.json();
      return { success: true, data };
    } catch (error) {
      console.error(`API Error [${endpoint}]:`, error);
      return { success: false, error: error.message };
    }
  }

  get(endpoint) {
    return this.request(endpoint, { method: 'GET' });
  }

  post(endpoint, data) {
    return this.request(endpoint, {
      method: 'POST',
      body: JSON.stringify(data),
    });
  }

  put(endpoint, data) {
    return this.request(endpoint, {
      method: 'PUT',
      body: JSON.stringify(data),
    });
  }

  delete(endpoint) {
    return this.request(endpoint, { method: 'DELETE' });
  }
}

// ===========================
// API SERVICE
// ===========================
const apiClient = new ApiClient(API_CONFIG);

const API = {
  // Profile Endpoints
  profile: {
    get: () => apiClient.get('/profile'),
    update: (data) => apiClient.put('/profile', data),
  },

  // Skills Endpoints
  skills: {
    getAll: () => apiClient.get('/skills'),
    getById: (id) => apiClient.get(`/skills/${id}`),
    getByCategory: (category) => apiClient.get(`/skills/category/${category}`),
    create: (data) => apiClient.post('/skills', data),
    update: (id, data) => apiClient.put(`/skills/${id}`, data),
    delete: (id) => apiClient.delete(`/skills/${id}`),
  },

  // Projects Endpoints
  projects: {
    getAll: () => apiClient.get('/projects'),
    getById: (id) => apiClient.get(`/projects/${id}`),
    getFeatured: () => apiClient.get('/projects/featured'),
    create: (data) => apiClient.post('/projects', data),
    update: (id, data) => apiClient.put(`/projects/${id}`, data),
    delete: (id) => apiClient.delete(`/projects/${id}`),
  },

  // Contact Endpoints
  contact: {
    send: (data) => apiClient.post('/contact', data),
    getAll: () => apiClient.get('/contact'),
    markAsRead: (id) => apiClient.put(`/contact/${id}/read`),
  },

  // Social Links Endpoints
  socialLinks: {
    getAll: () => apiClient.get('/sociallinks'),
    getActive: () => apiClient.get('/sociallinks/active'),
  },
};

// ===========================
// EXPORT
// ===========================
if (typeof module !== 'undefined' && module.exports) {
  module.exports = { API, API_CONFIG };
}
