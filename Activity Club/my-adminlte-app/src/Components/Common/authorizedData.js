import axios from "axios";

// Create an instance of Axios with default settings
const apiClient = axios.create({
  baseURL: "https://localhost:7095/api/", // Base URL for your API
});

// Add an interceptor to include the token in the Authorization header
apiClient.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers["Authorization"] = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default apiClient;
