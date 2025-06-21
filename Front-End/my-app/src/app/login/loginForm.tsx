'use client';
import { useState } from 'react';
import { useRouter } from 'next/navigation';
import { login, LoginData } from '../../services/authServices';

export default function LoginForm() {
    const router = useRouter();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setError('');

        try {
            const data: LoginData = await login(email, password);
            localStorage.setItem('token', data.token);
            localStorage.setItem('maTk', data.maTK.toString());
            localStorage.setItem('tenHienThi', data.tenHienThi);
            router.push('/home');
        } catch (err: any) {
            setError(err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="space-y-6">
            <div>
                <label className="block text-sm font-bold text-gray-800">Email</label>
                <input
                    type="email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                    placeholder="Enter your email"
                    required
                    className="mt-1 block w-full px-4 py-2 border rounded focus:ring-2 focus:ring-red-400"
                />
            </div>

            <div>
                <label className="block text-sm font-bold text-gray-800">Password</label>
                <input
                    type="password"
                    value={password}
                    onChange={e => setPassword(e.target.value)}
                    placeholder="*********"
                    required
                    className="mt-1 block w-full px-4 py-2 border rounded focus:ring-2 focus:ring-red-400"
                />
            </div>

            {error && <p className="text-red-500 text-sm">{error}</p>}

            <button
                onClick={handleSubmit}
                className="w-full bg-red-500 text-white py-2 rounded hover:bg-red-600 transition"
            >
                Log in
            </button>
        </form>
    );
}
