// src/app/signup/SignupForm.tsx
'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { register, RegisterData } from '../../services/authServices';

export default function SignupForm() {
    const router = useRouter();
    const [form, setForm] = useState<RegisterData>({
        tenTK: '',
        matKhau: '',
        tenHienThi: '',
        email: '',
        phone: '',
    });
    const [error, setError] = useState('');

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) =>
        setForm({ ...form, [e.target.name]: e.target.value });

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        try {
            await register(form);
            router.push('/login'); // chuyển về login sau đăng ký
        } catch (err: any) {
            setError(err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            {([
                { label: 'Tên tài khoản', name: 'tenTK', type: 'string' },
                { label: 'Mật khẩu', name: 'matKhau', type: 'string' },
                { label: 'Tên hiển thị', name: 'tenHienThi', type: 'string' },
                { label: 'Email', name: 'email', type: 'string' },
                { label: 'Số điện thoại', name: 'phone', type: 'string' },
            ]).map((f) => (
                <div key={f.name}>
                    <label className="block text-sm font-medium text-gray-700">{f.label}</label>
                    <input
                        type={f.type}
                        name={f.name}
                        value={(form as any)[f.name]}
                        onChange={handleChange}
                        required
                        className="mt-1 block w-full px-4 py-2 border rounded focus:border-indigo-500"
                    />
                </div>
            ))}

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
