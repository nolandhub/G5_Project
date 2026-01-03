'use client';
import { useRouter } from 'next/navigation';
import axiosClient from '../../services/axiosClient';

import { toast } from 'react-toastify';
const handleGetAll = async () => {
    try {
        const users = await axiosClient.get('/TaiKhoan/GetAll');
        console.log(users);

        toast.success(`Đã lấy được thông tin ${users.data.length} người dùng`);
    } catch (err) {
        console.error('Lỗi:', err);
        // toast đã hiển thị trong axiosClient interceptor rồi
    }
};


export default function Home() {
    const router = useRouter();

    return (
        <div className='justify-items-center space-y-30 bg-yellow-300  h-screen w-screen'>
            <div className=" text-green-500 justify-items-center font-extrabold text-9xl pt-80">
                Welcome to Home!
            </div>
            <div className='flex space-x-2'>
                <button
                    onClick={() => router.push('/login')}
                    className="w-auto bg-blue-500 text-white py-2 rounded hover:bg-blue-600 transition p-2"
                >
                    ← Back
                </button>

                <button
                    onClick={handleGetAll}
                    className="w-auto bg-blue-500 text-white py-2 rounded hover:bg-blue-600 transition p-2"
                >
                    GetAll
                </button>
            </div>
        </div>

    );
}
