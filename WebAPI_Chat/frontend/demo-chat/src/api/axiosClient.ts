import { cookies } from "next/headers";
import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5225",
  headers: {
    "Content-Type": "application/json",
  },
});

api.interceptors.request.use(async (config) => {
  const token = (await cookies()).get("token")?.value;

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

const axiosClient = {
  get: async <T>(url: string): Promise<T> => {
    const res = await api.get<T>(url);
    return res.data;
  },

  post: async <T>(url: string, data?: any): Promise<T> => {
    const res = await api.post<T>(url, data);
    return res.data;
  },

  put: async <T>(url: string, data?: any): Promise<T> => {
    const res = await api.put<T>(url, data);
    return res.data;
  },

  delete: async <T>(url: string): Promise<T> => {
    const res = await api.delete<T>(url);
    return res.data;
  },
};

export default axiosClient;
