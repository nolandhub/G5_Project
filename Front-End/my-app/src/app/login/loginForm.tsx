'use client';
import React, { useState } from 'react';
import { useRouter } from 'next/navigation';
import { loginRequest, login } from '../../services/authServices';
import { toast } from 'react-toastify';
import { jwtDecode } from 'jwt-decode';
import { ROLE } from '../constants/role';

export default function LoginForm() {
    const router = useRouter();
    const [form, setForm] = useState<loginRequest>({ tenTK: '', matKhau: '' })
    const [error, setError] = useState('');

    const handleChange = async (e: React.ChangeEvent<HTMLInputElement>) =>
        setForm({ ...form, [e.target.name]: e.target.value })   //dynamic key   key-value 

    interface JwtPayload {
        role: string
    }



    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        try {
            const res = await login(form);
            localStorage.setItem('token', res.data.token);
            console.log(res);

            const token = localStorage.getItem('token')
            if (token) {
                const decoded = jwtDecode<JwtPayload>(token)
                const role = decoded.role

                switch (role) {
                    case ROLE.ADMIN:
                        router.push('/admin/dashboard')
                        toast.success("Đăng nhập thành công ");
                        break;
                    case ROLE.STAFF:
                        router.push('/staff/dashboard')
                        toast.success("Đăng nhập thành công ");
                        break;
                    case ROLE.USER:
                        router.push('/user/dashboard')
                        toast.success("Đăng nhập thành công ");
                        break;
                    default:
                        router.push('/components/unAuth')
                        toast.error("Đăng nhập thất bại ");
                        break;
                }

            }

        } catch (err: any) {
            setError(err.message);
        }
    };



    return (
        <form onSubmit={handleSubmit} className="space-y-6">
            <div>
                <label className="block text-sm font-bold text-gray-800">Email</label>
                <input
                    name='tenTK'
                    value={form.tenTK}
                    onChange={handleChange}
                    type="text"
                    placeholder="Enter your email"
                    required
                    className="mt-1 block w-full px-4 py-2 border rounded focus:ring-2 focus:ring-red-400"
                />
            </div>

            <div>
                <label className="block text-sm font-bold text-gray-800">Password</label>
                <input
                    name='matKhau'
                    value={form.matKhau}
                    onChange={handleChange}
                    type="password"
                    placeholder="*********"
                    required
                    className=" mt-1 block w-full px-4 py-2 border rounded focus:ring-2 focus:ring-red-400"
                />
            </div>

            <div className="mb-4 flex items-center justify-between">
                <label className="flex items-center text-sm text-gray-600 font-bold">
                    <input type="checkbox" className="mr-2" />
                    Remember me
                </label>
                <a href="#" className="text-sm text-red-500 hover:underline font">
                    Forgotten password?
                </a>
            </div>

            {error && <p className="text-red-500 text-sm">{error}</p>}

            <button
                type='submit'
                className="w-full bg-red-500 text-white py-2 rounded hover:bg-red-600 transition"
            >
                Log in
            </button>
        </form>
    );
}
