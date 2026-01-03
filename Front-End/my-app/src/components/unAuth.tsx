// app/unauthorized/page.tsx
import React from 'react';
import Link from 'next/link';

export default function UnauthorizedPage() {
    return (
        <div className="min-h-screen flex flex-col items-center justify-center bg-red-50 text-center px-4">
            <h1 className="text-3xl font-bold text-red-600">⛔ Không có quyền truy cập</h1>
            <p className="mt-4 text-gray-700">Bạn không có quyền để truy cập vào trang này.</p>
            <Link href="/" className="mt-6 px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600">
                Quay về trang chủ
            </Link>
        </div>
    );
}
