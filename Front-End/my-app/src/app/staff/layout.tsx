'use client';

import React from "react";

export default function UserLayout({ children }: { children: React.ReactNode }) {

    return (
        <div className="bg-indigo-400 p-5 min-h-screen">

            <h1 className="text-9xl font-bold text-green-400 mb-5">Đây là trang cho Staff (lỏd)</h1>
            <hr className="mb-4" />
            {children}
        </div>
    )
}