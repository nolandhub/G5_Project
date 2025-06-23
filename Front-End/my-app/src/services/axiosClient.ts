import axios from 'axios';
import { toast } from 'react-toastify';

// Tạo instance axios
const axiosClient = axios.create({
    baseURL: 'http://localhost:5196/api/',
    headers: {
        'Content-Type': 'application/json',
    },
});

// Gắn token tự động nếu có
axiosClient.interceptors.request.use((config) => {
    const token = localStorage.getItem('token');
    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
}, (error) => {
    return Promise.reject(error);
});

// Xử lý phản hồi
axiosClient.interceptors.response.use(
    (response) => {
        // Chỉ trả về data
        return response.data
    },
    (error) => {
        // Lấy message từ server (nếu có), hoặc fallback
        const message = error?.response?.data?.message || 'Đã xảy ra lỗi!';

        toast.error(message); // Hiển thị lỗi bằng toast
        return Promise.reject(new Error(message));
    }
);

export default axiosClient;
