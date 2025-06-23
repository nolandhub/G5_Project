// src/app/signup/SignupForm.tsx
'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { register, RegisterFormData } from '../../services/authServices';
import { toast } from 'react-toastify';

export default function SignupForm() {

    const router = useRouter();
    const [form, setForm] = useState<RegisterFormData>({
        tenTK: '',
        tenHienThi: '',
        email: '',
        phone: '',
        matKhau: '',
        xacNhanMatKhau: '',
    });
    const [error, setError] = useState('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
        setForm({ ...form, [e.target.name]: e.target.value });

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        if (form.matKhau !== form.xacNhanMatKhau) {
            setError('Password is not same, Please check again!');
            return;
        }

        try {
            //bỏ xacNhanMatKhau vì gửi lên API không có field đó*****  dataToSubmit : biến để lưu  
            const { xacNhanMatKhau, ...dataToSubmit } = form;     //các trường sau khi bỏ xacNhanMatKhau

            await register(dataToSubmit);
            router.push('/login'); // chuyển về login sau đăng ký
            toast.success("Đăng ký thành công")
        } catch (err: any) {
            setError(err.message);
        }
    };



    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            {([
                { label: 'Tên tài khoản', name: 'tenTK', type: 'text' },
                { label: 'Tên hiển thị', name: 'tenHienThi', type: 'text' },
                { label: 'Email', name: 'email', type: 'email' },
                { label: 'Số điện thoại', name: 'phone', type: 'tel' },
            ]).map((f) => (
                <div key={f.name}>
                    <label className="block text-sm font-medium text-gray-700">{f.label}</label>
                    <input
                        type={f.type}
                        name={f.name}
                        value={(form as any)[f.name]}
                        onChange={handleChange}
                        required
                        className="mt-1 block w-full px-4 py-2 border rounded focus:outline-none focus:ring-1 focus:ring-indigo-500  focus:border-indigo-500"
                    />
                </div>
            ))}

            <div>
                <label className="block text-sm font-medium text-gray-700">Mật khẩu</label>
                <input
                    type="password"
                    name="matKhau"
                    value={form.matKhau}
                    onChange={handleChange}
                    required
                    className="mt-1 block w-full px-4 py-2 border rounded focus:outline-none focus:ring-1 focus:ring-indigo-500 focus:border-indigo-500"
                />
            </div>

            {/* Xác nhận mật khẩu */}
            <div>
                <label className="block text-sm font-medium text-gray-700">Xác nhận mật khẩu</label>
                <input
                    type="password"
                    name="xacNhanMatKhau"
                    value={form.xacNhanMatKhau}
                    onChange={handleChange}
                    required
                    className="mt-1 block w-full px-4 py-2 border rounded focus:outline-none focus:ring-1 focus:ring-indigo-500 focus:border-indigo-500"
                />
            </div>

            {error && <p className="text-red-600 text-sm">{error}</p>}

            <button
                type="submit"
                className="w-full bg-indigo-600 text-white py-2 rounded hover:bg-indigo-700"
            >
                Sign up
            </button>
        </form>
    );
}
